namespace CryptoNS
{
    partial class Crypto
    {
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

        #region Windows Form Designer Code

        /// <summary>
        /// Required method for Designer support
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Crypto));
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.lbFilePath = new System.Windows.Forms.Label();
            this.btBrowseFile = new System.Windows.Forms.Button();
            this.rbDecode = new System.Windows.Forms.RadioButton();
            this.rbEncode = new System.Windows.Forms.RadioButton();
            this.lbPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btStart = new System.Windows.Forms.Button();
            this.cbShowPassword = new System.Windows.Forms.CheckBox();
            this.pbLoading = new System.Windows.Forms.ProgressBar();
            this.lbProgress = new System.Windows.Forms.Label();
            this.lbFileSize = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.tbFolderPath = new System.Windows.Forms.TextBox();
            this.btBrowseFolder = new System.Windows.Forms.Button();
            this.lbFolderPath = new System.Windows.Forms.Label();
            this.lbMode = new System.Windows.Forms.Label();
            this.cbDeepScanMode = new System.Windows.Forms.CheckBox();
            this.picTitle = new System.Windows.Forms.PictureBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picMaxMin = new System.Windows.Forms.PictureBox();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.picHelp = new System.Windows.Forms.PictureBox();
            this.lbDynamicTitle = new System.Windows.Forms.Label();
            this.btClearFilePath = new System.Windows.Forms.Button();
            this.btClearFolderPath = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMaxMin)).BeginInit();
            this.pnlTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // tbFilePath
            // 
            this.tbFilePath.AllowDrop = true;
            this.tbFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(160)))));
            resources.ApplyResources(this.tbFilePath, "tbFilePath");
            this.tbFilePath.ForeColor = System.Drawing.Color.Black;
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.TextChanged += new System.EventHandler(this.tbFilePath_TextChanged);
            this.tbFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbFilePath_DragDrop);
            this.tbFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbFilePath_DragEnter);
            this.tbFilePath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFilePath_KeyDown);
            // 
            // lbFilePath
            // 
            resources.ApplyResources(this.lbFilePath, "lbFilePath");
            this.lbFilePath.BackColor = System.Drawing.Color.Transparent;
            this.lbFilePath.ForeColor = System.Drawing.Color.Black;
            this.lbFilePath.Name = "lbFilePath";
            // 
            // btBrowseFile
            // 
            this.btBrowseFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(160)))));
            resources.ApplyResources(this.btBrowseFile, "btBrowseFile");
            this.btBrowseFile.Name = "btBrowseFile";
            this.btBrowseFile.UseVisualStyleBackColor = false;
            this.btBrowseFile.Click += new System.EventHandler(this.btBrowseFile_Click);
            // 
            // rbDecode
            // 
            resources.ApplyResources(this.rbDecode, "rbDecode");
            this.rbDecode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(220)))));
            this.rbDecode.Name = "rbDecode";
            this.rbDecode.UseVisualStyleBackColor = true;
            this.rbDecode.CheckedChanged += new System.EventHandler(this.rbDecode_CheckedChanged);
            // 
            // rbEncode
            // 
            resources.ApplyResources(this.rbEncode, "rbEncode");
            this.rbEncode.Checked = true;
            this.rbEncode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(220)))));
            this.rbEncode.Name = "rbEncode";
            this.rbEncode.TabStop = true;
            this.rbEncode.UseVisualStyleBackColor = true;
            this.rbEncode.CheckedChanged += new System.EventHandler(this.rbEncode_CheckedChanged);
            // 
            // lbPassword
            // 
            resources.ApplyResources(this.lbPassword, "lbPassword");
            this.lbPassword.ForeColor = System.Drawing.Color.Black;
            this.lbPassword.Name = "lbPassword";
            // 
            // tbPassword
            // 
            this.tbPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(160)))));
            resources.ApplyResources(this.tbPassword, "tbPassword");
            this.tbPassword.ForeColor = System.Drawing.Color.Black;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyDown);
            // 
            // btStart
            // 
            this.btStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(160)))));
            this.btStart.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.btStart, "btStart");
            this.btStart.Name = "btStart";
            this.btStart.UseVisualStyleBackColor = false;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // cbShowPassword
            // 
            resources.ApplyResources(this.cbShowPassword, "cbShowPassword");
            this.cbShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(220)))));
            this.cbShowPassword.Name = "cbShowPassword";
            this.cbShowPassword.UseVisualStyleBackColor = true;
            this.cbShowPassword.CheckedChanged += new System.EventHandler(this.cbShowPassword_CheckedChanged);
            // 
            // pbLoading
            // 
            this.pbLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(156)))), ((int)(((byte)(195)))));
            this.pbLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(156)))), ((int)(((byte)(195)))));
            resources.ApplyResources(this.pbLoading, "pbLoading");
            this.pbLoading.Maximum = 10;
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lbProgress
            // 
            resources.ApplyResources(this.lbProgress, "lbProgress");
            this.lbProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(220)))));
            this.lbProgress.Name = "lbProgress";
            // 
            // lbFileSize
            // 
            resources.ApplyResources(this.lbFileSize, "lbFileSize");
            this.lbFileSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(220)))));
            this.lbFileSize.Name = "lbFileSize";
            // 
            // lbStatus
            // 
            resources.ApplyResources(this.lbStatus, "lbStatus");
            this.lbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(66)))), ((int)(((byte)(75)))));
            this.lbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(220)))));
            this.lbStatus.Name = "lbStatus";
            // 
            // tbFolderPath
            // 
            this.tbFolderPath.AllowDrop = true;
            this.tbFolderPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(160)))));
            resources.ApplyResources(this.tbFolderPath, "tbFolderPath");
            this.tbFolderPath.ForeColor = System.Drawing.Color.Black;
            this.tbFolderPath.Name = "tbFolderPath";
            this.tbFolderPath.TextChanged += new System.EventHandler(this.tbFolderPath_TextChanged);
            this.tbFolderPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbFolderPath_DragDrop);
            this.tbFolderPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbFolderPath_DragEnter);
            this.tbFolderPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFolderPath_KeyDown);
            // 
            // btBrowseFolder
            // 
            this.btBrowseFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(160)))));
            resources.ApplyResources(this.btBrowseFolder, "btBrowseFolder");
            this.btBrowseFolder.Name = "btBrowseFolder";
            this.btBrowseFolder.UseVisualStyleBackColor = false;
            this.btBrowseFolder.Click += new System.EventHandler(this.btBrowseFolder_Click);
            // 
            // lbFolderPath
            // 
            resources.ApplyResources(this.lbFolderPath, "lbFolderPath");
            this.lbFolderPath.ForeColor = System.Drawing.Color.Black;
            this.lbFolderPath.Name = "lbFolderPath";
            // 
            // lbMode
            // 
            resources.ApplyResources(this.lbMode, "lbMode");
            this.lbMode.ForeColor = System.Drawing.Color.Black;
            this.lbMode.Name = "lbMode";
            // 
            // cbDeepScanMode
            // 
            resources.ApplyResources(this.cbDeepScanMode, "cbDeepScanMode");
            this.cbDeepScanMode.ForeColor = System.Drawing.Color.Black;
            this.cbDeepScanMode.Name = "cbDeepScanMode";
            this.cbDeepScanMode.UseVisualStyleBackColor = true;
            this.cbDeepScanMode.CheckedChanged += new System.EventHandler(this.cbDeepScanMode_CheckedChanged);
            // 
            // picTitle
            // 
            resources.ApplyResources(this.picTitle, "picTitle");
            this.picTitle.Image = global::CryptoNS.Properties.Resources.title;
            this.picTitle.InitialImage = global::CryptoNS.Properties.Resources.title;
            this.picTitle.Name = "picTitle";
            this.picTitle.TabStop = false;
            // 
            // picClose
            // 
            this.picClose.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.picClose, "picClose");
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = global::CryptoNS.Properties.Resources.ButtonClose;
            this.picClose.InitialImage = global::CryptoNS.Properties.Resources.ButtonClose;
            this.picClose.Name = "picClose";
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.Crypto_Close);
            // 
            // picMaxMin
            // 
            this.picMaxMin.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.picMaxMin, "picMaxMin");
            this.picMaxMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMaxMin.Image = global::CryptoNS.Properties.Resources.buttonMinimize;
            this.picMaxMin.InitialImage = global::CryptoNS.Properties.Resources.buttonMinimize;
            this.picMaxMin.Name = "picMaxMin";
            this.picMaxMin.TabStop = false;
            this.picMaxMin.Click += new System.EventHandler(this.Crypto_Minimaze);
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(52)))), ((int)(((byte)(60)))));
            resources.ApplyResources(this.pnlTopBar, "pnlTopBar");
            this.pnlTopBar.Controls.Add(this.picHelp);
            this.pnlTopBar.Controls.Add(this.lbDynamicTitle);
            this.pnlTopBar.Controls.Add(this.picMaxMin);
            this.pnlTopBar.Controls.Add(this.picClose);
            this.pnlTopBar.ForeColor = System.Drawing.Color.Transparent;
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Crypto_Move);
            // 
            // picHelp
            // 
            this.picHelp.BackColor = System.Drawing.Color.Transparent;
            this.picHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHelp.Image = global::CryptoNS.Properties.Resources.buttonHelp;
            this.picHelp.InitialImage = global::CryptoNS.Properties.Resources.buttonHelp;
            resources.ApplyResources(this.picHelp, "picHelp");
            this.picHelp.Name = "picHelp";
            this.picHelp.TabStop = false;
            this.picHelp.Click += new System.EventHandler(this.Crypto_Help);
            // 
            // lbDynamicTitle
            // 
            resources.ApplyResources(this.lbDynamicTitle, "lbDynamicTitle");
            this.lbDynamicTitle.ForeColor = System.Drawing.Color.White;
            this.lbDynamicTitle.Name = "lbDynamicTitle";
            this.lbDynamicTitle.Click += new System.EventHandler(this.Crypto_UpdateTitle);
            // 
            // btClearFilePath
            // 
            this.btClearFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(160)))));
            resources.ApplyResources(this.btClearFilePath, "btClearFilePath");
            this.btClearFilePath.Name = "btClearFilePath";
            this.btClearFilePath.UseVisualStyleBackColor = false;
            this.btClearFilePath.Click += new System.EventHandler(this.btClearFilePath_Click);
            // 
            // btClearFolderPath
            // 
            this.btClearFolderPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(157)))), ((int)(((byte)(160)))));
            resources.ApplyResources(this.btClearFolderPath, "btClearFolderPath");
            this.btClearFolderPath.Name = "btClearFolderPath";
            this.btClearFolderPath.UseVisualStyleBackColor = false;
            this.btClearFolderPath.Click += new System.EventHandler(this.btClearFolderPath_Click);
            // 
            // Crypto
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(66)))), ((int)(((byte)(75)))));
            this.Controls.Add(this.btClearFolderPath);
            this.Controls.Add(this.btClearFilePath);
            this.Controls.Add(this.pnlTopBar);
            this.Controls.Add(this.picTitle);
            this.Controls.Add(this.cbDeepScanMode);
            this.Controls.Add(this.lbMode);
            this.Controls.Add(this.lbFolderPath);
            this.Controls.Add(this.btBrowseFolder);
            this.Controls.Add(this.tbFolderPath);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.lbFileSize);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.pbLoading);
            this.Controls.Add(this.cbShowPassword);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.rbEncode);
            this.Controls.Add(this.rbDecode);
            this.Controls.Add(this.btBrowseFile);
            this.Controls.Add(this.lbFilePath);
            this.Controls.Add(this.tbFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Crypto";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Crypto_DrawBorder);
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMaxMin)).EndInit();
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Label lbFilePath;
        private System.Windows.Forms.Button btBrowseFile;
        private System.Windows.Forms.RadioButton rbDecode;
        private System.Windows.Forms.RadioButton rbEncode;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.CheckBox cbShowPassword;
        private System.Windows.Forms.ProgressBar pbLoading;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.Label lbFileSize;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.TextBox tbFolderPath;
        private System.Windows.Forms.Button btBrowseFolder;
        private System.Windows.Forms.Label lbFolderPath;
        private System.Windows.Forms.Label lbMode;
        private System.Windows.Forms.CheckBox cbDeepScanMode;
        private System.Windows.Forms.PictureBox picTitle;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.PictureBox picMaxMin;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lbDynamicTitle;
        private System.Windows.Forms.PictureBox picHelp;
        private System.Windows.Forms.Button btClearFilePath;
        private System.Windows.Forms.Button btClearFolderPath;
    }
}

