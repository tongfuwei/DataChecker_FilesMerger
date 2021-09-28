using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataChecker_FilesMerger.Helper
{
    public class OcrJson
    {
        public List<OcrItem> array { get; set; }
    }

    public class OcrItem
    {
        public Box box { get; set; }
        public string str { get; set; }
        public float score { get; set; }

        private Rect _rect;
        public Rect rect
        {
            get
            {
                if (_rect == new Rect())
                {
                    _rect = OcrHelper.BoxToRect(box);
                }
                return _rect;
            }
        }
    }
    public class Box
    {
        public int left { get; set; }
        public int top { get; set; }
        public int right { get; set; }
        public int bottom { get; set; }

        public static float X_times { get; set; }
        public static float Y_times { get; set; }
        public Box(int _left, int _top, int _right, int _bottom)
        {
            left = _left;
            top = _top;
            right = _right;
            bottom = _bottom;
        }
        public static void InitTimes(float x, float y)
        {
            X_times = x;
            Y_times = y;
        }
    }
}
