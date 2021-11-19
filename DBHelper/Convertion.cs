using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace DBHelper
{
    public class Convertion
    {
        /// <summary>
        /// Convert a file to bytes.
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public byte[] FileToByte(string FileName)
        {
            var fs = new FileStream(FileName, FileMode.Open);
            var br = new BinaryReader(fs);

            byte[] data = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return data;
        }

        /// <summary>
        /// Convert bytes to a file.
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="data"></param>
        public void ByteToFile(string FileName, byte[] data)
        {
            var fs = new FileStream(FileName, FileMode.Create);
            var bw = new BinaryWriter(fs);

            bw.Write(data);

            bw.Close();
            fs.Close();
        }

        public byte[] ImageToByte(Bitmap Image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (Image != null)
                {
                    var bmp = new Bitmap(Image);
                    bmp.Save(ms, Image.RawFormat);
                    return ms.ToArray();
                }
                else
                {
                    return new byte[] { };
                }
            }
        }

        //public Bitmap ByteToImage(byte[] ImageData)
        //{
        //    using (MemoryStream ms = new MemoryStream(ImageData))
        //    {
        //        if (ImageData != null)
        //        {
        //           //return ms.;
        //        }
        //        else
        //        {
        //            //return new byte[] { }; 
        //        }
        //    }
        //}
    }
}
