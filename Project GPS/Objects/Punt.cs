using System;
using System.Collections.Generic;
using System.Text;

namespace Project_GPS
{
    class Punt
    {
        public double x { get; set; }
        public double y { get; set; }

        public Punt(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Punt punt &&
                this.x == punt.x &&
                this.y == punt.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.x, this.y);
        }

        public override string ToString()
        {
            return "Punt: (" + this.x + " x " + this.y + ")";
        }
    }
}
