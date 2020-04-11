using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Project_GPS
{
    class DataLoader
    {
        // Temp data
        public static Dictionary<int, Graaf> grafenTemp = new Dictionary<int, Graaf>();
        public static Dictionary<int, int> streetGemeente = new Dictionary<int, int>();

        public static void WRdataThread(List<String> lines)
        {
            //Program.progressWatch.Add("WRdata",  Stopwatch.StartNew());
            for (int i = 1; i < lines.Count; i++)
            {
                readWRdataLine(lines[i]);
                Program.progressStatus["WRdata"] = i;
            }
           // Program.progressWatch["WRdata"].Stop();
        }

        public static void readWRdataLine(String line)
        {
            String[] data = line.Split(";");
            String[] pstring = (data[1].Replace("(", "").Replace(")", "").Replace("LINESTRING ", "")).Split(",");

            // Load points
            List<Punt> points = new List<Punt>();
            foreach (String pline in pstring)
            {
                String temp = pline;
                if (pline.StartsWith(" "))
                    temp = pline.Substring(1);
                Double x = Double.Parse(temp.Split(" ")[0].Replace(".", ","));
                Double y = Double.Parse(temp.Split(" ")[1].Replace(".", ","));
                Punt punt = new Punt(x, y);
                points.Add(punt);
            }

            // Create Knopen
            Knoop start = new Knoop(int.Parse(data[4]), points.First());
            Knoop end = new Knoop(int.Parse(data[5]), points.Last());

            // Create segment
            Segment segment = new Segment(int.Parse(data[0]), start, end, points);

            // Add to waiting cache
            int straat1 = int.Parse(data[6]);
            int straat2 = int.Parse(data[7]);

            if (straat1 != -9)
                addGraaf(straat1, segment);
            if (straat2 != -9)
                addGraaf(straat2, segment);
        }

        public static void addGraaf(int ID, Segment segment)
        {
            if (!grafenTemp.ContainsKey(ID))
                grafenTemp.Add(ID, new Graaf(ID));
            grafenTemp[ID].addSegment(segment);
        }

        public static void ProvincieInfoThread(List<String> lines)
        {
            while (Program.progressStatus["WRGemeentenaam"] < Program.progressMax["WRGemeentenaam"])
                Thread.Sleep(50);

            Program.progressWatch.Add("ProvincieInfo", Stopwatch.StartNew());
            for (int i = 1; i < lines.Count; i++)
            {
                readProvincieInfoLine(lines[i]);
                Program.progressStatus["ProvincieInfo"] = i;
            }
            Program.progressWatch["ProvincieInfo"].Stop();
        }

        public static void readProvincieInfoLine(String line)
        {
            String[] data = line.Split(";");
            if (data[2].ToLower() == "nl")
            {
                int ID = int.Parse(data[1]);
                if (!Program.Provincies.ContainsKey(ID))
                    Program.Provincies.Add(ID, new Provincie(ID, data[3]));
                int GemeenteID = int.Parse(data[0]);
                Program.Provincies[ID].addGemeente(Program.Cities[GemeenteID]);
            }
        }

        public static void WRGemeentenaamThread(List<String> lines)
        {
            Program.progressWatch.Add("WRGemeentenaam", Stopwatch.StartNew());
            for (int i = 1; i < lines.Count; i++)
            {
                readWRGemeentenaamLine(lines[i]);
                Program.progressStatus["WRGemeentenaam"] = i;
            }
            Program.progressWatch["WRGemeentenaam"].Stop();
        }

        public static void readWRGemeentenaamLine(String line)
        {
            String[] data = line.Split(";");
            int ID = int.Parse(data[1]);
            if (!Program.Cities.ContainsKey(ID))
                Program.Cities.Add(ID, new Gemeente(ID, data[3]));
        }

        public static void WRstraatnamenThread(List<String> lines)
        {
            Program.progressWatch.Add("WRstraatnamen", Stopwatch.StartNew());
            for (int i = 1; i < lines.Count; i++)
            {
                readWRstraatnamenLine(lines[i]);
                Program.progressStatus["WRstraatnamen"] = i;
            }
            Program.progressWatch["WRstraatnamen"].Stop();
        }

        public static void readWRstraatnamenLine(String line)
        {
            String[] data = line.Split(";");
            int ID = int.Parse(data[0]);
            if (!Program.Streets.ContainsKey(ID))
                Program.Streets.Add(ID, new Straat(ID, data[1].Replace(" ", "")));
        }

        public static void WRGemeneteIDThread(List<String> lines)
        {
            Program.progressWatch.Add("WRGemeneteID", Stopwatch.StartNew());
            for (int i = 1; i < lines.Count; i++)
            {
                readWRGemeneteIDLine(lines[i]);
                Program.progressStatus["WRGemeneteID"] = i;
            }
            Program.progressWatch["WRGemeneteID"].Stop();
        }

        public static void readWRGemeneteIDLine(String line)
        {
            String[] data = line.Split(";");
            int ID = int.Parse(data[0]);
            if (!streetGemeente.ContainsKey(ID))
                streetGemeente.Add(ID, int.Parse(data[1]));
        }

        public static void BuildStreetsThread()
        {
            while (Program.progressStatus["WRstraatnamen"] < Program.progressMax["WRstraatnamen"])
                Thread.Sleep(50);

            if (Program.progressStatus["WRstraatnamen"] >= Program.progressMax["WRstraatnamen"])
            {
                Program.progressStatus.Add("StreetBuild", 0);
                Program.progressMax.Add("StreetBuild", Program.progressMax["WRstraatnamen"]);
            }

            while (Program.progressStatus["WRdata"] < Program.progressMax["WRdata"])
                Thread.Sleep(50);

            foreach (Straat street in Program.Streets.Values)
            {
                if (grafenTemp.ContainsKey(street.ID))
                    Program.Streets[street.ID].Graaf = grafenTemp[street.ID];
                if (streetGemeente.ContainsKey(street.ID))
                    Program.Cities[streetGemeente[street.ID]].addStreet(Program.Streets[street.ID]);
                Program.progressStatus["StreetBuild"] += 1;
            }
        }
    }
}
