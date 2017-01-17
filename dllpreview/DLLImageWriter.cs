using System;
using System.Drawing;

namespace dllpreview
{
    class DLLImageWriter
    {
        public static double MAX_THUMBNAIL_PIXELS = 1024;

        public static void WriteThumbnail(DLL deserialDll, string dstFile)
        {
            Size thumbnailSize = GetThumbnailSize(deserialDll.DllImage);
            Image NewImage =
                deserialDll.DllImage.GetThumbnailImage(
                    thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);

            deserialDll.DllImage.Dispose();
            NewImage.Save(dstFile);
        }

        public static void WriteFullImage(DLL deserialDll, string dstFile)
        {
            deserialDll.DllImage.Save(dstFile);
        }

        private static Size GetThumbnailSize(Image original)
        {
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            double factor = 1;
            if (originalWidth > originalHeight)
                factor = (double)MAX_THUMBNAIL_PIXELS / originalWidth;
            else
                factor = (double)MAX_THUMBNAIL_PIXELS / originalHeight;

            Size newSize = new Size(
                (int)(originalWidth * factor),
                (int)(originalHeight * factor));

            return newSize;
        }
    }
}
