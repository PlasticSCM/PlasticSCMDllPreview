using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace dllpreview
{
    class DLLInfoProvider
    {
        public static DLL DeSerializeObject(string filename)
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filename);
            DLL dll = new DLL();
            dll.ProductName = versionInfo.ProductName;
            dll.CompanyName = versionInfo.CompanyName;
            dll.FileName = versionInfo.InternalName;
            dll.comments = versionInfo.Comments;
            dll.productVersion = versionInfo.ProductVersion;
            dll.Version = versionInfo.FileVersion.ToString();
            dll.DllImage = WindowsThumbnailProvider.GetThumbnail(filename, 256, 256, ThumbnailOptions.BiggerSizeOk);
            FileInfo f = new FileInfo(filename);
            dll.Size = f.Length;
            dll.hashCode = GetMD5HashFromFile(filename);
            return dll;
        }

        public static string GetMD5HashFromFile(string filename)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var buffer = md5.ComputeHash(File.ReadAllBytes(filename));
                var sb = new StringBuilder();
                for (int i = 0; i < buffer.Length; i++)
                {
                    sb.Append(buffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
