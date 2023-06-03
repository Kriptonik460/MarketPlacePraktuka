using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlacePraktuka.Models
{
    internal class SaveSomeData
    {
        public static User user;


        public static Client client => user.Client.First();

    }
}
