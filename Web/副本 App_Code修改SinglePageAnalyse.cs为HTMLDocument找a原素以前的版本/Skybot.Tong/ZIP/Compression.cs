/******************************************************
* 文件名：Compression.cs
* 文件功能描述：用于对Buffer 进行压缩解压缩的功能 命名空间 Skybot.Tong.ZIP
* 
* 创建标识：周渊 2006-11-18
* 
* 修改标识：周渊 2011-10-26
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/


using System;
using System.IO;
using System.Text;
namespace Skybot.Tong.ZIP
{

    /// <summary>
    /// Zip文件处理方法
    /// </summary>
    public class Compression
    {
        public static CompressionType CompressionProvider = CompressionType.GZip;


        private static Stream OutputStream(Stream inputStream)
        {
            switch (CompressionProvider)
            {
                case CompressionType.BZip2:
                    return new ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream(inputStream);
                case CompressionType.GZip:
                    return new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(inputStream);
                case CompressionType.Zip:
                    return new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(inputStream);
                default:
                    return new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(inputStream);

            }
        }
        private static Stream InputStream(Stream inputStream)
        {
            switch (CompressionProvider)
            {
                case CompressionType.BZip2:
                    return new ICSharpCode.SharpZipLib.BZip2.BZip2InputStream(inputStream);
                case CompressionType.GZip:
                    return new ICSharpCode.SharpZipLib.GZip.GZipInputStream(inputStream);
                case CompressionType.Zip:
                    return new ICSharpCode.SharpZipLib.Zip.ZipInputStream(inputStream);
                default:
                    return new ICSharpCode.SharpZipLib.GZip.GZipInputStream(inputStream);
            }
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="bytesToCompress"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] bytesToCompress)
        {
            MemoryStream ms = new MemoryStream();
            Stream s = OutputStream(ms);
            s.Write(bytesToCompress, 0, bytesToCompress.Length);
            s.Close();
            return ms.ToArray();
        }
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="bytesToCompress"></param>
        /// <returns></returns>
        public static string Compress(string stringToCompress)
        {
            byte[] compressedData = CompressToByte(stringToCompress);
            string strOut = Convert.ToBase64String(compressedData);
            return strOut;
        }
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="bytesToCompress"></param>
        /// <returns></returns>
        public static byte[] CompressToByte(string stringToCompress)
        {
            byte[] bytData = Encoding.Unicode.GetBytes(stringToCompress);
            return Compress(bytData); ;
        }
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="bytesToCompress"></param>
        /// <returns></returns>
        public string DeCompress(string stringToDecompress)
        {
            string outString = string.Empty;
            if (stringToDecompress == null)
            {
                throw new ArgumentNullException("stringToDecompress", "You tried to use an empty string");
            }
            try
            {
                byte[] inArr = Convert.FromBase64String(stringToDecompress.Trim());
                outString = System.Text.Encoding.Unicode.GetString(DeCompress(inArr));
            }
            catch (NullReferenceException nEx)
            {
                return nEx.Message;
            }
            return outString;
        }
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="bytesToCompress"></param>
        /// <returns></returns>
        public static byte[] DeCompress(byte[] bytesToDecompress)
        {
            byte[] writeData = new byte[4096];
            Stream s2 = InputStream(new MemoryStream(bytesToDecompress));
            MemoryStream outStream = new MemoryStream();
            while (true)
            {
                int size = s2.Read(writeData, 0, writeData.Length);
                if (size > 0)
                {
                    outStream.Write(writeData, 0, size);
                }
                else
                {
                    break;
                }
            }
            s2.Close();
            byte[] outArr = outStream.ToArray();
            outStream.Close();
            return outArr;
        }
    }

    public enum CompressionType
    {
        GZip,
        BZip2,
        Zip
    }

}