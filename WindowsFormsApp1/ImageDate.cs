using System.Drawing;
using System.IO;
using System.Linq;

namespace ImDa
{
    public class ImageDate
    {
        public byte[] MasByte(Image image)
        {
            byte[] bytes;
            var ms = new MemoryStream();
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            }
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            return  bytes = ms.ToArray();
        }
        
        public Image RetIm(int[] mas)
        {
            byte[] b = mas.Select(i => (byte)i).ToArray();
            var imageMemoryStream = new MemoryStream(b);
            Image imgFromStream = Image.FromStream(imageMemoryStream);
            return  imgFromStream;
        }
    }
}