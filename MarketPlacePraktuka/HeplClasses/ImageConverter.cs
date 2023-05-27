using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;

namespace MarketPlacePraktuka.HeplClasses
{
    internal class ImageConverter
    {
        public static ImageSource ConvertToImageSource(byte[] bytes)
        {
            if (bytes == null) return null;
            BitmapImage bitImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(bytes);
            bitImg.BeginInit();
            bitImg.StreamSource = ms;
            bitImg.EndInit();
            ImageSource image = bitImg as ImageSource;
            return image;
        }

        public static byte[] OpenFileDialogSave()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image filies|*.jpg;*.jpeg;*.png"
            };

            if(openFileDialog.ShowDialog().GetValueOrDefault()) 
            {
                BitmapFrame.Create(new MemoryStream(File.ReadAllBytes(openFileDialog.FileName)), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                return File.ReadAllBytes(openFileDialog.FileName);
            }
            return null;
        }
    }
}
