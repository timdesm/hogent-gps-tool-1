using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;

namespace Project_GPS
{
    class FileUtil
    {
        // Download manager
        public static void downloadDataThread(String url, String path, String file)
        {
            Thread thread = new Thread(() =>
            {
                Directory.CreateDirectory(path);

                if(File.Exists(Program.appData + @"\download\WRdata-master.zip"))
                    File.Delete(Program.appData + @"\download\WRdata-master.zip");

                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadDataProgress);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadDataCompleted);
                client.DownloadFileAsync(new Uri(url), path + @"\" + file);
            });
            thread.Start();
        }

        static void downloadDataProgress (object sender, DownloadProgressChangedEventArgs e)
        {
            Program.progressMax["DownloadFile"] = (int)ConvertBytesToMegabytes(e.TotalBytesToReceive);
            Program.progressStatus["DownloadFile"] = (int) ConvertBytesToMegabytes(e.BytesReceived);
        }

        static void downloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Directory.CreateDirectory(Program.appData + @"\extract");
            unZip(Program.appData + @"\download\WRdata-master.zip", Program.appData + @"\extract");
            Thread.Sleep(50);
            unZip(Program.appData + @"\extract\WRdata-master\WRdata.zip", Program.appData + @"\extract\WRdata-master\WRdata");
            Thread.Sleep(50);
            unZip(Program.appData + @"\extract\WRdata-master\WRstraatnamen.zip", Program.appData + @"\extract\WRdata-master\WRstraatnamen");

            Program.DownloadWait = false;
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }


        public static IReadOnlyCollection<ZipArchiveEntry> openZip(String path)
        {
            using(ZipArchive archive = ZipFile.OpenRead(path))
            {
                return archive.Entries;
            }
        }

        public static List<String> getCSVFiles(String path)
        {
            List<String> list = new List<String>();
            foreach(String file in Directory.EnumerateFiles(path, "*.csv"))
            {
                list.Add(file);
            }
            return list;
        }

        public static void  unZip(String pathFile, String pathTo)
        {
            if (Directory.Exists(pathTo))
            {
                Directory.Delete(pathTo, true);
            }
            Directory.CreateDirectory(pathTo);
            ZipFile.ExtractToDirectory(pathFile, pathTo);
        }

        public static List<String> readFileLines(String file)
        {
            List<String> list = new List<String>();
            using (var reader = new StreamReader(file))
            {
                while(!reader.EndOfStream)
                {
                    String line = reader.ReadLine();
                    list.Add(line);
                    
                }
            }
            return list;
        }

        public static List<String> readFileInZip(string path, string file)
        {
            IReadOnlyCollection<ZipArchiveEntry> entries = openZip(path);
            foreach(ZipArchiveEntry entry in entries)
            {
                if (entry.Name.Equals(file))
                {
                    return readFileLines(entry.FullName);
                }
            }
            return null;
        }

    }
}
