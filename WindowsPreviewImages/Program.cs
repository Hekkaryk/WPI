using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPreviewImages
{
    class Program
    {
        static string failure = "Failed to retrieve Windows Preview Images. Press any key to close this window.";
        static string success = "Press any key to close this window and open folder.";
        static void Main(string[] args)
        {
            string sourcePath = GetSourcePath(args);
            string destinationPath = GetDestinationPath(args);

            string copy = CopyFiles(sourcePath, destinationPath);
            if (copy != null)
            {
                Console.WriteLine(copy);
                Console.WriteLine(failure);
                Console.ReadKey();
            }
            else
            {
                string setNames = SetNames(destinationPath);
                if (setNames != null)
                {

                    Console.WriteLine(setNames);
                    Console.WriteLine(failure);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Windows Preview Images were successfully copied and renamed into " + destinationPath);
                    Console.WriteLine(success);
                    Console.ReadKey();
                    Process.Start(destinationPath);
                }
            }
        }

        private static string GetSourcePath(string[] args)
        {
            string sDefault = @"C:\Users\piotr\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
            if (args.Length == 0)
            {
                return string.Format(sDefault);
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);
                if (directoryInfo.Exists)
                {
                    return string.Format(args[0]);
                }
                else
                {
                    return string.Format(sDefault);
                }
            }
        }

        private static string GetDestinationPath(string[] args)
        {
            DateTime now = DateTime.Now;
            string year = now.Year.ToString();
            string month = now.Month < 10 ? "0" + now.Month.ToString() : now.Month.ToString();
            string day = now.Day < 10 ? "0" + now.Day.ToString() : now.Day.ToString();
            string hour = now.Hour < 10 ? "0" + now.Hour.ToString() : now.Hour.ToString();
            string minute = now.Minute < 10 ? "0" + now.Minute.ToString() : now.Minute.ToString();
            string second = now.Second < 10 ? "0" + now.Second.ToString() : now.Second.ToString();

            string sDefault = string.Format("D:\\preview\\{0}-{1}-{2}-{3}-{4}-{5}", now.Year, month, day, hour, minute, second);
            if (args.Length == 0)
            {
                return string.Format(sDefault);
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);
                if (directoryInfo.Exists)
                {
                    return string.Format(args[1]);
                }
                else
                {
                    return string.Format(sDefault);
                }
            }
        }

        private static string CopyFiles(string sourcePath, string destinationPath)
        {
            try
            {
                if (!Directory.Exists(sourcePath))
                {
                    return string.Format("Source directory does not exist; if you used default settings you'll have to add new Windows Preview Images path as a first parameter of this program.");
                }
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                foreach (var srcPath in Directory.GetFiles(sourcePath))
                {
                    File.Copy(srcPath, srcPath.Replace(sourcePath, destinationPath), true);
                }
                return null;
            }
            catch (Exception e)
            {
                return string.Format("An exception occured while copying files: {0}", e.Message);
            }
        }

        private static string SetNames(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return string.Format("Destination folder does not exist anymore; can't set file names.");
                }
                foreach (var srcPath in Directory.GetFiles(path))
                {
                    byte[] array = new byte[4];
                    using (FileStream fs = new FileStream(srcPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        fs.Read(array, 0, 4);
                    }
                    File.Move(srcPath, srcPath + "." + GetImageFormat(array).ToString());
                }
                return null;
            }
            catch (Exception e)
            {
                return string.Format("An exception occured while setting file names: {0}", e.Message);
            }
            
        }

        private enum ImageFormat
        {
            bmp,
            jpg,
            gif,
            tiff,
            png,
            unknown
        }

        private static ImageFormat GetImageFormat(byte[] bytes)
        {
            // see http://www.mikekunz.com/image_file_header.html  
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpg;

            return ImageFormat.unknown;
        }
    }
}
