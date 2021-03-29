using System;
using System.Collections.Generic;

using System.Windows.Forms;
using System.Runtime.InteropServices;
using Bentley.MicroStation.InteropServices;
using Bentley.Interop.MicroStationDGN;
using BCOM = Bentley.Interop.MicroStationDGN;

namespace LiDARFeatTable
{
    [Bentley.MicroStation.AddInAttribute
                 (KeyinTree = "LiDARFeatTable.LidarCommands.xml", MdlTaskID = "LiDARFeatTable")]
    class Addin: Bentley.MicroStation.AddIn
    {
        private BCOM.Application app = Utilities.ComApp;
        

        public static Addin s_addin = null;
        private Addin(System.IntPtr mdlDesc): base(mdlDesc)
        {
            s_addin = this;
        }
        protected override int Run(string[] commandLine)
        {
            FeatTable FT = new FeatTable();
            FT.AttachAsTopLevelForm(Addin.s_addin, false); 
            FT.Show();
            return 0;
        }
        public static void OpenKeyIn(string unparsed)
        {
            FeatTable FT = new FeatTable();
            FT.AttachAsTopLevelForm(Addin.s_addin, false);
            FT.Show();
        }
    }
}
