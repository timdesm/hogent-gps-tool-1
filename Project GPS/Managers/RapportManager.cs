using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project_GPS
{
    class RapportManager
    {
        public class JsonRapport
        {
            public DateTimeOffset Date { get; set; }
            public String File { get; set; }
            public JsonCount Count { get; set; }
            public IList<JsonProvincie> States { get; set; }
        }

        public class JsonCount
        {
            public int States { get; set; }
            public int Cities { get; set; }
            public int Streets { get; set; }
        }

        public class JsonProvincie
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public JsonProvincieCount Count { get; set; }
            public IList<JsonGemeente> Cities { get; set; }
        }

        public class JsonProvincieCount
        {
            public int Streets { get; set; }
            public int Cities { get; set; }
        }

        public class JsonGemeente
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public int Streets { get; set; }
            public JsonStreet Longest { get; set; }
            public JsonStreet Shortest { get; set; }

        }

        public class JsonStreet
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public Double Length { get; set; }
        }

        public static JsonStreet getDefaultJsonStreet()
        {
            JsonStreet street = new JsonStreet();
            street.ID = -9;
            street.Name = "NULL";
            street.Length = 0.0;
            return street;
        }

        public static JsonRapport JsonBuilder()
        {
            JsonRapport rapport = new JsonRapport();
            rapport.Date = DateTimeOffset.Now;
            rapport.File = "RAPPORT_FILE";

            JsonCount count = new JsonCount();
            count.States = Program.Provincies.Count;
            count.Cities = Program.Cities.Count;
            count.Streets = Program.Streets.Count;
            rapport.Count = count;

            IList<JsonProvincie> States = new List<JsonProvincie>();
            foreach(Provincie provincie in Program.Provincies.Values)
            {
                JsonProvincie state = new JsonProvincie();
                state.ID = provincie.ID;
                state.Name = provincie.Name;

                JsonProvincieCount stateCount = new JsonProvincieCount();
                stateCount.Streets = 0;
                stateCount.Cities = provincie.gemeentes.Count;
                state.Count = stateCount;

                IList<JsonGemeente> cities = new List<JsonGemeente>();
                foreach (Gemeente gemeente in provincie.gemeentes)
                {
                    stateCount.Streets += gemeente.straten.Count;

                    JsonGemeente city = new JsonGemeente();
                    city.ID = gemeente.ID;
                    city.Name = gemeente.Name;
                    city.Streets = gemeente.straten.Count;

                    List<Straat> tempList = new List<Straat>();
                    tempList = gemeente.straten.Where(x => x.Graaf != null).ToList();
                    tempList.Sort(delegate(Straat x, Straat y) {
                        if (x.Graaf == null && y.Graaf == null) return 0;
                        else if (x.Graaf == null) return -1;
                        else if (y.Graaf == null) return 1;
                        else return x.Graaf.getLength().CompareTo(y.Graaf.getLength());
                    });

                    JsonStreet longest = getDefaultJsonStreet();

                    if (tempList.Count > 1) {
                        longest.ID = tempList.Last().ID; // Null
                        longest.Name = tempList.Last().Name;
                        longest.Length = tempList.Last().Graaf.getLength();
                    }
                    city.Longest = longest;

                    JsonStreet shortest = getDefaultJsonStreet();
                    if (tempList.Count > 0) {
                        shortest.ID = tempList.First().ID;
                        shortest.Name = tempList.First().Name;
                        shortest.Length = tempList.First().Graaf.getLength();
                    }    
                    city.Shortest = shortest;

                    cities.Add(city);
                }
                state.Cities = cities;


                States.Add(state);
            }
            rapport.States = States;

            return rapport;
        }


        public static void exportRapport(String path, String name, String type)
        {
            if (type.ToLower() == "json")
            {
                JsonRapport rapport = JsonBuilder();
                using (StreamWriter file = File.CreateText(path + @"\" + name + ".json"))
                {
                    JsonSerializer serialize = new JsonSerializer();
                    serialize.Serialize(file, rapport);
                }
            }

            if (type.ToLower() == "txt") { }
        }
    }
}
