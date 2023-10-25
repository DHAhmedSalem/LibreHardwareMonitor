using System;
using System.IO;

class HotfixLogger
{
    public static void error(String msg)
    {
        string path = @"c:\Scripts\LHM.txt";
        if (!File.Exists(path) && Directory.Exists(Directory.GetParent(path).FullName))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("LHM Log");
            }
        }

        if (File.Exists(path))
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(String.Format("[{0}] - {1}", DateTime.Now.ToString(), msg));
            }
        }
    }
}
