using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISServer.Core
{
    public class Attribute
    {
        private char p1;
        private char p2;

        public Attribute()
        {
        }

        public Attribute(string key,string value)
        {
            Key = key; Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
