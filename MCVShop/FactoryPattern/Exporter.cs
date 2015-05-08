using MCVShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCVShop.FactoryPattern
{
    public interface Exporter
    {
        void export(List<Product> products);
    }
}
