using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Project_GPS
{
    class DataManager
    {
        public class JsonData
        {
            public DateTimeOffset Date { get; set; }
            public String File { get; set; }
            public IList<int> States { get; set; }
            public IList<String> Files { get; set; }
        }

        public class JsonProvincie
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public IList<JsonGemeente> Cities { get; set; }
        }

        public class JsonGemeente
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public IList<JsonStreet> Streets { get; set; }
        }

        public class JsonStreet
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public Double Length { get; set; }
            public JsonGraaf Graaf { get; set; }
        }

        public class JsonGraaf
        {
            public int ID { get; set; }
            public Dictionary<JsonKnoop, IList<JsonSegment>> Map { get; set; }
        }

        public class JsonKnoop
        {
            public int ID { get; set; }
            public JsonPunt Point { get; set; }
        }

        public class JsonSegment
        {
            public int ID { get; set; }
            public JsonKnoop Start { get; set; }
            public JsonKnoop End { get; set; }
            public IList<JsonPunt> Points { get; set; }
        }

        public class JsonPunt
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        public static JsonProvincie buildState(Provincie provincie)
        {
            JsonProvincie state = new JsonProvincie();
            state.ID = provincie.ID;
            state.Name = provincie.Name;

            IList<JsonGemeente> cities = new List<JsonGemeente>();
            foreach (Gemeente gemeente in provincie.gemeentes)
            {
                JsonGemeente city = new JsonGemeente();
                city.ID = gemeente.ID;
                city.Name = gemeente.Name;

                IList<JsonStreet> streets = new List<JsonStreet>();
                foreach (Straat straat in gemeente.straten)
                {
                    JsonStreet street = new JsonStreet();
                    street.ID = straat.ID;
                    street.Name = straat.Name;
                    street.Length = 0.0;

                    if (straat.Graaf != null)
                    {
                        street.Length = straat.Graaf.getLength();

                        JsonGraaf graaf = new JsonGraaf();
                        graaf.ID = straat.Graaf.id;

                        Dictionary<JsonKnoop, IList<JsonSegment>> map = new Dictionary<JsonKnoop, IList<JsonSegment>>();
                        foreach (Knoop k in straat.Graaf.map.Keys)
                        {
                            JsonKnoop knoop = buildKnoop(k);

                            IList<JsonSegment> segments = new List<JsonSegment>();
                            foreach (Segment s in straat.Graaf.map[k])
                            {
                                JsonSegment segment = new JsonSegment();
                                segment.ID = s.id;
                                segment.Start = buildKnoop(s.start);
                                segment.End = buildKnoop(s.end);

                                IList<JsonPunt> points = new List<JsonPunt>();
                                foreach (Punt punt in s.vertices)
                                    points.Add(buildPoint(punt));
                                segment.Points = points;

                                segments.Add(segment);
                            }

                            map.Add(knoop, segments);
                        }
                        graaf.Map = map;

                        street.Graaf = graaf;
                    }

                    streets.Add(street);
                }
                city.Streets = streets;

                cities.Add(city);
            }
            state.Cities = cities;

            return state;
        }

        public static JsonKnoop buildKnoop(Knoop k)
        {
            JsonKnoop knoop = new JsonKnoop();
            knoop.ID = k.id;

            JsonPunt point = new JsonPunt();
            point.X = k.punt.x;
            point.Y = k.punt.y;
            knoop.Point = point;

            return knoop;
        }

        public static JsonPunt buildPoint(Punt punt)
        {
            JsonPunt point = new JsonPunt();
            point.X = punt.x;
            point.Y = punt.y;
            return point;
        }

        public static JsonData JsonBuilder()
        {
            JsonData data = new JsonData();
            data.Date = DateTimeOffset.Now;
            data.File = "DATA_FILE";

            IList<int> States = new List<int>();
            IList<String> Files = new List<String>();
            foreach (Provincie provincie in Program.Provincies.Values)
            {
                States.Add(provincie.ID);
                Files.Add(provincie.ID + ".json");
            }
            data.States = States;
            data.Files = Files;

            return data;
        }

        public static void exportData(String path, String name, String type)
        {
            Directory.CreateDirectory(Program.appData + @"\generate\data\");

            if (type.ToLower() == "json")
            {
                foreach (Provincie provincie in Program.Provincies.Values)
                {
                    JsonProvincie state = buildState(provincie);
                    using (StreamWriter file = File.CreateText(Program.appData + @"\generate\data\" + state.ID + ".json"))
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.ContractResolver = new DictionaryAsArrayResolver();

                        JsonSerializer serialize = new JsonSerializer();
                        string json = JsonConvert.SerializeObject(state, settings);

                        file.Write(json);
                    }
                    state = null;
                }

                JsonData data = JsonBuilder();
                using (StreamWriter file = File.CreateText(Program.appData + @"\generate\data\data.json"))
                {
                    JsonSerializer serialize = new JsonSerializer();
                    serialize.Serialize(file, data);
                }
                data = null;

                ZipFile.CreateFromDirectory(Program.appData + @"\generate\data", path + @"\" + name + ".zip");
                Directory.Delete(Program.appData + @"\generate\data", true);
            }

            if (type.ToLower() == "txt") { }
        }

        class DictionaryAsArrayResolver : DefaultContractResolver
        {
            protected override JsonContract CreateContract(Type objectType)
            {
                if (objectType.GetInterfaces().Any(i => i == typeof(IDictionary) ||
                   (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>))))
                {
                    return base.CreateArrayContract(objectType);
                }

                return base.CreateContract(objectType);
            }
        }
    }
}
