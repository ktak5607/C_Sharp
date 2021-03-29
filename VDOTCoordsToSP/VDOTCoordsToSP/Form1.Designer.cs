namespace VDOTCoordsToSP
{
    partial class Form1
    {

        private System.Windows.Forms.ComboBox toCoordSysSelect;
        private System.Windows.Forms.ComboBox fromCoordSysSelect;
        private System.Windows.Forms.TextBox countyScaleFactorBox;
        private System.Windows.Forms.TextBox textFileBox;
        private System.Windows.Forms.TextBox outFileBox;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button outBrowseButton;
        private System.Windows.Forms.OpenFileDialog browser;
        private System.Windows.Forms.SaveFileDialog outBrowser;
        private System.Windows.Forms.Label countyScaleFactorLabel;
        private System.Windows.Forms.Label fromCoordSysLabel;
        private System.Windows.Forms.Label toCoordSysLabel;
        private System.Windows.Forms.Label selectFileLabel;
        private System.Windows.Forms.Label outFileLabel;
        private System.Windows.Forms.ComboBox fileLayout;
        private System.Windows.Forms.Label fileLayoutLabel;

        

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toCoordSysSelect = new System.Windows.Forms.ComboBox();
            this.fromCoordSysSelect = new System.Windows.Forms.ComboBox();
            this.textFileBox = new System.Windows.Forms.TextBox();
            this.outFileBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.countyScaleFactorBox = new System.Windows.Forms.TextBox();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.outBrowseButton = new System.Windows.Forms.Button();
            this.browser = new System.Windows.Forms.OpenFileDialog();
            this.outBrowser = new System.Windows.Forms.SaveFileDialog();
            this.toCoordSysLabel = new System.Windows.Forms.Label();
            this.fromCoordSysLabel = new System.Windows.Forms.Label();
            this.countyScaleFactorLabel = new System.Windows.Forms.Label();
            this.selectFileLabel = new System.Windows.Forms.Label();
            this.outFileLabel = new System.Windows.Forms.Label();
            this.fileLayout = new System.Windows.Forms.ComboBox();
            this.fileLayoutLabel = new System.Windows.Forms.Label();
            this.projectScaleFactorBox = new System.Windows.Forms.TextBox();
            this.projectScaleFactorLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toCoordSysSelect
            // 
            this.toCoordSysSelect.FormattingEnabled = true;
            this.toCoordSysSelect.Location = new System.Drawing.Point(277, 25);
            this.toCoordSysSelect.Name = "toCoordSysSelect";
            this.toCoordSysSelect.Size = new System.Drawing.Size(194, 21);
            this.toCoordSysSelect.TabIndex = 0;
            this.toCoordSysSelect.SelectedIndexChanged += new System.EventHandler(this.toCoordSys_Select);
            // 
            // fromCoordSysSelect
            // 
            this.fromCoordSysSelect.FormattingEnabled = true;
            this.fromCoordSysSelect.Items.AddRange(new object[] {
            "State Plane North",
            "State Plane South",
            "VDOT Legacy North",
            "VDOT Legacy South",
            "VDOT14"});
            this.fromCoordSysSelect.Location = new System.Drawing.Point(12, 25);
            this.fromCoordSysSelect.Name = "fromCoordSysSelect";
            this.fromCoordSysSelect.Size = new System.Drawing.Size(201, 21);
            this.fromCoordSysSelect.TabIndex = 2;
            this.fromCoordSysSelect.SelectedIndexChanged += new System.EventHandler(this.fromCoordSys_Select);
            // 
            // textFileBox
            // 
            this.textFileBox.Location = new System.Drawing.Point(88, 236);
            this.textFileBox.Name = "textFileBox";
            this.textFileBox.Size = new System.Drawing.Size(194, 20);
            this.textFileBox.TabIndex = 3;
            // 
            // outFileBox
            // 
            this.outFileBox.Location = new System.Drawing.Point(116, 275);
            this.outFileBox.Name = "outFileBox";
            this.outFileBox.Size = new System.Drawing.Size(194, 20);
            this.outFileBox.TabIndex = 4;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 326);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(66, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Ok";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // countyScaleFactorBox
            // 
            this.countyScaleFactorBox.Enabled = false;
            this.countyScaleFactorBox.Location = new System.Drawing.Point(138, 100);
            this.countyScaleFactorBox.Name = "countyScaleFactorBox";
            this.countyScaleFactorBox.Size = new System.Drawing.Size(100, 20);
            this.countyScaleFactorBox.TabIndex = 0;
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(487, 9);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageBox.Size = new System.Drawing.Size(205, 167);
            this.messageBox.TabIndex = 3;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(300, 236);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(28, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // outBrowseButton
            // 
            this.outBrowseButton.Location = new System.Drawing.Point(318, 272);
            this.outBrowseButton.Name = "outBrowseButton";
            this.outBrowseButton.Size = new System.Drawing.Size(26, 23);
            this.outBrowseButton.TabIndex = 4;
            this.outBrowseButton.Click += new System.EventHandler(this.outBrowseButton_Click);
            // 
            // browser
            // 
            this.browser.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            //this.browser.FileOk += new System.ComponentModel.CancelEventHandler(this.browser_FileOk);
            // 
            // outBrowser
            // 
            this.outBrowser.Filter = "Text Files (.txt)|*.txt";
            //this.outBrowser.FileOk += new System.ComponentModel.CancelEventHandler(this.outBrowser_FileOk);
            // 
            // toCoordSysLabel
            // 
            this.toCoordSysLabel.Location = new System.Drawing.Point(274, 9);
            this.toCoordSysLabel.Name = "toCoordSysLabel";
            this.toCoordSysLabel.Size = new System.Drawing.Size(119, 13);
            this.toCoordSysLabel.TabIndex = 0;
            this.toCoordSysLabel.Text = "To Coordinate System";
            // 
            // fromCoordSysLabel
            // 
            this.fromCoordSysLabel.Location = new System.Drawing.Point(51, 9);
            this.fromCoordSysLabel.Name = "fromCoordSysLabel";
            this.fromCoordSysLabel.Size = new System.Drawing.Size(137, 13);
            this.fromCoordSysLabel.TabIndex = 0;
            this.fromCoordSysLabel.Text = "From Coordinate System";
            // 
            // countyScaleFactorLabel
            // 
            this.countyScaleFactorLabel.Location = new System.Drawing.Point(9, 100);
            this.countyScaleFactorLabel.Name = "countyScaleFactorLabel";
            this.countyScaleFactorLabel.Size = new System.Drawing.Size(103, 23);
            this.countyScaleFactorLabel.TabIndex = 0;
            this.countyScaleFactorLabel.Text = "County Scale Factor";
            // 
            // selectFileLabel
            // 
            this.selectFileLabel.Location = new System.Drawing.Point(9, 239);
            this.selectFileLabel.Name = "selectFileLabel";
            this.selectFileLabel.Size = new System.Drawing.Size(69, 17);
            this.selectFileLabel.TabIndex = 4;
            this.selectFileLabel.Text = "Select Input File";
            // 
            // outFileLabel
            // 
            this.outFileLabel.Location = new System.Drawing.Point(9, 278);
            this.outFileLabel.Name = "outFileLabel";
            this.outFileLabel.Size = new System.Drawing.Size(101, 23);
            this.outFileLabel.TabIndex = 5;
            this.outFileLabel.Text = "Select Output File";
            // 
            // fileLayout
            // 
            this.fileLayout.Items.AddRange(new object[] {
            "Name, X, Y, Z",
            "Name, Y, X, Z",
            "X, Y, Z",
            "Y, X, Z"});
            this.fileLayout.Location = new System.Drawing.Point(138, 165);
            this.fileLayout.Name = "fileLayout";
            this.fileLayout.Size = new System.Drawing.Size(121, 21);
            this.fileLayout.TabIndex = 5;
            // 
            // fileLayoutLabel
            // 
            this.fileLayoutLabel.Location = new System.Drawing.Point(12, 165);
            this.fileLayoutLabel.Name = "fileLayoutLabel";
            this.fileLayoutLabel.Size = new System.Drawing.Size(100, 20);
            this.fileLayoutLabel.TabIndex = 6;
            this.fileLayoutLabel.Text = "Select File Layout";
            // 
            // projectScaleFactorBox
            // 
            this.projectScaleFactorBox.Enabled = false;
            this.projectScaleFactorBox.Location = new System.Drawing.Point(371, 97);
            this.projectScaleFactorBox.Name = "projectScaleFactorBox";
            this.projectScaleFactorBox.Size = new System.Drawing.Size(100, 20);
            this.projectScaleFactorBox.TabIndex = 7;
            // 
            // projectScaleFactorLabel
            // 
            this.projectScaleFactorLabel.AutoSize = true;
            this.projectScaleFactorLabel.Location = new System.Drawing.Point(262, 100);
            this.projectScaleFactorLabel.Name = "projectScaleFactorLabel";
            this.projectScaleFactorLabel.Size = new System.Drawing.Size(103, 13);
            this.projectScaleFactorLabel.TabIndex = 8;
            this.projectScaleFactorLabel.Text = "Project Scale Factor";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(269, 326);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 9;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 416);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.projectScaleFactorLabel);
            this.Controls.Add(this.projectScaleFactorBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.toCoordSysSelect);
            this.Controls.Add(this.fromCoordSysSelect);
            this.Controls.Add(this.countyScaleFactorBox);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.outBrowseButton);
            this.Controls.Add(this.textFileBox);
            this.Controls.Add(this.outFileBox);
            this.Controls.Add(this.toCoordSysLabel);
            this.Controls.Add(this.fromCoordSysLabel);
            this.Controls.Add(this.countyScaleFactorLabel);
            this.Controls.Add(this.selectFileLabel);
            this.Controls.Add(this.outFileLabel);
            this.Controls.Add(this.fileLayout);
            this.Controls.Add(this.fileLayoutLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "State Plane Coordinate Converter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

         }
       
        #endregion

        private System.Windows.Forms.TextBox projectScaleFactorBox;
        private System.Windows.Forms.Label projectScaleFactorLabel;
        private System.Windows.Forms.Button closeButton;

    }//end class
}

