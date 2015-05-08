using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MCVShop.Models
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Admin : Attribute
    {
        public int ID { set; get; }
        public string loginName { set; get; }
        public string Nume { get; set; }
        public string Prenume { set; get; }
        public string Parola { set; get; }
        public bool Logged = false;
    }

    public class AdminDBContext : DbContext
    {
        public DbSet<Admin> Users { get; set; }
    }
}