using ImageMagick;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using OpenCvSharp.Extensions;
using System.Collections;
using System.Drawing.Imaging;
using PdfiumViewer;
using DataChecker_FilesMerger.Entities;
using Newtonsoft.Json;

namespace DataChecker_FilesMerger.Helper
{
	static class OcrHelper
	{
		[DllImport("ocr_system.dll", EntryPoint = "Rec", SetLastError = true, CharSet = CharSet.Ansi)]
		static extern IntPtr Rec(byte[] input, int height, int width, StringBuilder configPath);


		public static Rect BoxToRect(Box box)
		{
			Rect r = new Rect(box.left, box.top, box.right - box.left, box.bottom - box.top);
			return r;
		}

		private static readonly object GhostScriptLock = new object();
		public static Stream ConvertPDF2Image(string pdfInputPath)
		{
			lock (GhostScriptLock)
			{
				MagickNET.SetGhostscriptDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
				MagickReadSettings settings = new MagickReadSettings();
				settings.Density = new Density(300, 300); //设置格式
				MagickImageCollection images = new MagickImageCollection();
				images.Read(pdfInputPath, settings);
				if (images.Count == 0)
					return null;
				MagickImage image = (MagickImage)images[0];
				if (image.HasAlpha)
					image.Alpha(AlphaOption.Background);
				image.Format = MagickFormat.Png;
				MemoryStream stream = new MemoryStream();
				image.Write(stream);
				//image.Write(@"D:\test\temp.png");
				return stream;
			}
		}

		public static void RenderPage(string pdfPath, string outputPath, int dpi = 300)
		{
			PdfDocument pdf = PdfDocument.Load(pdfPath);
			//string P_str_path = pdfPath.Substring(0, pdfPath.LastIndexOf("\\") + 1); // 文件路径
			var pagesizes = pdf.PageSizes;

			System.Drawing.Size size = new System.Drawing.Size();
			size.Height = (int)pagesizes[(1 - 1)].Height * 3;
			size.Width = (int)pagesizes[(1 - 1)].Width * 3;
			
			using (var stream = new FileStream(outputPath, FileMode.Create))
			using (var image = GetPageImage(1, size, pdf, dpi))
			{
				image.Save(stream, ImageFormat.Jpeg);
			}
		}
		static Image GetPageImage(int pageNumber, System.Drawing.Size size, PdfiumViewer.PdfDocument document, int dpi)
		{
			return document.Render(pageNumber - 1, size.Width, size.Height, dpi, dpi, PdfRenderFlags.Annotations);
		}

		public static bool OCR(string filePath, string outPath, string configPath, int sort)
		{
			#region 读取数据
			if (!File.Exists(filePath))
			{
				return false;
			}
			Mat mat;
			if (filePath.Substring(filePath.Length - 4) == ".pdf")
			{
				RenderPage(filePath, outPath);
				mat = Cv2.ImRead(outPath);
				//mat = Mat.FromStream(stream, ImreadModes.AnyColor);
			}
			else
				mat = Cv2.ImRead(filePath);
			#endregion
			#region OCR识别
			int stride;
			byte[] source = GetBGRValues(mat.ToBitmap(), out stride);
			//float X_times = (float)mat.Width / 2598;
			//float Y_times = (float)mat.Height / 1650;
			//Box.InitTimes(X_times, Y_times);
			StringBuilder stringBuilder = new StringBuilder(configPath);
			lock (GhostScriptLock)
			{
				IntPtr reconginzedStringPtr = Rec(source, mat.Height, mat.Width, stringBuilder);
				byte[] bytes = System.Text.Encoding.Unicode.GetBytes(Marshal.PtrToStringUni(reconginzedStringPtr));//转成UNICODE编码
				string result = System.Text.Encoding.UTF8.GetString(bytes);//转成UTF8
				mat.Dispose();
				#endregion
				if (sort == 1)
				{
					if (result.Contains("\"str\":\"企业人事档案袋\""))
						return true;
					else
						return false;
				}
				else if (sort == 2)
				{
					if (result.Contains("\"str\":\"企业人事档案\""))
						return true;
					else
						return false;
				}
				else if (sort == 3)
				{
					if (result.Contains("\"str\":\"本卷情况说明\""))
						return true;
					else
						return false;
				}
				else
					return false;

			}
		}

		public static byte[] GetBGRValues(Bitmap bmp, out int stride)
		{
			var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
			var bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
			stride = bmpData.Stride;
			var rowBytes = bmpData.Width * Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
			var imgBytes = bmp.Height * rowBytes;
			byte[] rgbValues = new byte[imgBytes];
			IntPtr ptr = bmpData.Scan0;
			for (var i = 0; i < bmp.Height; i++)
			{
				Marshal.Copy(ptr, rgbValues, i * rowBytes, rowBytes);
				ptr += bmpData.Stride;
			}
			bmp.UnlockBits(bmpData);
			return rgbValues;
		}

	}
}

