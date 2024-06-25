using System;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form1 = new Form1();
            Ermit_line ermitLine = new Ermit_line();

            Application.Run(ermitLine);
            Application.Run(form1);
        }
    }
}
