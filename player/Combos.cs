using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace player
{
    class Combos
    {
        public Combos(string id, string val)
        {
            this.ID = id;
            this.Value = val;
        }

        public string ID { get; set; }

        public string Value { get; set; }

    }
}
