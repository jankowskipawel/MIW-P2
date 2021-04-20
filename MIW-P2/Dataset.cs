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
        public List<List<double>> normalizedAttributes;
        public List<List<float>> attributeRanges;
        public List<Dictionary<string, double>> stringAssignmentValues;

        public Dataset()
        {
            attributes = new List<List<object>>();
            attributeTypes = new List<string>();
            normalizedAttributes = new List<List<double>>();
            attributeRanges = new List<List<float>>();
            stringAssignmentValues = new List<Dictionary<string, double>>();
        }
    }
}
