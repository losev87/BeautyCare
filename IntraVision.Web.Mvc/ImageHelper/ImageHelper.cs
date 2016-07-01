using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace IntraVision.Web.Mvc
{
    public class ImageHelper
    {
        public static bool StreamIsImage(Stream stream)
        {
            try
            {
                var bmp = new Bitmap(stream);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Image ConvertBytesToImage(byte[] bytes)
        {
            return Image.FromStream(new MemoryStream(bytes, 0, bytes.Length), true);
        }

        public static Image ConvertStreamToImage(Stream stream)
        {
            return Image.FromStream(stream);
        }

        public static Bitmap ResizeImage(byte[] bytes, int width, int height)
        {
            return ResizeImage(bytes, width, height, 0);
        }

        public static Bitmap ResizeImage(byte[] bytes, int width, int height, int dpi)
        {
            return ResizeImage(ConvertBytesToImage(bytes), width, height, dpi);
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            return ResizeImage(image, width, height, 0);
        }

        public static Bitmap ResizeImage(Image image, int width, int height, int dpi)
        {
            if (image.Width == width && image.Height == height)
                return new Bitmap(image);

            var result = new Bitmap(width, height);
            
            //use a graphics object to draw the resized image into the bitmap
            using (Graphics graphics = Graphics.FromImage(result))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }
            
            if (dpi > 0)
                result.SetResolution(dpi, dpi);
            
            //return the resulting bitmap
            return result;
        }

        public static Bitmap SquareImage(Image image)
        {
            int dimension = Math.Min(image.Width, image.Height);

            int x = 0;
            int y = 0;

            if (image.Width < image.Height)
                y = -1 * Convert.ToInt32((image.Height - dimension) / 2);
            else
                x = -1 * Convert.ToInt32((image.Width - dimension) / 2);

            var result = new Bitmap(dimension, dimension);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.DrawImage(image, x, y, image.Width, image.Height);
            }

            return result;
        }

        public static Bitmap SquareImage(byte[] image)
        {
            return SquareImage(ConvertBytesToImage(image));
        }

        public static string GetFormatName(ImageFormat format)
        {
            if (format.Equals(ImageFormat.Bmp))
                return "image/bmp";
            if (format.Equals(ImageFormat.Emf))
                return "image/emf";
            if (format.Equals(ImageFormat.Exif))
                return "image/exif";
            if (format.Equals(ImageFormat.Gif))
                return "image/gif";
            if (format.Equals(ImageFormat.Icon))
                return "image/icon";
            if (format.Equals(ImageFormat.Jpeg))
                return "image/jpeg";
            if (format.Equals(ImageFormat.MemoryBmp))
                return "image/bmp";
            if (format.Equals(ImageFormat.Png))
                return "image/png";
            if (format.Equals(ImageFormat.Tiff))
                return "image/tiff";
            if (format.Equals(ImageFormat.Wmf))
                return "image/wmf";
            return "image";
        }
    }
}
