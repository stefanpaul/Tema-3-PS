using MCVShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace MCVShop.FactoryPattern
{
    public class CSVExporter : Exporter
    {
        public CSVExporter()
        {

        }

        public void export(List<Product> products)
        {
            string attachment = "attachment; filename=PersonList.csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            WriteColumnName();
            foreach (Product product in products)
            {
                WriteUserInfo(product);
            }
            HttpContext.Current.Response.End();
        }

        private static void WriteUserInfo(Product product)
        {
            StringBuilder stringBuilder = new StringBuilder();
            AddComma(product.ID.ToString(), stringBuilder);
            AddComma(product.nume, stringBuilder);
            AddComma(product.pret.ToString(), stringBuilder);
            HttpContext.Current.Response.Write(stringBuilder.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        private static void AddComma(string value, StringBuilder stringBuilder)
        {
            stringBuilder.Append(value.Replace(',', ' '));
            stringBuilder.Append(", ");
        }

        private static void WriteColumnName()
        {
            string columnNames = "ID, Nume, Pret";
            HttpContext.Current.Response.Write(columnNames);
            HttpContext.Current.Response.Write(Environment.NewLine);
        }
    }
     
}