using System;
using System.Collections.Generic;
using System.Text;

namespace Project_GPS
{
    class Knoop
    {
        public int id { get; }
        public Punt punt { get; set; }

        public Knoop(int id, Punt punt) 
        {
            this.id = id;
            this.punt = punt;
        }

        public override bool Equals(object obj)
        {
            return obj is Knoop knoop &&
                this.id == knoop.id &&
                this.punt == knoop.punt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.id, this.punt.GetHashCode());
        }

        public override string ToString()
        {
            return "Knoop: " + this.id + ", " + this.punt.ToString();
        }


    }
}
