using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using PWWrappers;
using System.Runtime.InteropServices;
using ArcGIS.Desktop.Internal.Mapping.PropertyPages;
using System.Collections.ObjectModel;

namespace ArcProToPW
{
    internal class BaseClass : Module
        
    {
        internal static bool loggedIn = false;
        internal static bool init = false;
        internal static Dictionary<string, Doc> pwShpFiles = new Dictionary<string, Doc>();
        private static BaseClass _this = null;

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static BaseClass Current
        {
            get
            {
                return _this ?? (_this = (BaseClass)FrameworkApplication.FindModule("ArcProToPW_Module"));
            }
        }

        #region Overrides
        /// <summary>
        /// Called by Framework when ArcGIS Pro is closing
        /// </summary>
        /// <returns>False to prevent Pro from closing, otherwise True</returns>
        
        protected override bool CanUnload()
        {

            //TODO - add your business logic
            //return false to ~cancel~ Application close
            return true;
        }
        
        

        #endregion Overrides

    }

    internal class OpenPWFile : ArcGIS.Desktop.Framework.Contracts.Button
    {
        protected override void OnClick()
        {
            if (!BaseClass.init)
            {
                 BaseClass.init = Wrappers.aaApi_Initialize(0);
            }

            if (!BaseClass.loggedIn)
            {
                string title = "ProjectWise Login";
                StringBuilder dataSource = new StringBuilder("VDOT ProjectWise");
                string user = "";
                string password = "";
                IntPtr login = Wrappers.aaApi_LoginDlgExt(IntPtr.Zero, title, 0, dataSource, dataSource.Length, user, password, null);
                if(login.ToInt32() == 1)
                {
                    BaseClass.loggedIn = true;
                }
            }
            
            IntPtr winHandle = IntPtr.Zero;
            string openFileTitle = "Open File";
            StringBuilder fileName = new StringBuilder("\0");
            int docID = 0;
            int prjIDRet = 0;

            //IntPtr openFile = Wrappers.aaApi_DocumentExtendedSelectDlg(winHandle, openFileTitle, prjID, appPtr, 0, count, ref docInfo, "", IntPtr.Zero);
            
            //**Puts all files from folder into buffer**
            IntPtr openFile = Wrappers.aaApi_SelectDocumentDlg(winHandle, openFileTitle, 0, ref prjIDRet, ref docID);
            if (openFile.ToInt32() == 1)
            {
                int docSelect = Wrappers.aaApi_SelectDocument(prjIDRet, docID);
                string docName = Wrappers.aaApi_GetDocumentStringProperty(Wrappers.DocumentProperty.Name, 0);
                docName = docName.Replace("shp", "");
                string searchName = docName + "%";
                int shpFilesCnt = Wrappers.aaApi_SelectDocumentsByNameProp(prjIDRet, null, searchName, null, null);
                string currentShp = "";

                for (int i = 0; i < shpFilesCnt; i += 1)
                {
                    int docIDShp = Wrappers.aaApi_GetDocumentNumericProperty(Wrappers.DocumentProperty.ID, i);
                    string docNameShp = Wrappers.aaApi_GetDocumentStringProperty(Wrappers.DocumentProperty.Name, i);

                    StringBuilder fileNameShp = new StringBuilder(1024);
                    bool checkOut = Wrappers.aaApi_CheckOutDocument(prjIDRet, docIDShp, null, fileNameShp, 1024);
                    if (docNameShp.EndsWith(".shp"))
                    {
                        currentShp = docNameShp;
                    }
                    Doc file = new Doc();
                    file.DocID = docIDShp;

                    file.Docpath = fileNameShp.ToString();
                    file.PrjID = prjIDRet;

                    BaseClass.pwShpFiles.Add(docNameShp, file);


                }


                if (BaseClass.pwShpFiles.Count != 0)
                {
                    foreach (string key in BaseClass.pwShpFiles.Keys.ToList())
                    {
                        if (key == currentShp)
                        {
                            AddLayer(BaseClass.pwShpFiles[key].Docpath);
                        }
                    }


                }

                else
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"No Files to Add");
                }
            }

            else if(openFile.ToInt32() == 3){
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Error opening Open Dialog Window");
            }
        }

        async void AddLayer(string filePath)
        {
            await QueuedTask.Run(() =>
            {
                
                Map map = MapView.Active.Map;
                
                Layer lyr = LayerFactory.Instance.CreateFeatureLayer(new Uri(filePath), map);

            });
            
        }

        
    }

    internal class CheckInPWFiles : Button
    {
        protected override void OnClick()
        {
            var selected = MapView.Active.GetSelectedLayers();
            foreach(Layer l in selected)
            {
                
                foreach (string key in BaseClass.pwShpFiles.Keys.ToList())
                {
                    string baseName = key.Remove(key.IndexOf('.'));


                    if (l.Name == baseName)
                    {
                        
                        //ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"match found");
                        Wrappers.aaApi_CheckInDocument(BaseClass.pwShpFiles[key].PrjID, BaseClass.pwShpFiles[key].DocID);
                        BaseClass.pwShpFiles.Remove(key);
                        
                    }
                }
            }
        }
    }

    internal class PWSync : Button
    {
        protected override void OnClick()
        {
            var selected = MapView.Active.GetSelectedLayers();
            foreach(Layer l in selected)
            {
                foreach(string key in BaseClass.pwShpFiles.Keys.ToList())
                {
                    string baseName = key.Remove(key.IndexOf('.'));
                    if(l.Name == baseName)
                    {
                        
                    }
                }
            }
        }
    }
    internal class Doc
    {
        //public string DocName { get; set; }
        public int DocID { get; set; }
        public string Docpath { get; set; }
        public int PrjID { get; set; }
    }

}
