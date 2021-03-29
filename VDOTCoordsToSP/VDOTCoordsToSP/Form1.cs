using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace VDOTCoordsToSP
{
    public partial class Form1 : Form
    {

        private string selFromSys;
        private string selToSys;
        private string outFile;
        private Decimal countyScaleFactor;
        private Decimal projectScaleFactor;


        public Form1()
        {
            InitializeComponent();
            messageBox.Clear();
            messageBox.AppendText("Welcome to Kevin's coordinate convertor.");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void browseButton_Click(object sender, EventArgs e)
        {
            textFileBox.Clear();
            browser.ShowDialog();
            textFileBox.AppendText(this.browser.FileName);
        }//end browseButton clicked


        private void outBrowseButton_Click(object sender, EventArgs e)
        {
            this.outFileBox.Clear();
            this.outBrowser.ShowDialog();
            this.outFileBox.AppendText(this.outBrowser.FileName);
            outFile = outBrowser.FileName;
        }//end outBrowseButton clicked


        private void fromCoordSys_Select(object sender, EventArgs e)
        {

            selFromSys = fromCoordSysSelect.Text;
            toCoordSysSelect.Text = "";
            this.toCoordSysSelect.Items.Clear();
            countyScaleFactorBox.Clear();
            projectScaleFactorBox.Clear();
            countyScaleFactorBox.Enabled = false;
            projectScaleFactorBox.Enabled = false;
            switch (selFromSys)
            {
                case "State Plane North":
                    this.toCoordSysSelect.Items.AddRange(new object[] { "VDOT Legacy North", "VDOT14" });
                    break;
                case "State Plane South":
                    this.toCoordSysSelect.Items.AddRange(new object[] { "VDOT Legacy South", "VDOT14" });
                    break;
                case "VDOT Legacy North":
                    this.toCoordSysSelect.Items.AddRange(new object[] { "State Plane North", "VDOT14" });
                    break;
                case "VDOT Legacy South":
                    this.toCoordSysSelect.Items.AddRange(new object[] { "State Plane South", "VDOT14" });
                    break;
                case "VDOT14":
                    this.toCoordSysSelect.Items.AddRange(new object[] { "State Plane North", "VDOT Legacy North", "State Plane South", "VDOT Legacy South" });
                    break;
                default:
                    break;
            }//end switch

        }//end from coordinate system selected


        private void toCoordSys_Select(object sender, EventArgs e)
        {

            selToSys = toCoordSysSelect.Text;
            if (selToSys == "VDOT14" || selFromSys == "VDOT14")
            {
                projectScaleFactorBox.Enabled = true;
            }
            else
            {
                projectScaleFactorBox.Enabled = false;
            }


            if (selToSys == "VDOT Legacy North" || selToSys == "VDOT Legacy South" || selFromSys == "VDOT Legacy North" || selFromSys == "VDOT Legacy South")
            {
                countyScaleFactorBox.Enabled = true;
            }

            else
            {
                countyScaleFactorBox.Enabled = false;
            }


        }//end to coord system selected


        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {

                if (countyScaleFactorBox.Enabled == true)
                {
                    countyScaleFactor = Convert.ToDecimal(countyScaleFactorBox.Text);
                }
                if (projectScaleFactorBox.Enabled == true)
                {
                    projectScaleFactor = Convert.ToDecimal(projectScaleFactorBox.Text);
                }
            }
            catch (FormatException f)
            {
                messageBox.Clear();
                messageBox.AppendText("Error: Please make sure your scale factor(s) is/are a number");
            }

            ReadFile();

        }//end okButton_Click



        private void ReadFile()
        {
            Console.WriteLine("In readfile");
            String[] line = System.IO.File.ReadAllLines(this.browser.FileName);

            line = line.Select(l => String.Join(" ", l.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))).ToArray();
            int rows = 0;
            int cols = 0;

            for (int i = 0; i < line.Length; i += 1)
            {
                if (line[i].Split(' ').Length > cols)
                {
                    cols = line[i].Split(' ').Length;
                }
                if (line[i].Split(' ').Length > 2)
                {
                    rows += 1;
                }
            }//end for loop to get number of rows

            string[,] sepLines = new string[rows, cols];
            int tempRow = 0;

            for (int i = 0; i < line.Length; i += 1)
            {
                Console.WriteLine(line[0]);

                //see if seperator is a space
                if (line[i].Split(' ').Length > 2)
                {
                    string[] tempArray = line[i].Split(' ');
                    for (int j = 0; j < cols; j += 1)
                    {
                        sepLines[tempRow, j] = tempArray[j];

                    }//end cols
                    tempRow += 1;
                }//end space seperator

                //see if seperator is a tab
                else if (line[i].Split('\t').Length > 2)
                {
                    string[] tempArray = line[i].Split('\t');
                    for (int j = 0; j < cols; j += 1)
                    {
                        sepLines[tempRow, j] = tempArray[j];

                    }//end cols
                    tempRow += 1;
                }//end tab seperator

                else
                {
                    continue;
                }//end else its an empty line

            }//end rows iterator

            if (selToSys == "State Plane South" || selToSys == "State Plane North")
            {
                ToSP(sepLines);
            }

            else if (selToSys == "VDOT Legacy North" || selToSys == "VDOT Legacy South")
            {
                ToVDOT(sepLines);
            }
            else if (selToSys == "VDOT14")
            {
                ToVDOT14(sepLines);
            }
            /* }//end try
             catch (IndexOutOfRangeException e)
             {
                 messageBox.Clear();
                 messageBox.AppendText("Error: Please make sure that all of the points have the same number of columns.");
             }//end index out of range exception
             catch (FileNotFoundException f)
             {
                 messageBox.Clear();
                 messageBox.AppendText("Error: The specified input file couldn't be found");
             }//end file not found exception
             catch (Exception e)
             {
                 messageBox.Clear();
                 messageBox.AppendText(e.ToString());
             }//end exception*/
        }//end read file


        private void ToSP(string[,] coords)
        {
            decimal xCoord;
            decimal yCoord;
            decimal zCoord;
            string name;

            StreamWriter sw = File.CreateText(outFile);

            //Name, X, Y, Z
            if (fileLayout.SelectedIndex == 0)
            {
                sw.WriteLine("{0,-7}{1,18}{2,18}{3,18}", "Name", "Easting", "Northing", "Elevation");
                switch (selToSys)
                {
                    case "State Plane North":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {
                            name = coords[i, 0];
                            xCoord = Convert.ToDecimal(coords[i, 1]);
                            yCoord = Convert.ToDecimal(coords[i, 2]);
                            zCoord = Convert.ToDecimal(coords[i, 3]);
                            ConvToSPNorth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                        }//end for in vdot legacy north

                        break;

                    case "State Plane South":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {
                            name = coords[i, 0];
                            xCoord = Convert.ToDecimal(coords[i, 1]);
                            yCoord = Convert.ToDecimal(coords[i, 2]);
                            zCoord = Convert.ToDecimal(coords[i, 3]);
                            ConvToSPSouth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                        }//end for in south

                        break;

                    default:
                        break;
                }//end convert to SP switch
            }//end Name X Y Z format

            //
            //begin Name, Y, X, Z format
            //
            else if (fileLayout.SelectedIndex == 1)
            {
                sw.WriteLine("{0,-7}{1,18}{2,18}{3,18}", "Name", "Easting", "Northing", "Elevation");
                switch (selToSys)
                {
                    case "State Plane North":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {
                            name = coords[i, 0];
                            xCoord = Convert.ToDecimal(coords[i, 2]);
                            yCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 3]);
                            ConvToSPNorth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                        }//end for in vdot legacy north

                        break;

                    case "State Plane South":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {
                            name = coords[i, 0];
                            xCoord = Convert.ToDecimal(coords[i, 2]);
                            yCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 3]);
                            ConvToSPSouth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                        }//end for in south

                        break;


                    default:
                        break;
                }//end convert to SP switch
            }//end Name, Y, X, Z format
            //
            //begin X, Y, Z format
            //
            else if (fileLayout.SelectedIndex == 2)
            {
                sw.WriteLine("{0,-7}{1,18}{2,18}", "Easting", "Northing", "Elevation");
                switch (selToSys)
                {
                    case "State Plane North":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {
                            xCoord = Convert.ToDecimal(coords[i, 0]);
                            yCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 2]);
                            ConvToSPNorth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", xCoord, yCoord, zCoord);
                        }//end for in vdot legacy north

                        break;

                    case "State Plane South":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {

                            xCoord = Convert.ToDecimal(coords[i, 0]);
                            yCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 2]);
                            ConvToSPSouth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", xCoord, yCoord, zCoord);
                        }//end for in south

                        break;

                    default:
                        break;
                }//end convert to SP switch
            }//end X Y Z format

            //
            //begin Y, X, Z format
            //
            else if (fileLayout.SelectedIndex == 3)
            {
                sw.WriteLine("{0,-7}{1,18}{2,18}", "Easting", "Northing", "Elevation");
                switch (selToSys)
                {
                    case "State Plane North":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {

                            yCoord = Convert.ToDecimal(coords[i, 0]);
                            xCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 2]);
                            ConvToSPNorth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7:0.0000}{1,18:0.0000}{2,15:0.0000}", xCoord, yCoord, zCoord);
                        }

                        break;

                    case "State Plane South":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {

                            yCoord = Convert.ToDecimal(coords[i, 0]);
                            xCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 2]);
                            ConvToSPSouth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7:0.0000}{1,18:0.0000}{2,15:0.0000}", xCoord, yCoord, zCoord);
                        }
                        break;

                    default:
                        break;
                }//end convert to SP switch
            }//end Y X Z format

            sw.Close();

        }//end ToSP method

        //
        //begin toVDOT method
        //
        private void ToVDOT(string[,] coords)
        {
            decimal xCoord;
            decimal yCoord;
            decimal zCoord;
            string name;

            StreamWriter sw = File.CreateText(outFile);

            //Name, X, Y, Z
            if (fileLayout.SelectedIndex == 0)
            {
                sw.WriteLine("{0,-7}{1,14}{2,18}{3,15}", "Name", "Easting", "Northing", "Elevation");
                switch (selToSys)
                {
                    case "VDOT Legacy North":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {
                            name = coords[i, 0];
                            xCoord = Convert.ToDecimal(coords[i, 1]);
                            yCoord = Convert.ToDecimal(coords[i, 2]);
                            zCoord = Convert.ToDecimal(coords[i, 3]);
                            ConvToLegacyNorth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                        }


                        break;

                    case "VDOT Legacy South":

                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {
                            name = coords[i, 0];
                            xCoord = Convert.ToDecimal(coords[i, 1]);
                            yCoord = Convert.ToDecimal(coords[i, 2]);
                            zCoord = Convert.ToDecimal(coords[i, 3]);
                            ConvToLegacySouth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                        }
                        break;

                    default:
                        break;
                }//end convert to SP switch
            }//end Name X Y Z format

            //
            //begin Name, Y, X, Z format
            //
            else if (fileLayout.SelectedIndex == 1)
            {
                sw.WriteLine("{0,-7}{1,18}{2,18}{3,18}", "Name", "Easting", "Northing", "Elevation");
                switch (selToSys)
                {
                    case "VDOT Legacy North":
                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {
                            name = coords[i, 0];
                            yCoord = Convert.ToDecimal(coords[i, 1]);
                            xCoord = Convert.ToDecimal(coords[i, 2]);
                            zCoord = Convert.ToDecimal(coords[i, 3]);
                            ConvToLegacyNorth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                        }//end for in north
                        break;

                    case "VDOT Legacy South":
                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {
                            name = coords[i, 0];
                            yCoord = Convert.ToDecimal(coords[i, 1]);
                            xCoord = Convert.ToDecimal(coords[i, 2]);
                            zCoord = Convert.ToDecimal(coords[i, 3]);
                            ConvToLegacySouth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                        }//end for in south
                        break;

                    default:
                        break;
                }//end convert to SP switch
            }//end Name, Y, X, Z format

            //
            //begin X, Y, Z format
            //
            else if (fileLayout.SelectedIndex == 2)
            {
                sw.WriteLine("{0,-7}{1,18}{2,15}", "Easting", "Northing", "Elevation");
                switch (selToSys)
                {
                    case "VDOT Legacy North":
                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {

                            xCoord = Convert.ToDecimal(coords[i, 0]);
                            yCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 2]);
                            ConvToLegacyNorth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7:0.0000}{1,18:0.0000}{2,15:0.0000}", xCoord, yCoord, zCoord);
                        }//end for in north
                        break;

                    case "VDOT Legacy South":
                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {

                            xCoord = Convert.ToDecimal(coords[i, 0]);
                            yCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 2]);
                            ConvToLegacySouth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7:0.0000}{1,18:0.0000}{2,15:0.0000}", xCoord, yCoord, zCoord);
                        }//end for in south
                        break;

                    default:
                        break;
                }//end convert to SP switch
            }//end X Y Z format
            //
            //begin Y, X, Z format
            //
            else if (fileLayout.SelectedIndex == 3)
            {
                sw.WriteLine("{0,-7}{1,18}{2,19}", "Easting", "Northing", "Elevation");
                switch (selToSys)
                {
                    case "VDOT Legacy North":
                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {

                            yCoord = Convert.ToDecimal(coords[i, 0]);
                            xCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 2]);
                            ConvToLegacyNorth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7:0.0000}{1,18:0.0000}{2,15:0.0000}", xCoord, yCoord, zCoord);
                        }//end for in north
                        break;

                    case "VDOT Legacy South":
                        for (int i = 0; i < coords.GetLength(0); i += 1)
                        {

                            yCoord = Convert.ToDecimal(coords[i, 0]);
                            xCoord = Convert.ToDecimal(coords[i, 1]);
                            zCoord = Convert.ToDecimal(coords[i, 2]);
                            ConvToLegacySouth(ref xCoord, ref yCoord);
                            sw.WriteLine("{0,-7:0.0000}{1,18:0.0000}{2,15:0.0000}", xCoord, yCoord, zCoord);
                        }//end for in south
                        break;

                    default:
                        break;
                }//end convert to VDOT switch
            }//end Y X Z format

            sw.Close();

        }//end ToVDOT

        /*
         *begin ToVDOT14 method (incomplete)
         *get conversion factors and fix math parts
         */

        private void ToVDOT14(string[,] coords)
        {
            decimal xCoord;
            decimal yCoord;
            decimal zCoord;
            string name;

            StreamWriter sw = File.CreateText(outFile);

            //Name, X, Y, Z

            if (fileLayout.SelectedIndex == 0)
            {
                sw.WriteLine("{0,-7}{1,18}{2,18}{3,18}", "Name", "Easting", "Northing", "Elevation");

                for (int i = 0; i < coords.GetLength(0); i += 1)
                {
                    name = coords[i, 0];
                    xCoord = Convert.ToDecimal(coords[i, 1]);
                    yCoord = Convert.ToDecimal(coords[i, 2]);
                    zCoord = Convert.ToDecimal(coords[i, 3]);
                    ConvToVDOT14(ref xCoord, ref yCoord);
                    sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                }

            }//end Name X Y Z format

            //
            //begin Name, Y, X, Z format
            //
            else if (fileLayout.SelectedIndex == 1)
            {
                sw.WriteLine("{0,-7}{1,18}{2,18}{3,18}", "Name", "Easting", "Northing", "Elevation");

                for (int i = 0; i < coords.GetLength(0); i += 1)
                {
                    name = coords[i, 0];
                    yCoord = Convert.ToDecimal(coords[i, 1]);
                    xCoord = Convert.ToDecimal(coords[i, 2]);
                    zCoord = Convert.ToDecimal(coords[i, 3]);
                    ConvToVDOT14(ref xCoord, ref yCoord);
                    sw.WriteLine("{0,-7}{1,14:0.0000}{2,18:0.0000}{3,15:0.0000}", name, xCoord, yCoord, zCoord);
                }//end for in north

            }//end Name, Y, X, Z format

            //
            //begin X, Y, Z format
            //
            else if (fileLayout.SelectedIndex == 2)
            {
                sw.WriteLine("{0,-7}{1,18}{2,19}", "Easting", "Northing", "Elevation");

                for (int i = 0; i < coords.GetLength(0); i += 1)
                {
                    xCoord = Convert.ToDecimal(coords[i, 0]);
                    yCoord = Convert.ToDecimal(coords[i, 1]);
                    zCoord = Convert.ToDecimal(coords[i, 2]);
                    ConvToVDOT14(ref xCoord, ref yCoord);
                    sw.WriteLine("{0,-7:0.0000}{1,18:0.0000}{2,15:0.0000}", xCoord, yCoord, zCoord);
                }

            }//end X Y Z format
            //
            //begin Y, X, Z format
            //
            else if (fileLayout.SelectedIndex == 3)
            {
                sw.WriteLine("{0,-7}{1,18}{2,19}", "Easting", "Northing", "Elevation");

                for (int i = 0; i < coords.GetLength(0); i += 1)
                {

                    yCoord = Convert.ToDecimal(coords[i, 0]);
                    xCoord = Convert.ToDecimal(coords[i, 1]);
                    zCoord = Convert.ToDecimal(coords[i, 2]);
                    ConvToVDOT14(ref xCoord, ref yCoord);
                    sw.WriteLine("{0,-7:0.0000}{1,18:0.0000}{2,15:0.0000}", xCoord, yCoord, zCoord);
                }//end for in north

            }//end Y X Z format

            sw.Close();

        }//end ToVDOT14


        private void ConvToSPNorth(ref Decimal xCoord, ref Decimal yCoord)
        {
            Console.WriteLine("In toSPNorth");
            if (selFromSys == "VDOT Legacy North")
            {
                xCoord = xCoord / countyScaleFactor + Convert.ToDecimal(8202083.33325);
                yCoord = yCoord / countyScaleFactor + Convert.ToDecimal(6561666.6666);
            }

            else if (selFromSys == "VDOT14")
            {
                xCoord = xCoord / projectScaleFactor;
                yCoord = yCoord / projectScaleFactor;
            }

        }//end  ConvToSPNorth


        private void ConvToSPSouth(ref Decimal xCoord, ref Decimal yCoord)
        {

            if (selFromSys == "VDOT Legacy South")
            {
                xCoord = xCoord / countyScaleFactor + Convert.ToDecimal(8202083.33325);
                yCoord = yCoord / countyScaleFactor + Convert.ToDecimal(3280833.3333);
            }

            else if (selFromSys == "VDOT14")
            {
                xCoord = xCoord / projectScaleFactor;
                yCoord = yCoord / projectScaleFactor;
            }

        }//end  ConvToSPSouth




        private void ConvToLegacyNorth(ref Decimal xCoord, ref Decimal yCoord)
        {
            if (selFromSys == "VDOT14")
            {
                ConvToSPNorth(ref xCoord, ref yCoord);
                xCoord = (xCoord - Convert.ToDecimal(8202083.33325)) * countyScaleFactor;
                yCoord = (yCoord - Convert.ToDecimal(6561666.6666)) * countyScaleFactor;
            }//end from VDOT 14


            else if (selFromSys == "State Plane North")
            {
                xCoord = (xCoord - Convert.ToDecimal(8202083.33325)) * countyScaleFactor;
                yCoord = (yCoord - Convert.ToDecimal(6561666.6666)) * countyScaleFactor;
            }//end from State Plane North
        }//end ConvToLegacyNorth


        private void ConvToLegacySouth(ref Decimal xCoord, ref Decimal yCoord)
        {
            if (selFromSys == "VDOT14")
            {
                ConvToSPSouth(ref xCoord, ref yCoord);
                xCoord = (xCoord - Convert.ToDecimal(8202083.33325)) * countyScaleFactor;
                yCoord = (yCoord - Convert.ToDecimal(3280833.3333)) * countyScaleFactor;
            }//end from VDOT 14


            else if (selFromSys == "State Plane South")
            {
                xCoord = (xCoord - Convert.ToDecimal(8202083.33325)) * countyScaleFactor;
                yCoord = (yCoord - Convert.ToDecimal(3280833.3333)) * countyScaleFactor;
            }//end from State Plane North
        }//end ConvToLegacySouth




        private void ConvToVDOT14(ref Decimal xCoord, ref Decimal yCoord)
        {
            if (selFromSys == "State Plane North")
            {
                xCoord = xCoord * projectScaleFactor;
                yCoord = yCoord * projectScaleFactor;
            }// end SP North

            else if (selFromSys == "State Plane South")
            {
                xCoord = xCoord * projectScaleFactor;
                yCoord = yCoord * projectScaleFactor;
            }//end SP South

            else if (selFromSys == "VDOT Legacy North")
            {
                ConvToSPNorth(ref xCoord, ref yCoord);
                xCoord = xCoord * projectScaleFactor;
                yCoord = yCoord * projectScaleFactor;
            }//end Legacy North

            else if (selFromSys == "VDOT Legacy South")
            {
                ConvToSPSouth(ref xCoord, ref yCoord);
                xCoord = xCoord * projectScaleFactor;
                yCoord = yCoord * projectScaleFactor;
            }//end Legacy South
        }//end ConvToVDOT14



        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }//end class
}//end namespace
