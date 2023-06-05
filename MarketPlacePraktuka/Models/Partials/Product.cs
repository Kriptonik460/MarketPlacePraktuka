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
        public int CountProductInBasket => ProductList.Where(pl => pl.Basket.ID_Client == SaveSomeData.client?.ID && pl.Basket.Status == true).FirstOrDefault()?.Count ?? 0;
        
    }
}
