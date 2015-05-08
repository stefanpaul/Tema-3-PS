using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Collections;

namespace MCVShop.Models
{
    public class Product
    {
        public int ID { set; get; }
        public string nume { set; get; }
        public double pret { set; get; }
    }

    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Movies { get; set; }
    }
}