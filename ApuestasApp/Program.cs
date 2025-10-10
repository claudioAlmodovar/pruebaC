using System;
using System.Windows.Forms;
using ApuestasApp.Forms;

namespace ApuestasApp;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
