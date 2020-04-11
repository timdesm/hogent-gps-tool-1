using System;
using System.Collections.Generic;
using System.Text;

namespace Project_GPS
{
    class Straat
    {
        public int ID { get; }
        public Graaf Graaf { get; set;  }
        public string Name { get; }

        public Straat(int id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.Graaf = null;
        }

        public Straat(int id, string name, Graaf graaf)
        {
            this.ID = id;
            this.Name = name;
            this.Graaf = graaf;
        }

        public void showStraat()
        {
            Console.WriteLine("[" + this.ID + "] " + this.Name + " : " + this.Graaf.ToString());
        }

        public List<Knoop> getKnopen()
        {
            return this.Graaf.getKnopen();
        }

        public override string ToString()
        {
            return "[ID: " + this.ID + "] " + this.Name;
        }
    }
}
