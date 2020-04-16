using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project_GPS
{
    // Copyright (c) 2020 Tim De Smet

    class Program
    {
        public static Dictionary<int, Provincie> Provincies = new Dictionary<int, Provincie>();
        public static Dictionary<int, Gemeente> Cities = new Dictionary<int, Gemeente>();
        public static Dictionary<int, Straat> Streets = new Dictionary<int, Straat>();


        private static Boolean progressEnabled = true;
        public static Dictionary<String, int> progressStatus = new Dictionary<string, int>();
        public static Dictionary<String, int> progressMax = new Dictionary<string, int>();
        public static Dictionary<String, System.Diagnostics.Stopwatch> progressWatch = new Dictionary<string, System.Diagnostics.Stopwatch>();

        public static Boolean DownloadWait;

        public static String appData;

        static void Main(string[] args)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appData = Path.Combine(appDataPath, @"TimDeSmet-HoGent\GPS-Project");

            // Download WRdata
            if (!File.Exists(appData + @"\download\WRdata-master.zip") || !File.Exists(appData + @"\extract\WRdata-master\WRdata\WRdata.csv"))
            {
                DownloadWait = true;
                printHeader();
                Console.WriteLine("Downloading data...");
                Console.WriteLine(" ");
                FileUtil.downloadDataThread("https://github.com/tvdewiel/WRdata/archive/master.zip", appData + @"\download", "WRdata-master.zip");
                var downloadProgressThread = Task.Run(() => DownloadProgressThread());

                while (DownloadWait)
                    Thread.Sleep(20);
            }

            Thread.Sleep(100);
            String folderPath = appData + @"\extract\WRdata-master";

            // Loading screen
            printHeader();
            Console.WriteLine("Loading GPS data...");
            Console.WriteLine(" ");

            // Load WRdata.csv
            String WRdataFile = folderPath + @"\WRdata\WRdata.csv";
            List<String> WRdata = FileUtil.readFileLines(WRdataFile);
            progressStatus.Add("WRdata", 0);
            progressMax.Add("WRdata", WRdata.Count - 1);
            var WRDataThread = Task.Run(() => DataLoader.WRdataThread(WRdata));

            // Load WRGemeentenaam.csv
            String WRGemeentenaamFile = folderPath + @"\WRGemeentenaam.csv";
            List<String> WRGemeentenaam = FileUtil.readFileLines(WRGemeentenaamFile);
            progressStatus.Add("WRGemeentenaam", 0);
            progressMax.Add("WRGemeentenaam", WRGemeentenaam.Count - 1);
            var WRGemeentenaamThread = Task.Run(() => DataLoader.WRGemeentenaamThread(WRGemeentenaam));

            // Load ProvincieIDsVlaanderen.csv
            String ProvincieIDsVlaanderenFile = folderPath + @"\ProvincieIDsVlaanderen.csv";
            List<String> ProvincieIDsVlaanderen = FileUtil.readFileLines(ProvincieIDsVlaanderenFile);
            progressStatus.Add("ProvincieIDsVlaanderen", 0);
            progressMax.Add("ProvincieIDsVlaanderen", 5);
            var ProvincieIDsVlaanderenThread = Task.Run(() => DataLoader.ProvincieIDsVlaanderenThread(ProvincieIDsVlaanderen));

            // Load WRdata.csv
            String provincieInfoFile = folderPath + @"\ProvincieInfo.csv";
            List<String> ProvincieInfo = FileUtil.readFileLines(provincieInfoFile);
            progressStatus.Add("ProvincieInfo", 0);
            progressMax.Add("ProvincieInfo", ProvincieInfo.Count - 1);
            var ProvincieInfoThread = Task.Run(() => DataLoader.ProvincieInfoThread(ProvincieInfo));

            // Load WRstraatnamen.csv
            String WRstraatnamenFile = folderPath + @"\WRstraatnamen\WRstraatnamen.csv";
            List<String> WRstraatnamen = FileUtil.readFileLines(WRstraatnamenFile);
            progressStatus.Add("WRstraatnamen", 0);
            progressMax.Add("WRstraatnamen", WRstraatnamen.Count - 1);
            var WRstraatnamenThread = Task.Run(() => DataLoader.WRstraatnamenThread(WRstraatnamen));
            var BuildStreetsThread = Task.Run(() => DataLoader.BuildStreetsThread());

            // Load WRGemeenteID.csv
            String WRGemeenteIDFile = folderPath + @"\WRGemeenteID.csv";
            List<String> WRGemeneteID = FileUtil.readFileLines(WRGemeenteIDFile);
            progressStatus.Add("WRGemeneteID", 0);
            progressMax.Add("WRGemeneteID", WRGemeneteID.Count - 1);
            var WRGemeneteIDThread = Task.Run(() => DataLoader.WRGemeneteIDThread(WRGemeneteID));


            // Start progress bars
            var progressThread = Task.Run(() => ProgressThread());

            // Keep alive
            while (true)
            {
                if (progressStatus.ContainsKey("StreetBuild") && progressMax.ContainsKey("StreetBuild"))
                    if (progressStatus["StreetBuild"] >= progressMax["StreetBuild"])
                    {
                        Thread.Sleep(50);
                        progressEnabled = false;
                        break;
                    }
            }

            Thread.Sleep(25);
            Boolean runApp = true;
            while (runApp)
            {
                printHeader();
                Console.WriteLine("All data has been loaded");
                Console.WriteLine(" ");

                Console.WriteLine("----- [MENU] -----");
                Console.WriteLine("[1] STATE LIST");
                Console.WriteLine("[2] STATE INFO");
                Console.WriteLine("[3] CITY LIST");
                Console.WriteLine("[4] CITY INFO");
                Console.WriteLine("[5] STREET LIST");
                Console.WriteLine("[6] STREET INFO");
                Console.WriteLine("[7] LOADING STATS");
                Console.WriteLine("[8] EXPORT RAPPORT");
                Console.WriteLine("[9] EXPORT DATA");
                Console.WriteLine("[10] CLOSE APPLICATION");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        MenuManager.case1();
                        break;
                    case "2":
                        MenuManager.case2();
                        break;
                    case "3":
                        MenuManager.case3();
                        break;
                    case "4":
                        MenuManager.case4();
                        break;
                    case "5":
                        MenuManager.case5();
                        break;
                    case "6":
                        MenuManager.case6();
                        break;
                    case "7":
                        MenuManager.case7();
                        break;
                    case "8":
                        MenuManager.case8();
                        break;
                    case "9":
                        MenuManager.case9();
                        break;
                    case "10":
                        runApp = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void DownloadProgressThread()
        {
            while(DownloadWait)
            {
                if (progressStatus.ContainsKey("DownloadFile") && progressMax.ContainsKey("DownloadFile"))
                {
                    Thread.Sleep(150);
                    drawTextProgressBar("Downloading data", progressStatus["DownloadFile"], progressMax["DownloadFile"], 8);
                }  
                Thread.Sleep(20);
            }
        }

        static void ProgressThread()
        {
            while (progressEnabled)
            {
                // WRdata
                drawTextProgressBar("Loading WRdata", progressStatus["WRdata"], progressMax["WRdata"], 8);
                drawTextProgressBar("Loading WRGemeentenaam", progressStatus["WRGemeentenaam"], progressMax["WRGemeentenaam"], 10);
                drawTextProgressBar("Loading ProvincieIDsVlaanderen", progressStatus["ProvincieIDsVlaanderen"], progressMax["ProvincieIDsVlaanderen"], 12);
                drawTextProgressBar("Loading ProvincieInfo", progressStatus["ProvincieInfo"], progressMax["ProvincieInfo"], 14);
                drawTextProgressBar("Loading WRGemeneteID", progressStatus["WRGemeneteID"], progressMax["WRGemeneteID"], 16);
                drawTextProgressBar("Loading WRstraatnamen", progressStatus["WRstraatnamen"], progressMax["WRstraatnamen"], 18);
                drawTextProgressBar("Building Grafen", DataLoader.grafenTemp.Count, 94215, 20);
                if (progressStatus.ContainsKey("StreetBuild"))
                    drawTextProgressBar("Odering Streets", progressStatus["StreetBuild"], progressMax["StreetBuild"], 22);
                Thread.Sleep(20);
            }

        }

        public static void drawTextProgressBar(string stepDescription, int progress, int total, int curstorTop)
        {
            int totalChunks = 50;

            Console.CursorLeft = 0;
            Console.CursorTop = curstorTop;
            Console.Write("[");
            Console.CursorLeft = totalChunks + 1;
            Console.Write("]");
            Console.CursorLeft = 1;

            double pctComplete = Convert.ToDouble((int)progress) / total;
            int numChunksComplete = Convert.ToInt16(totalChunks * pctComplete);

            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("".PadRight(numChunksComplete));

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("".PadRight(totalChunks - numChunksComplete));

            Console.CursorLeft = totalChunks + 5;
            Console.BackgroundColor = ConsoleColor.Black;

            string output = progress + " of " + total + " (" + string.Format("{0:F1}", pctComplete * 100) + "%)";
            Console.Write(output.PadRight(35) + stepDescription);
        }

        public static void printHeader()
        {
            Console.Clear();
            Console.WriteLine("--------------------------");
            Console.WriteLine("Project created by Tim De Smet");
            Console.WriteLine("HoGent GPS - Tool 1");
            Console.WriteLine("--------------------------");
            Console.WriteLine(" ");
        }
    }
}
