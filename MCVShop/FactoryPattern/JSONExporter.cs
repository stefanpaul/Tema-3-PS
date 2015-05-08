using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using MCVShop.Models;
using System.IO;

namespace MCVShop.FactoryPattern
{
    public class JSONExporter : Exporter
    {
        public JSONExporter()
        {

        }

        public void export(List<Product> products)
        {
            string path = @"d:\JSON.txt";
            if (!File.Exists(path)) 
            {
                foreach (Product product in products)
                {
                    var json = new JavaScriptSerializer().Serialize(product);
                    write((string)json);

                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(json);
                    }	
                }
            }
        }

        public void write(string json)
        {
           
            // This text is added only once to the file. 
            
        }
    }
}