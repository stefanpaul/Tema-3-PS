using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCVShop.FactoryPattern
{
    public class Factory 
    {
        private Exporter obj;
        public Factory()
        {

        }

        public Exporter getExporter(int type)
        {
            if(type == 3)
            {
                obj = new CSVExporter();
            }
            else 
                if(type == 4)
                {
                    obj = new JSONExporter();
                }
            return obj;
        }
    }
}