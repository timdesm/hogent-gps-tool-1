using System;
using System.Collections.Generic;
using System.Text;

namespace Project_GPS
{
    class Provincie
    {
        public int ID { get; set; }
        public String Name { get; set; }

        public List<Gemeente> gemeentes = new List<Gemeente>();

        public Provincie(int id, String name)
        {
            this.ID = id;
            this.Name = name;
        }

        public void addGemeente(Gemeente gemeente)
        {
            if (!gemeentes.Contains(gemeente))
                gemeentes.Add(gemeente);
        }
    }
}
