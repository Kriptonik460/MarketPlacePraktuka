//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarketPlacePraktuka.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductListOrder
    {
        public int ID_Product { get; set; }
        public int ID_Order { get; set; }
        public Nullable<int> Count { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
