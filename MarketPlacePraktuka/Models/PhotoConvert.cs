using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MarketPlacePraktuka.Models
{
    partial class Product
    {
        public ImageSource PhotoProductConvert =>HeplClasses.ImageConverter.ConvertToImageSource(PhotoProduct.FirstOrDefault()?.Photo);
    }

   
    
    
}
