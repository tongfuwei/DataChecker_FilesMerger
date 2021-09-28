using System;
using System.Collections.Generic;
using System.Threading;

namespace DataChecker_FilesMerger.Helper
{
    internal class ThreadHelper
    {

        public static int ThreadCount
        {
            get;
            set;
        } = 4;
        /// <summary>
        /// 线程等待时间
        /// </summary>
        public static int Wait
        {
            get;
            set;
        } = 0;
        public class ThreadBaseControl<T>
        {
            #region 变量&属性
            /// <summary>
            /// 回调委托
            /// </summary>
            public delegate void CallBackHanlder(T t);

            /// <summary>
            /// 回调方法
            /// </summary>
            private CallBackHanlder callBack;

            /// <summary>
            /// 待处理项
            /// </summary>
            private class PendingResult
            {
                /// 待处理值
                public T PendingValue { get; set; }

                /// 是否有值
                public bool IsHad { get; set; }
            }

            /// <summary>
            /// 线程数
            /// </summary>
            private static int ThreadCount
            {
                get
                {
                    return ThreadHelper.ThreadCount;
                }
                set
                {
                    ThreadHelper.ThreadCount = value;
                }
            }
            //private int m_ThreadCount = 8;

            /// <summary>
            /// 取消
            /// </summary>
            public bool Cancel { get; set; }

            /// <summary>
            /// 线程list
            /// </summary>
            private List<Thread> m_ThreadList;

            /// <summary>
            /// 任务完成计数器
            /// </summary>
            private volatile int m_CompletedCount = 0;

            /// <summary>
            /// 线程完成计数器
            /// </summary>
            private int m_ThreadCompetedCount = 0;

            /// <summary>
            /// 队列锁
            /// </summary>
            private static readonly object m_PendingQueueLock = new object();

            /// <summary>
            /// 任务完成锁
            /// </summary>
            private static readonly object m_OneCompletedLock = new object();

            /// <summary>
            /// 线程完成锁
            /// </summary>
            private static readonly object m_AllCompletedLock = new object();

            /// <summary>
            /// 内部队列
            /// </summary>
            private Queue<T> m_InnerQueue;

            /// <summary>
            /// 任务总数
            /// </summary>
            private int m_QueueCount = 0;
            #endregion

            #region 事件
            /// <summary>
            /// 单个开始事件
            /// </summary>
            public event Action<T> OneStart;
            private void OnOneJobStart(T pendingValue)
            {
                if (OneStart != null)
                {
                    try
                    {
                        OneStart(pendingValue);
                    }
                    catch { }
                }
            }

            /// <summary>
            /// 全部完成事件
            /// </summary>
            public event Action<CompetedEventArgs> AllCompleted;
            private void OnAllCompleted(CompetedEventArgs args)
            {
                if (AllCompleted != null)
                {
                    try
                    {
                        AllCompleted(args);//全部完成事件
                    }
                    catch { }
                }
            }

            /// <summary>
            /// 单个完成事件
            /// </summary>
            public event Action<T, CompetedEventArgs> OneCompleted;
            private void OnOneCompleted(T pendingValue, CompetedEventArgs args)
            {
                if (OneCompleted != null)
                {
                    try
                    {
                        OneCompleted(pendingValue, args);
                    }
                    catch { }
                }
            }
            #endregion

            #region 构造

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="collection">任务项</param>
            /// <param name="callBack">对任务操作的方法</param>
            public ThreadBaseControl(IEnumerable<T> collection, CallBackHanlder callBack)
            {
                m_InnerQueue = new Queue<T>(collection);
                this.m_QueueCount = m_InnerQueue.Count;
                this.callBack = callBack;
            }

            /// <summary>
            /// 无参数的构造函数需要向队列中填充元素,需要追加操作方法
            /// </summary>
            public ThreadBaseControl()
            {
                m_InnerQueue = new Queue<T>();
                this.m_QueueCount = m_InnerQueue.Count;
            }

            /// <summary>
            /// 取出队列中的第一项并将其从队列中移除
            /// </summary>
            /// <returns></returns>
            public T Peek()
            {
                var t = m_InnerQueue.Dequeue();
                this.m_QueueCount = m_InnerQueue.Count;
                return t;
            }
            /// <summary>
            /// 向队列的末尾追加项
            /// </summary>
            /// <param name="ff"></param>
            public void AddQueue(T ff)
            {
                try
                {
                    m_InnerQueue.Enqueue(ff);
                    this.m_QueueCount = m_InnerQueue.Count;
                }
                catch
                {
                    throw;
                }
            }

            /// <summary>
            /// 修改委托方法
            /// </summary>
            /// <param name="callBack">方法</param>
            public void AddCallBack(CallBackHanlder callBack)
            {
                this.callBack = callBack;
            }

            #endregion

            #region 主体

            /// <summary>
            /// 初始化线程
            /// </summary>
            private void InitThread()
            {
                m_ThreadList = new List<Thread>();

                for (int i = 0; i < ThreadCount; i++)
                {
                    Thread t = new Thread(new ThreadStart(InnerDoWork));
                    m_ThreadList.Add(t);
                    t.IsBackground = true;
                    t.Start(); 
                }
            }

            /// <summary>
            /// 开始进行线程工作
            /// </summary>
            public void Start()
            {
                InitThread();
            }

            /// <summary>
            /// 线程工作
            /// </summary>
            private void InnerDoWork()
            {
                try
                {
                    //初始化状态
                    Exception doWorkEx = null;
                    DoWorkResult doworkResult = DoWorkResult.ContinueThread;
                    //等待1ms让所有线程错开工作
                    if (Wait != 0)
                        Thread.Sleep(Wait);
                    //获取队列中的第一项
                    var t = CurrentPendingQueue;
                    //循环取工作内容
                    while (!this.Cancel && t.IsHad)
                    {
                        try
                        {
                            //触发单个开始事件
                            OnOneJobStart(t.PendingValue);
                            //执行操作,返回操作结果
                            doworkResult = DoWork(t.PendingValue);
                        }
                        catch (Exception ex)
                        {
                            //错误信息
                            doWorkEx = ex;
                        }
                        lock (m_OneCompletedLock)
                        {
                            //任务完成计数器
                            m_CompletedCount++;
                            //计算当前进度
                            int percent = m_CompletedCount * 100 / m_QueueCount;
                            //触发单个完成事件
                            OnOneCompleted(t.PendingValue, new CompetedEventArgs() { CompetedPrecent = percent, InnerException = doWorkEx });
                        }
                        //是否中止
                        if (doworkResult == DoWorkResult.AbortAllThread)
                        {
                            this.Cancel = true;
                            break;
                        }
                        else if (doworkResult == DoWorkResult.AbortCurrentThread)
                        {
                            break;
                        }
                        //提取下一项
                        t = CurrentPendingQueue;
                    }

                    lock (m_AllCompletedLock)
                    {
                        //分配给当前线程的工作全部完成
                        m_ThreadCompetedCount++;
                        //是否所有线程都结束工作
                        if (m_ThreadCompetedCount == m_ThreadList.Count)
                        {
                            //触发全部完成事件
                            OnAllCompleted(new CompetedEventArgs() { CompetedPrecent = 100 });
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }

            /// <summary>
            /// 执行操作
            /// </summary>
            /// <param name="pendingValue"></param>
            /// <returns></returns>
            protected DoWorkResult DoWork(T pendingValue)
            {
                try
                {
                    callBack(pendingValue);
                    return DoWorkResult.ContinueThread;//没有异常让线程继续跑..
                }
                catch (Exception ex)
                {
                    MainForm.CreateInstrance().WriteErrorInfo("线程控制", "[DoWork]", ex.Message);
                    return DoWorkResult.AbortCurrentThread;//有异常,可以终止当前线程.当然.也可以继续,
                    //return  DoWorkResult.AbortAllThread; //特殊情况下 ,有异常终止所有的线程...
                }
            }

            /// <summary>
            /// 获取当前项
            /// </summary>
            private PendingResult CurrentPendingQueue
            {
                get
                {
                    lock (m_PendingQueueLock)
                    {
                        PendingResult t = new PendingResult();

                        if (m_InnerQueue.Count != 0)
                        {
                            t.PendingValue = m_InnerQueue.Dequeue();
                            t.IsHad = true;
                        }
                        else
                        {
                            t.PendingValue = default(T);
                            t.IsHad = false;
                        }
                        return t;
                    }
                }
            }
            #endregion            
        }

        #region 相关类&枚举
        /// <summary>
        /// dowork结果枚举
        /// </summary>
        public enum DoWorkResult
        {
            /// 继续运行，默认
            ContinueThread = 0,
            /// 终止当前线程
            AbortCurrentThread = 1,
            /// 终止全部线程
            AbortAllThread = 2
        }
        /// <summary>
        /// 完成事件数据
        /// </summary>
        public class CompetedEventArgs : EventArgs
        {
            public CompetedEventArgs()
            {
            }
            /// 完成百分率
            public int CompetedPrecent { get; set; }
            /// 异常信息
            public Exception InnerException { get; set; }
        }
        #endregion
    }
}




