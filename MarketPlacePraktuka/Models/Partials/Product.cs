using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlacePraktuka.Models
{
    public partial class Product
    {
        public byte[] MainPhoto
        {
            get
            {
                var photo = this.PhotoProduct.FirstOrDefault();
                if (photo != null)
                    return photo.Photo;
                return null;

            }
        }
    }
}
