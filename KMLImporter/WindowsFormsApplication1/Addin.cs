using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Bentley.MicroStation.InteropServices;
using Bentley.Interop.MicroStationDGN;
using BCOM = Bentley.Interop.MicroStationDGN;
using System.Threading;

namespace KMLImporter
{
    //[Bentley.MicroStation.AddInAttribute
    //             (KeyinTree = "LiDARFeatTable.LidarCommands.xml", MdlTaskID = "LiDARFeatTable")]
    class Addin : Bentley.MicroStation.AddIn
    {
        private BCOM.Application app = Utilities.ComApp;


        public static Addin s_addin = null;
        private Addin(System.IntPtr mdlDesc)
            : base(mdlDesc)
        {
            s_addin = this;
        }
        protected override int Run(string[] commandLine)
        {
            KMLWindow win = new KMLWindow();
            win.AttachAsTopLevelForm(Addin.s_addin, false);
            win.Show();
            return 0;
        }
      
        
         // uncomment below and change .net version and build path to work with winforms
        //[STAThread]
        //public static void Main()
        //{
        //    System.Windows.Forms.Application.EnableVisualStyles();
        //    System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        //    System.Windows.Forms.Application.Run(new KMLWindow());
        //}
         
    }
}