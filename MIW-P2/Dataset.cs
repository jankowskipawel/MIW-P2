using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIW_P2
{
    class Dataset
    {
        public List<List<object>> attributes;
        public List<string> attributeTypes;

        public Dataset()
        {
            attributes = new List<List<object>>();
            attributeTypes = new List<string>();
        }
    }
}
