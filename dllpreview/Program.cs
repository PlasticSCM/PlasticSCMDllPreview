using System;
using System.Drawing;

namespace dllpreview
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) return;

            try
            {
                ProcessInfo(args);
            } catch (Exception e)
            {
                Console.WriteLine("Unable to process the file. Error: " + e.Message);
            }
        }

        private static void ProcessInfo(string[] args)
        {
            DLL dll = GetDLLObjectFromFile(args);

            switch (args.Length)
            {
                case 1:
                    PrintDllInfo(dll);
                    break;
                case 2:
                    DLLImageWriter.WriteFullImage(dll, args[1]);
                    PrintDllInfo(dll);
                    break;
                case 3:
                    DLLImageWriter.WriteThumbnail(dll, args[2]);
                    break;
            }
        }

        private static DLL GetDLLObjectFromFile(string[] args)
        {
            switch (args.Length)
            {
                case 1:
                case 2:
                    return DLLInfoProvider.DeSerializeObject(args[0]);
                case 3:
                    return DLLInfoProvider.DeSerializeObject(args[1]);
            }

            return null;
        }

        private static void PrintDllInfo(DLL deserializedDll)
        {
            Console.WriteLine(
                string.Format("FileName: {0}",
                    deserializedDll.FileName));
            Console.WriteLine(
                string.Format("HashCode: {0}",
                    deserializedDll.hashCode));
            Console.WriteLine(
                string.Format("Product Name: {0}",
                    deserializedDll.ProductName));
            Console.WriteLine(
                string.Format("Version: {0}",
                    deserializedDll.Version));
            Console.WriteLine(
                string.Format("Size: {0} bytes",
                    deserializedDll.Size));
        }
    }

    class DLL
    {
        public Bitmap DllImage;
        public string Version;
        internal string comments;
        internal string CompanyName;
        internal string FileName;
        internal string hashCode;
        internal string ProductName;
        internal string productVersion;
        internal object Size;
    }
}