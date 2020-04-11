using System;
using System.Collections.Generic;
using System.Text;

namespace Project_GPS
{
    class Gemeente
    {

        public int ID { get; set; }
        public String Name { get; set; }

        public List<Straat> straten = new List<Straat>();

        public Gemeente(int id, String name)
        {
            this.ID = id;
            this.Name = name;
        }

        public void addStreet(Straat straat)
        {
            if (!straten.Contains(straat))
                straten.Add(straat);
        }

        public override string ToString()
        {
            return "[ID: " + this.ID + "] " + this.Name;
        }
    }
}
