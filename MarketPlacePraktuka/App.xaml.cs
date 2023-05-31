﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MarketPlacePraktuka.Models;

namespace MarketPlacePraktuka
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public static MarketPlaceEntities DB = new MarketPlaceEntities();
        public static MarketPlaceEntities1 DB = new MarketPlaceEntities1();

        public App()
        {
            DB.ProductList.Load();
            DB.Product.Load();
            DB.User.Load();
        }
    }
}
