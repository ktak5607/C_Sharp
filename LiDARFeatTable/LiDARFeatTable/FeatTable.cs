using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Bentley.MicroStation.WinForms;
using Bentley.Interop.MicroStationDGN;
using Bentley.MicroStation.InteropServices;
using System.Collections.Generic;
namespace LiDARFeatTable
{
    public partial class FeatTable : //Form
                                     Adapter
    {

        private Bentley.Interop.MicroStationDGN.Application app = Utilities.ComApp;
        private List<Level> levels = new List<Level>();
        public FeatTable()
        {
            InitializeComponent();
            foreach (Level level in app.ActiveModelReference.Levels)
            {
                if (level.Name.Contains("LIDAR"))
                {
                    levels.Add(level);
                }
            }
        }//end FeatTable Constructor


        private void MainWindow_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag.ToString() != "Main")
            {
                try
                {
                    Level actLevel = levels.Find(x => x.Name == e.Node.Tag.ToString());
                    app.ActiveSettings.Level = actLevel;
                    app.ActiveSettings.Color = app.ByLevelColor();
                    app.ActiveSettings.LineWeight = app.ByLevelLineWeight();
                    app.ActiveSettings.LineStyle = app.ByLevelLineStyle();
                }
                catch
                {
                    MessageBox.Show("The level for the feature you have selected doesn't appear to be in the level library. Please import the level and try again. If you need help call or email Kevin or CAD support.");
                }
                if (LTACheck.Checked)
                {
                    if (e.Node.Text == "Concrete" || e.Node.Text == "Drop Inlet" || e.Node.Text == "Sign Structure")
                    {
                        app.CadInputQueue.SendCommand("place block rotated");
                    }//end rotated block features
                    else if (e.Node.Text == "Water" || e.Node.Text == "Driveway")
                    {
                        app.CadInputQueue.SendCommand("place bspline curve points");
                    }//end bspline features
                    else if (e.Node.Text == "Building")
                    {
                        app.CadInputQueue.SendCommand("place shape orthogonal");
                    }//end orthogonal shape features
                    else if (e.Node.Text == "Traffic Striping")
                    {
                        app.CadInputQueue.SendCommand("topodotappv2 extractstripe");
                    }//end traffic striping


                    else if (e.Node.Tag.ToString().Contains("SYM"))
                    {
                        app.AttachCellLibrary(@"C:\Proj\supv8i\cells\survey\surveyphoto.cel");
                        switch (e.Node.Text)
                        {
                            case "Bollard":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification Bollard_PH");
                                break;
                            case "Electrical Guy Wire":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification EGW_PH");
                                break;
                            case "Fiber Optic Marker":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification FOM_PH");
                                break;
                            case "Filler Cap":
                                app.CadInputQueue.SendCommand("active cell FC_PH");
                                break;
                            case "Fire Hydrant":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification FH_PH");
                                break;
                            case "Flag Pole":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification FLG_PH");
                                break;
                            case "Gas Manhole":
                                app.CadInputQueue.SendCommand("active cell GMH_PH");
                                break;
                            case "Light Pole":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification LP_PH");
                                break;
                            case "Luminaire":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification LUMINAIRE_PH");
                                break;
                            case "Parking Meter":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification PAR_PH");
                                break;
                            case "Power Pole":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification PP_PH");
                                break;
                            case "Satellite Dish":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification SATDIS_PH");
                                break;
                            case "Shrub/Bush":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification SH_PH");
                                break;
                            case "Sign":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification ADSIGN_PH");
                                break;
                            case "Storm Sewer Manhole":
                                app.CadInputQueue.SendCommand("active cell SSMH_PH");
                                break;
                            case "Traffic Signal Pole":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification TSP_PH");
                                break;
                            case "Tree":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification TR_PH");
                                break;
                            case "Unknown Item":
                                app.CadInputQueue.SendCommand("topodotappv2 assetidentification UNK_PH");
                                break;
                            case "Unknown Manhole":
                                app.CadInputQueue.SendCommand("active cell MHUK_PH");
                                break;
                            case "Well":
                                app.CadInputQueue.SendCommand("active cell WELL_PH");
                                break;
                            default:
                                break;
                        }//end cell switch statement
                    }//end cell test
                    else
                    {
                        app.CadInputQueue.SendCommand("place smartline");
                    }//end place smartline for all other features
                }//end load tools automatically
            }//end tag Main check (isn't parent node)
            else
            {
                return;
            }//end if parent node double click don't do anything
        }//end MainWindow_NodeMouseDoubleClick function
    }//end FeatTable class
}//end LiDARFeatTable namespace
