using System.Collections.Generic;

namespace Foundation.Dictionary.Models
{
    public class PhraseDictionary
    {
        public PhraseDictionary()
        {
            Values = new SortedDictionary<string, string>();
            Children = new List<PhraseDictionary>();
        }

        public string Key { get; set; }
        public SortedDictionary<string, string> Values { get; set; }
        public List<PhraseDictionary> Children { get; set; }
    }
}