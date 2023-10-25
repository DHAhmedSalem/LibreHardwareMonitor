// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) LibreHardwareMonitor and Contributors.
// Partial Copyright (C) Michael Möller <mmoeller@openhardwaremonitor.org> and Contributors.
// All Rights Reserved.

using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using LibreHardwareMonitor.UI;
using OxyPlot;

namespace LibreHardwareMonitor;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        try
        {
            if (!AllRequiredFilesAvailable())
            Environment.Exit(0);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(handleException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(handleException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
      
            using (MainForm form = new MainForm())
            {
                form.FormClosed += delegate
                {
                    Application.Exit();
                };

                
                Application.Run();
            }
        } catch (Exception ex)
        {
            string msg = String.Format("(Main) - {0}", ex.Message);
            HotfixLogger.error(msg);
        }
    }

    private static void handleException(object sender, EventArgs e)
    {
        string msg = String.Format("Exception Encountered: Object: {0}. Exception {1}", sender.ToString(), e.ToString());
        Console.WriteLine(msg);
    }

    private static bool IsFileAvailable(string fileName)
    {
        string path = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar;
        if (!File.Exists(path + fileName))
        {
            //MessageBox.Show("The following file could not be found: " + fileName +
            //                "\nPlease extract all files from the archive.", "Error",
            //                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        return true;
    }

    private static bool AllRequiredFilesAvailable()
    {
        if (!IsFileAvailable("Aga.Controls.dll"))
            return false;

        if (!IsFileAvailable("LibreHardwareMonitorLib.dll"))
            return false;

        if (!IsFileAvailable("OxyPlot.dll"))
            return false;

        if (!IsFileAvailable("OxyPlot.WindowsForms.dll"))
            return false;

        return true;
    }
}
