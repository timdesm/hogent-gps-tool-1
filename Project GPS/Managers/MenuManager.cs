using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_GPS
{
    class MenuManager
    {
        // provincie list
        public static void case1()
        {
            Program.printHeader();
            Console.WriteLine("----- [PROVINCIE LIST] -----");
            Console.WriteLine(" ");
            foreach (Provincie provincie in Program.Provincies.Values)
                Console.WriteLine("[ID: " + provincie.ID + "] " + provincie.Name);
            Console.WriteLine("");
            Console.Write("Press ENTER to continue...");
            Console.ReadLine();
        }

        // Provincie stats
        public static void case2()
        {
            Boolean runLoop = true;
            while (runLoop)
            {
                Program.printHeader();
                Console.WriteLine("----- [PROVINCIE INFO] -----");
                Console.WriteLine("[1] Search on Name");
                Console.WriteLine("[2] Search on ID");
                Console.WriteLine("[3] Go back");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Program.printHeader();
                        Console.WriteLine("----- [PROVINCIE INFO] -----");
                        Console.Write("Search on Name: ");
                        String NameSearch = Console.ReadLine();
                        Provincie provincie = Program.Provincies.Single(x => x.Value.Name.ToLower() == NameSearch.ToLower()).Value;
                        if (provincie != null)
                        {
                            Program.printHeader();
                            Console.WriteLine("----- [PROVINCIE INFO] -----");
                            Console.WriteLine("ID: " + provincie.ID);
                            Console.WriteLine("Name: " + provincie.Name);
                            Console.WriteLine("Cities: " + provincie.gemeentes.Count);
                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No provincie found with that name, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "2":
                        Program.printHeader();
                        Console.WriteLine("----- [PROVINCIE INFO] -----");
                        Console.Write("Search on ID: ");
                        String IdSearch = Console.ReadLine();
                        if (Program.Provincies.ContainsKey(int.Parse(IdSearch)))
                        {
                            Provincie provincie2 = Program.Provincies[int.Parse(IdSearch)];
                            Program.printHeader();
                            Console.WriteLine("----- [PROVINCIE INFO] -----");
                            Console.WriteLine("ID: " + provincie2.ID);
                            Console.WriteLine("Name: " + provincie2.Name);
                            Console.WriteLine("Cities: " + provincie2.gemeentes.Count);
                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No provincie found with that ID, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "3":
                        runLoop = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // City list-
        public static void case3()
        {
            Boolean runLoop = true;
            while (runLoop)
            {
                Program.printHeader();
                Console.WriteLine("----- [CITY LIST] -----");
                Console.WriteLine("[1] Search on (Provincie) Name");
                Console.WriteLine("[2] Search on (Provincie) ID");
                Console.WriteLine("[3] Go back");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Program.printHeader();
                        Console.WriteLine("----- [CITY LIST] -----");
                        Console.Write("Search on Name: ");
                        String NameSearch = Console.ReadLine();
                        Provincie provincie = Program.Provincies.Single(x => x.Value.Name.ToLower() == NameSearch.ToLower()).Value;
                        if (provincie != null)
                        {
                            Program.printHeader();
                            Console.WriteLine("----- [CITY LIST] -----");
                            Console.WriteLine(provincie.Name + ":");
                            foreach (Gemeente gemeente in provincie.gemeentes)
                                Console.WriteLine(gemeente.ToString());

                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No provincie found with that name, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "2":
                        Program.printHeader();
                        Console.WriteLine("----- [CITY LIST] -----");
                        Console.Write("Search on ID: ");
                        String IdSearch = Console.ReadLine();
                        if (Program.Provincies.ContainsKey(int.Parse(IdSearch)))
                        {
                            Provincie provincie2 = Program.Provincies[int.Parse(IdSearch)];
                            Program.printHeader();
                            Console.WriteLine("----- [CITY LIST] -----");
                            Console.WriteLine(provincie2.Name + ":");
                            foreach (Gemeente gemeente in provincie2.gemeentes)
                                Console.WriteLine(gemeente.ToString());

                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No provincie found with that ID, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "3":
                        runLoop = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // City stats
        public static void case4()
        {
            Boolean runLoop = true;
            while (runLoop)
            {
                Program.printHeader();
                Console.WriteLine("----- [CITY INFO] -----");
                Console.WriteLine("[1] Search on Name");
                Console.WriteLine("[2] Search on ID");
                Console.WriteLine("[3] Go back");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Program.printHeader();
                        Console.WriteLine("----- [CITY INFO] -----");
                        Console.Write("Search on Name: ");
                        String NameSearch = Console.ReadLine();
                        Gemeente gemeente = Program.Cities.Single(x => x.Value.Name.ToLower() == NameSearch.ToLower()).Value;
                        if (gemeente != null)
                        {
                            Program.printHeader();
                            Console.WriteLine("----- [CITY INFO] -----");
                            Console.WriteLine("ID: " + gemeente.ID);
                            Console.WriteLine("Name: " + gemeente.Name);
                            Console.WriteLine("Streets: " + gemeente.straten.Count);
                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No city found with that name, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "2":
                        Program.printHeader();
                        Console.WriteLine("----- [CITY INFO] -----");
                        Console.Write("Search on ID: ");
                        String IdSearch = Console.ReadLine();
                        if (Program.Cities.ContainsKey(int.Parse(IdSearch)))
                        {
                            Gemeente gemeente2 = Program.Cities[int.Parse(IdSearch)];
                            Program.printHeader();
                            Console.WriteLine("----- [PROVINCIE STATS] -----");
                            Console.WriteLine("ID: " + gemeente2.ID);
                            Console.WriteLine("Name: " + gemeente2.Name);
                            Console.WriteLine("Streets: " + gemeente2.straten.Count);
                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No city found with that ID, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "3":
                        runLoop = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // Street list
        public static void case5()
        {
            Boolean runLoop = true;
            while (runLoop)
            {
                Program.printHeader();
                Console.WriteLine("----- [STREET LIST] -----");
                Console.WriteLine("[1] Search on (City) Name");
                Console.WriteLine("[2] Search on (City) ID");
                Console.WriteLine("[3] Go back");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Program.printHeader();
                        Console.WriteLine("----- [STREET LIST] -----");
                        Console.Write("Search on Name: ");
                        String NameSearch = Console.ReadLine();
                        Gemeente gemeente = Program.Cities.Single(x => x.Value.Name.ToLower() == NameSearch.ToLower()).Value;
                        if (gemeente != null)
                        {
                            Program.printHeader();
                            Console.WriteLine("----- [STREET LIST] -----");
                            Console.WriteLine(gemeente.Name + ":");
                            foreach (Straat street in gemeente.straten)
                                Console.WriteLine(street.ToString());
                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No city found with that name, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "2":
                        Program.printHeader();
                        Console.WriteLine("----- [STREET LIST] -----");
                        Console.Write("Search on ID: ");
                        String IdSearch = Console.ReadLine();
                        if (Program.Cities.ContainsKey(int.Parse(IdSearch)))
                        {
                            Gemeente gemeente2 = Program.Cities[int.Parse(IdSearch)];
                            Program.printHeader();
                            Console.WriteLine("----- [STREET LIST] -----");
                            Console.WriteLine(gemeente2.Name + ":");
                            foreach (Straat street in gemeente2.straten)
                                Console.WriteLine(street.ToString());
                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No city found with that ID, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "3":
                        runLoop = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // Street stats
        public static void case6()
        {
            Boolean runLoop = true;
            while (runLoop)
            {
                Program.printHeader();
                Console.WriteLine("----- [STREET INFO] -----");
                Console.WriteLine("[1] Search on Name");
                Console.WriteLine("[2] Search on ID");
                Console.WriteLine("[3] Go back");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Program.printHeader();
                        Console.WriteLine("----- [STREET INFO] -----");
                        Console.Write("Search on Name: ");
                        String NameSearch = Console.ReadLine();


                        var gemeenteList = Program.Cities.Values.Where(x => x.straten.Where(y => y.Name.ToLower().StartsWith(NameSearch.ToLower())).Count() > 0);

                        if (gemeenteList.Count() > 1)
                        {
                            Program.printHeader();
                            Console.WriteLine("----- [STREET INFO] -----");
                            Console.WriteLine("Name: " + NameSearch);
                            Console.WriteLine("Total found: " + gemeenteList.Count());

                            foreach (Gemeente gemeente in gemeenteList)
                                Console.WriteLine(gemeente.ToString());
                            Console.Write("City ID: ");
                            String GemeenteSearch = Console.ReadLine();

                            Gemeente gemeente2 = Program.Cities.Single(x => x.Value.ID == int.Parse(GemeenteSearch)).Value;
                            Straat straat = gemeente2.straten.Single(x => x.Name.ToLower().StartsWith(NameSearch.ToLower()));
                            Program.printHeader();
                            Console.WriteLine("----- [STREET INFO] -----");
                            Console.WriteLine("ID: " + straat.ID);
                            Console.WriteLine("Name: " + straat.Name);
                            Console.WriteLine("Length: " + straat.Graaf.getLength() + "m");
                            Console.WriteLine("Gemeente: " + gemeente2.ToString());
                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else if (gemeenteList.Count() == 1)
                        {
                            Straat straat = gemeenteList.First().straten.Single(x => x.Name.ToLower().StartsWith(NameSearch.ToLower()));

                            Program.printHeader();
                            Console.WriteLine("----- [STREET INFO] -----");
                            Console.WriteLine("ID: " + straat.ID);
                            Console.WriteLine("Name: " + straat.Name);
                            Console.WriteLine("Length: " + straat.Graaf.getLength() + "m");
                            Console.WriteLine("Gemeente: " + gemeenteList.First().ToString());
                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No street found with that Name, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "2":
                        Program.printHeader();
                        Console.WriteLine("----- [STREET INFO] -----");
                        Console.Write("Search on ID: ");
                        String IdSearch = Console.ReadLine();
                        if (Program.Streets.ContainsKey(int.Parse(IdSearch)))
                        {
                            Straat straat = Program.Streets[int.Parse(IdSearch)];
                            Gemeente gemeente = Program.Cities.Single(x => x.Value.straten.Where(y => y.ID == straat.ID).Contains(straat)).Value;

                            Program.printHeader();
                            Console.WriteLine("----- [STREET INFO] -----");
                            Console.WriteLine("ID: " + straat.ID);
                            Console.WriteLine("Name: " + straat.Name.Replace(Environment.NewLine, ""));
                            Console.WriteLine("Length: " + straat.Graaf.getLength() + "m");
                            Console.WriteLine("Gemeente: " + gemeente.ToString());
                            Console.WriteLine("");
                            Console.Write("Press ENTER to continue...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("No street found with that ID, press ENTER to continue...");
                            Console.ReadLine();
                        }
                        break;
                    case "3":
                        runLoop = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // Data stats
        public static void case7()
        {
            Program.printHeader();
            Console.WriteLine("----- [DATA STATS] -----");
            Console.WriteLine(" ");
            Console.WriteLine("Provincies: " + Program.Provincies.Count);
            Console.WriteLine("Cities: " + Program.Cities.Count);
            Console.WriteLine("Streets: " + Program.Streets.Count);
            Console.WriteLine("");
            Console.WriteLine("WRData Loaded: " + Program.progressWatch["WRdata"].ElapsedMilliseconds + "ms");
            Console.WriteLine("WRGemeentenaam loaded: " + Program.progressWatch["WRGemeentenaam"].ElapsedMilliseconds + "ms");
            Console.WriteLine("ProvincieInfo loaded: " + Program.progressWatch["ProvincieInfo"].ElapsedMilliseconds + "ms");
            Console.WriteLine("WRGemeneteID loaded: " + Program.progressWatch["WRGemeneteID"].ElapsedMilliseconds + "ms");
            Console.WriteLine("WRstraatnamen loaded: " + Program.progressWatch["WRstraatnamen"].ElapsedMilliseconds + "ms");
            Console.WriteLine("");
            Console.Write("Press ENTER to continue...");
            Console.ReadLine();
        }

        // Export rapport
        public static void case8()
        {
            Boolean runLoop = true;
            while (runLoop)
            {
                Program.printHeader();
                Console.WriteLine("----- [EXPORT RAPPORT] -----");
                Console.WriteLine("[1] Export as JSON");
                Console.WriteLine("[ ] Export as TXT (disabled)");
                Console.WriteLine("[3] Go back");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Program.printHeader();
                        Console.WriteLine("----- [EXPORT RAPPORT (JSON)] -----");
                        Console.Write("File path: ");
                        String ExportPath = Console.ReadLine();

                        Program.printHeader();
                        Console.WriteLine("----- [EXPORT RAPPORT (JSON)] -----");
                        Console.Write("File name: ");
                        String ExportName = Console.ReadLine();

                        Boolean overviewLoop = true;
                        while (overviewLoop)
                        {
                            Program.printHeader();
                            Console.WriteLine("----- [EXPORT RAPPORT] -----");
                            Console.WriteLine("Overview: ");
                            Console.WriteLine("File name: " + ExportName + ".json");
                            Console.WriteLine("File path: " + ExportPath);
                            Console.WriteLine("Export type: JSON");
                            Console.Write("Are you sure to continue (Y/N)? ");
                            String continueExport = Console.ReadLine();

                            switch (continueExport.ToUpper())
                            {
                                case "Y":
                                    Program.printHeader();
                                    Console.WriteLine("Generating rapport....");
                                    RapportManager.exportRapport(ExportPath, ExportName, "json");
                                    overviewLoop = false;
                                    break;
                                case "N":
                                    overviewLoop = false;
                                    break;
                                default:
                                    Console.Write("Wrong selection input, press ENTER to continue...");
                                    Console.ReadLine();
                                    break;
                            }
                        }
                        break;
                    case "2":
                        break;
                    case "3":
                        runLoop = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // Export data
        public static void case9()
        {
            Boolean runLoop = true;
            while (runLoop)
            {
                Program.printHeader();
                Console.WriteLine("----- [EXPORT DATA] -----");
                Console.WriteLine("[1] Export as JSON");
                Console.WriteLine("[ ] Export as TXT (disabled)");
                Console.WriteLine("[3] Go back");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Program.printHeader();
                        Console.WriteLine("----- [EXPORT DATA (JSON)] -----");
                        Console.Write("File path: ");
                        String ExportPath = Console.ReadLine();

                        Program.printHeader();
                        Console.WriteLine("----- [EXPORT DATA (JSON)] -----");
                        Console.Write("File name: ");
                        String ExportName = Console.ReadLine();

                        Boolean overviewLoop = true;
                        while (overviewLoop)
                        {
                            Program.printHeader();
                            Console.WriteLine("----- [EXPORT DATA] -----");
                            Console.WriteLine("Overview: ");
                            Console.WriteLine("File name: " + ExportName + ".zip");
                            Console.WriteLine("File path: " + ExportPath);
                            Console.WriteLine("Export type: JSON");
                            Console.Write("Are you sure to continue (Y/N)? ");
                            String continueExport = Console.ReadLine();

                            switch (continueExport.ToUpper())
                            {
                                case "Y":
                                    Program.printHeader();
                                    Console.WriteLine("Generating data....");
                                    DataManager.exportData(ExportPath, ExportName, "json");
                                    overviewLoop = false;
                                    break;
                                case "N":
                                    overviewLoop = false;
                                    break;
                                default:
                                    Console.Write("Wrong selection input, press ENTER to continue...");
                                    Console.ReadLine();
                                    break;
                            }
                        }
                        break;
                    case "2":
                        break;
                    case "3":
                        runLoop = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
