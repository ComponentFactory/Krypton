namespace OutlookMockup
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Deleted Items");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Drafts");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Family");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Friends");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Work");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Inbox", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Outbox");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Sent Items");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Drafts");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Family");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.kryptonPanelMain = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonSplitContainerMain = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonNavigatorMain = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.buttonSpecExpandCollapse = new ComponentFactory.Krypton.Navigator.ButtonSpecNavigator();
            this.kryptonPageMail = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.treeViewMailFolders = new System.Windows.Forms.TreeView();
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.panelFoldersSep = new System.Windows.Forms.Panel();
            this.kryptonHeaderFolders = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.treeViewMailFavs = new System.Windows.Forms.TreeView();
            this.panelFavoriteSep = new System.Windows.Forms.Panel();
            this.kryptonHeaderFavorites = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonPageCalendar = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.kryptonPageNotes = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.radioFriends = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioFamily = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioProject = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonSplitContainerDetails = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonNavigatorDetails = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.kryptonPageMailDetails = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonDataGridView1 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgReceived = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonPageCalendarDetails = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.kryptonPageNotesDetails = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.listViewNotes = new System.Windows.Forms.ListView();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.kryptonButtonGroup = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroupInner = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.radioSparkle = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioSystem = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioOffice2003 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioOffice2007Black = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioOffice2007Silver = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioOffice2007Blue = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.dataSet = new System.Data.DataSet();
            this.dtDeletedItems = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dtDrafts = new System.Data.DataTable();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dtFamily = new System.Data.DataTable();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dtOutbox = new System.Data.DataTable();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dtSentItems = new System.Data.DataTable();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn19 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dtFriends = new System.Data.DataTable();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            this.dtWork = new System.Data.DataTable();
            this.dataColumn25 = new System.Data.DataColumn();
            this.dataColumn26 = new System.Data.DataColumn();
            this.dataColumn27 = new System.Data.DataColumn();
            this.dataColumn28 = new System.Data.DataColumn();
            this.dtInbox = new System.Data.DataTable();
            this.dataColumn29 = new System.Data.DataColumn();
            this.dataColumn30 = new System.Data.DataColumn();
            this.dataColumn31 = new System.Data.DataColumn();
            this.dataColumn32 = new System.Data.DataColumn();
            this.radioOffice2010Black = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioOffice2010Silver = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.radioOffice2010Blue = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel1)).BeginInit();
            this.kryptonSplitContainerMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel2)).BeginInit();
            this.kryptonSplitContainerMain.Panel2.SuspendLayout();
            this.kryptonSplitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigatorMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageMail)).BeginInit();
            this.kryptonPageMail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageCalendar)).BeginInit();
            this.kryptonPageCalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageNotes)).BeginInit();
            this.kryptonPageNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerDetails.Panel1)).BeginInit();
            this.kryptonSplitContainerDetails.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerDetails.Panel2)).BeginInit();
            this.kryptonSplitContainerDetails.Panel2.SuspendLayout();
            this.kryptonSplitContainerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigatorDetails)).BeginInit();
            this.kryptonNavigatorDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageMailDetails)).BeginInit();
            this.kryptonPageMailDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageCalendarDetails)).BeginInit();
            this.kryptonPageCalendarDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageNotesDetails)).BeginInit();
            this.kryptonPageNotesDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonButtonGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonButtonGroup.Panel)).BeginInit();
            this.kryptonButtonGroup.Panel.SuspendLayout();
            this.kryptonButtonGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupInner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupInner.Panel)).BeginInit();
            this.kryptonGroupInner.Panel.SuspendLayout();
            this.kryptonGroupInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDeletedItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDrafts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFamily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtOutbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSentItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFriends)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInbox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(712, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolStripSeparator6,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator7,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(208, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "&Print";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "C&ut";
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "&Copy";
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "&Paste";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.kryptonPanelMain);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(712, 450);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(712, 475);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // kryptonPanelMain
            // 
            this.kryptonPanelMain.Controls.Add(this.kryptonSplitContainerMain);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(4);
            this.kryptonPanelMain.Size = new System.Drawing.Size(712, 450);
            this.kryptonPanelMain.TabIndex = 0;
            // 
            // kryptonSplitContainerMain
            // 
            this.kryptonSplitContainerMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.kryptonSplitContainerMain.Location = new System.Drawing.Point(4, 4);
            this.kryptonSplitContainerMain.Name = "kryptonSplitContainerMain";
            // 
            // kryptonSplitContainerMain.Panel1
            // 
            this.kryptonSplitContainerMain.Panel1.Controls.Add(this.kryptonNavigatorMain);
            // 
            // kryptonSplitContainerMain.Panel2
            // 
            this.kryptonSplitContainerMain.Panel2.Controls.Add(this.kryptonSplitContainerDetails);
            this.kryptonSplitContainerMain.Size = new System.Drawing.Size(704, 442);
            this.kryptonSplitContainerMain.SplitterDistance = 183;
            this.kryptonSplitContainerMain.TabIndex = 0;
            // 
            // kryptonNavigatorMain
            // 
            this.kryptonNavigatorMain.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigatorMain.Button.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Navigator.ButtonSpecNavigator[] {
            this.buttonSpecExpandCollapse});
            this.kryptonNavigatorMain.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigatorMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigatorMain.Header.HeaderValuesPrimary.MapImage = ComponentFactory.Krypton.Navigator.MapKryptonPageImage.None;
            this.kryptonNavigatorMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigatorMain.Name = "kryptonNavigatorMain";
            this.kryptonNavigatorMain.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.OutlookFull;
            this.kryptonNavigatorMain.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPageMail,
            this.kryptonPageCalendar,
            this.kryptonPageNotes});
            this.kryptonNavigatorMain.SelectedIndex = 0;
            this.kryptonNavigatorMain.Size = new System.Drawing.Size(183, 442);
            this.kryptonNavigatorMain.StateCommon.CheckButton.Content.AdjacentGap = 5;
            this.kryptonNavigatorMain.TabIndex = 0;
            this.kryptonNavigatorMain.Text = "kryptonNavigator1";
            this.kryptonNavigatorMain.SelectedPageChanged += new System.EventHandler(this.kryptonNavigatorMain_SelectedPageChanged);
            // 
            // buttonSpecExpandCollapse
            // 
            this.buttonSpecExpandCollapse.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowLeft;
            this.buttonSpecExpandCollapse.TypeRestricted = ComponentFactory.Krypton.Navigator.PaletteNavButtonSpecStyle.ArrowLeft;
            this.buttonSpecExpandCollapse.UniqueName = "1B343938A2284A991B343938A2284A99";
            this.buttonSpecExpandCollapse.Click += new System.EventHandler(this.buttonSpecExpandCollapse_Click);
            // 
            // kryptonPageMail
            // 
            this.kryptonPageMail.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageMail.Controls.Add(this.treeViewMailFolders);
            this.kryptonPageMail.Controls.Add(this.panelFoldersSep);
            this.kryptonPageMail.Controls.Add(this.kryptonHeaderFolders);
            this.kryptonPageMail.Controls.Add(this.treeViewMailFavs);
            this.kryptonPageMail.Controls.Add(this.panelFavoriteSep);
            this.kryptonPageMail.Controls.Add(this.kryptonHeaderFavorites);
            this.kryptonPageMail.Flags = 65534;
            this.kryptonPageMail.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPageMail.ImageLarge")));
            this.kryptonPageMail.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPageMail.ImageMedium")));
            this.kryptonPageMail.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPageMail.ImageSmall")));
            this.kryptonPageMail.LastVisibleSet = true;
            this.kryptonPageMail.MinimumSize = new System.Drawing.Size(180, 230);
            this.kryptonPageMail.Name = "kryptonPageMail";
            this.kryptonPageMail.Size = new System.Drawing.Size(181, 295);
            this.kryptonPageMail.Text = "Mail";
            this.kryptonPageMail.TextTitle = "Mail";
            this.kryptonPageMail.ToolTipTitle = "Page ToolTip";
            this.kryptonPageMail.UniqueName = "6D4A539F5AB946C76D4A539F5AB946C7";
            // 
            // treeViewMailFolders
            // 
            this.treeViewMailFolders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewMailFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewMailFolders.HideSelection = false;
            this.treeViewMailFolders.ImageIndex = 0;
            this.treeViewMailFolders.ImageList = this.imageListSmall;
            this.treeViewMailFolders.Location = new System.Drawing.Point(0, 89);
            this.treeViewMailFolders.Name = "treeViewMailFolders";
            treeNode3.Name = "nodeDeletedItems";
            treeNode3.Tag = "0";
            treeNode3.Text = "Deleted Items";
            treeNode4.Name = "nodeDrafts";
            treeNode4.Tag = "1";
            treeNode4.Text = "Drafts";
            treeNode5.Name = "nodeFamily";
            treeNode5.Tag = "3";
            treeNode5.Text = "Family";
            treeNode6.Name = "nodeFriends";
            treeNode6.Tag = "4";
            treeNode6.Text = "Friends";
            treeNode7.Name = "nodeWork";
            treeNode7.Tag = "5";
            treeNode7.Text = "Work";
            treeNode8.Name = "nodeInbox";
            treeNode8.Tag = "2";
            treeNode8.Text = "Inbox";
            treeNode9.Name = "nodeOutbox";
            treeNode9.Tag = "6";
            treeNode9.Text = "Outbox";
            treeNode10.Name = "nodeSentItems";
            treeNode10.Tag = "7";
            treeNode10.Text = "Sent Items";
            this.treeViewMailFolders.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode8,
            treeNode9,
            treeNode10});
            this.treeViewMailFolders.SelectedImageIndex = 0;
            this.treeViewMailFolders.Size = new System.Drawing.Size(181, 206);
            this.treeViewMailFolders.TabIndex = 1;
            this.treeViewMailFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMail_AfterSelect);
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListSmall.Images.SetKeyName(0, "VSFolder_closed.bmp");
            this.imageListSmall.Images.SetKeyName(1, "OpendedClosed.bmp");
            this.imageListSmall.Images.SetKeyName(2, "eps_openHS.png");
            // 
            // panelFoldersSep
            // 
            this.panelFoldersSep.BackColor = System.Drawing.SystemColors.Window;
            this.panelFoldersSep.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFoldersSep.Location = new System.Drawing.Point(0, 84);
            this.panelFoldersSep.Name = "panelFoldersSep";
            this.panelFoldersSep.Size = new System.Drawing.Size(181, 5);
            this.panelFoldersSep.TabIndex = 5;
            // 
            // kryptonHeaderFolders
            // 
            this.kryptonHeaderFolders.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderFolders.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderFolders.Location = new System.Drawing.Point(0, 63);
            this.kryptonHeaderFolders.Name = "kryptonHeaderFolders";
            this.kryptonHeaderFolders.Size = new System.Drawing.Size(181, 21);
            this.kryptonHeaderFolders.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)));
            this.kryptonHeaderFolders.TabIndex = 2;
            this.kryptonHeaderFolders.Values.Description = "";
            this.kryptonHeaderFolders.Values.Heading = "Mail Folders";
            this.kryptonHeaderFolders.Values.Image = null;
            // 
            // treeViewMailFavs
            // 
            this.treeViewMailFavs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewMailFavs.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeViewMailFavs.HideSelection = false;
            this.treeViewMailFavs.ImageIndex = 0;
            this.treeViewMailFavs.ImageList = this.imageListSmall;
            this.treeViewMailFavs.Location = new System.Drawing.Point(0, 25);
            this.treeViewMailFavs.Name = "treeViewMailFavs";
            treeNode11.Name = "nodeDrafts";
            treeNode11.Tag = "1";
            treeNode11.Text = "Drafts";
            treeNode12.Name = "nodeFamily";
            treeNode12.Tag = "3";
            treeNode12.Text = "Family";
            this.treeViewMailFavs.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
            this.treeViewMailFavs.SelectedImageIndex = 0;
            this.treeViewMailFavs.Size = new System.Drawing.Size(181, 38);
            this.treeViewMailFavs.TabIndex = 0;
            this.treeViewMailFavs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMail_AfterSelect);
            // 
            // panelFavoriteSep
            // 
            this.panelFavoriteSep.BackColor = System.Drawing.SystemColors.Window;
            this.panelFavoriteSep.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFavoriteSep.Location = new System.Drawing.Point(0, 20);
            this.panelFavoriteSep.Name = "panelFavoriteSep";
            this.panelFavoriteSep.Size = new System.Drawing.Size(181, 5);
            this.panelFavoriteSep.TabIndex = 4;
            // 
            // kryptonHeaderFavorites
            // 
            this.kryptonHeaderFavorites.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderFavorites.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderFavorites.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderFavorites.Name = "kryptonHeaderFavorites";
            this.kryptonHeaderFavorites.Size = new System.Drawing.Size(181, 20);
            this.kryptonHeaderFavorites.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom;
            this.kryptonHeaderFavorites.TabIndex = 1;
            this.kryptonHeaderFavorites.Values.Description = "";
            this.kryptonHeaderFavorites.Values.Heading = "Favorite Folders";
            this.kryptonHeaderFavorites.Values.Image = null;
            // 
            // kryptonPageCalendar
            // 
            this.kryptonPageCalendar.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageCalendar.Controls.Add(this.monthCalendar1);
            this.kryptonPageCalendar.Flags = 65534;
            this.kryptonPageCalendar.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPageCalendar.ImageLarge")));
            this.kryptonPageCalendar.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPageCalendar.ImageMedium")));
            this.kryptonPageCalendar.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPageCalendar.ImageSmall")));
            this.kryptonPageCalendar.LastVisibleSet = true;
            this.kryptonPageCalendar.MinimumSize = new System.Drawing.Size(190, 155);
            this.kryptonPageCalendar.Name = "kryptonPageCalendar";
            this.kryptonPageCalendar.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPageCalendar.Size = new System.Drawing.Size(190, 296);
            this.kryptonPageCalendar.Text = "Calendar";
            this.kryptonPageCalendar.TextTitle = "Calendar";
            this.kryptonPageCalendar.ToolTipTitle = "Page ToolTip";
            this.kryptonPageCalendar.UniqueName = "20332D6AA91B4AF120332D6AA91B4AF1";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(5, 5);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowToday = false;
            this.monthCalendar1.ShowTodayCircle = false;
            this.monthCalendar1.TabIndex = 0;
            // 
            // kryptonPageNotes
            // 
            this.kryptonPageNotes.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageNotes.Controls.Add(this.radioFriends);
            this.kryptonPageNotes.Controls.Add(this.radioFamily);
            this.kryptonPageNotes.Controls.Add(this.radioProject);
            this.kryptonPageNotes.Flags = 65534;
            this.kryptonPageNotes.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPageNotes.ImageLarge")));
            this.kryptonPageNotes.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPageNotes.ImageMedium")));
            this.kryptonPageNotes.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPageNotes.ImageSmall")));
            this.kryptonPageNotes.LastVisibleSet = true;
            this.kryptonPageNotes.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPageNotes.Name = "kryptonPageNotes";
            this.kryptonPageNotes.Padding = new System.Windows.Forms.Padding(20);
            this.kryptonPageNotes.Size = new System.Drawing.Size(181, 296);
            this.kryptonPageNotes.Text = "Notes";
            this.kryptonPageNotes.TextTitle = "Notes";
            this.kryptonPageNotes.ToolTipTitle = "Page ToolTip";
            this.kryptonPageNotes.UniqueName = "F896ACB8955B498FF896ACB8955B498F";
            // 
            // radioFriends
            // 
            this.radioFriends.Location = new System.Drawing.Point(23, 73);
            this.radioFriends.Name = "radioFriends";
            this.radioFriends.Size = new System.Drawing.Size(62, 20);
            this.radioFriends.TabIndex = 8;
            this.radioFriends.Values.Text = "Friends";
            this.radioFriends.CheckedChanged += new System.EventHandler(this.radioNotes_CheckedChanged);
            // 
            // radioFamily
            // 
            this.radioFamily.Location = new System.Drawing.Point(23, 48);
            this.radioFamily.Name = "radioFamily";
            this.radioFamily.Size = new System.Drawing.Size(57, 20);
            this.radioFamily.TabIndex = 7;
            this.radioFamily.Values.Text = "Family";
            this.radioFamily.CheckedChanged += new System.EventHandler(this.radioNotes_CheckedChanged);
            // 
            // radioProject
            // 
            this.radioProject.Checked = true;
            this.radioProject.Location = new System.Drawing.Point(23, 23);
            this.radioProject.Name = "radioProject";
            this.radioProject.Size = new System.Drawing.Size(60, 20);
            this.radioProject.TabIndex = 6;
            this.radioProject.Values.Text = "Project";
            this.radioProject.CheckedChanged += new System.EventHandler(this.radioNotes_CheckedChanged);
            // 
            // kryptonSplitContainerDetails
            // 
            this.kryptonSplitContainerDetails.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainerDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainerDetails.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainerDetails.Name = "kryptonSplitContainerDetails";
            this.kryptonSplitContainerDetails.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // kryptonSplitContainerDetails.Panel1
            // 
            this.kryptonSplitContainerDetails.Panel1.Controls.Add(this.kryptonNavigatorDetails);
            // 
            // kryptonSplitContainerDetails.Panel2
            // 
            this.kryptonSplitContainerDetails.Panel2.Controls.Add(this.kryptonButtonGroup);
            this.kryptonSplitContainerDetails.Size = new System.Drawing.Size(516, 442);
            this.kryptonSplitContainerDetails.SplitterDistance = 233;
            this.kryptonSplitContainerDetails.TabIndex = 0;
            // 
            // kryptonNavigatorDetails
            // 
            this.kryptonNavigatorDetails.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigatorDetails.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigatorDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigatorDetails.Header.HeaderVisibleSecondary = false;
            this.kryptonNavigatorDetails.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigatorDetails.Name = "kryptonNavigatorDetails";
            this.kryptonNavigatorDetails.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.HeaderGroup;
            this.kryptonNavigatorDetails.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPageMailDetails,
            this.kryptonPageCalendarDetails,
            this.kryptonPageNotesDetails});
            this.kryptonNavigatorDetails.SelectedIndex = 0;
            this.kryptonNavigatorDetails.Size = new System.Drawing.Size(516, 233);
            this.kryptonNavigatorDetails.StateCommon.HeaderGroup.HeaderPrimary.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonNavigatorDetails.TabIndex = 0;
            this.kryptonNavigatorDetails.Text = "kryptonNavigator1";
            // 
            // kryptonPageMailDetails
            // 
            this.kryptonPageMailDetails.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageMailDetails.Controls.Add(this.kryptonDataGridView1);
            this.kryptonPageMailDetails.Flags = 65534;
            this.kryptonPageMailDetails.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPageMailDetails.ImageLarge")));
            this.kryptonPageMailDetails.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPageMailDetails.ImageMedium")));
            this.kryptonPageMailDetails.LastVisibleSet = true;
            this.kryptonPageMailDetails.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPageMailDetails.Name = "kryptonPageMailDetails";
            this.kryptonPageMailDetails.Size = new System.Drawing.Size(514, 202);
            this.kryptonPageMailDetails.Text = "Mail";
            this.kryptonPageMailDetails.TextTitle = "Mail";
            this.kryptonPageMailDetails.ToolTipTitle = "Page ToolTip";
            this.kryptonPageMailDetails.UniqueName = "2978E4C56C8641122978E4C56C864112";
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.AllowUserToAddRows = false;
            this.kryptonDataGridView1.AllowUserToDeleteRows = false;
            this.kryptonDataGridView1.AllowUserToResizeRows = false;
            this.kryptonDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgName,
            this.dgSubject,
            this.dgReceived,
            this.dgSize});
            this.kryptonDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDataGridView1.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.kryptonDataGridView1.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonDataGridView1.HideOuterBorders = true;
            this.kryptonDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.kryptonDataGridView1.MultiSelect = false;
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.ReadOnly = true;
            this.kryptonDataGridView1.RowHeadersVisible = false;
            this.kryptonDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.kryptonDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.kryptonDataGridView1.Size = new System.Drawing.Size(514, 202);
            this.kryptonDataGridView1.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonDataGridView1.StateCommon.DataCell.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom;
            this.kryptonDataGridView1.TabIndex = 0;
            // 
            // dgName
            // 
            this.dgName.DataPropertyName = "Name";
            this.dgName.HeaderText = "Name";
            this.dgName.MinimumWidth = 100;
            this.dgName.Name = "dgName";
            this.dgName.ReadOnly = true;
            // 
            // dgSubject
            // 
            this.dgSubject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgSubject.DataPropertyName = "Subject";
            this.dgSubject.HeaderText = "Subject";
            this.dgSubject.MinimumWidth = 100;
            this.dgSubject.Name = "dgSubject";
            this.dgSubject.ReadOnly = true;
            // 
            // dgReceived
            // 
            this.dgReceived.DataPropertyName = "Received";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgReceived.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgReceived.HeaderText = "Received";
            this.dgReceived.MinimumWidth = 100;
            this.dgReceived.Name = "dgReceived";
            this.dgReceived.ReadOnly = true;
            // 
            // dgSize
            // 
            this.dgSize.DataPropertyName = "Size";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgSize.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgSize.HeaderText = "Size";
            this.dgSize.MinimumWidth = 50;
            this.dgSize.Name = "dgSize";
            this.dgSize.ReadOnly = true;
            this.dgSize.Width = 50;
            // 
            // kryptonPageCalendarDetails
            // 
            this.kryptonPageCalendarDetails.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageCalendarDetails.Controls.Add(this.textBox1);
            this.kryptonPageCalendarDetails.Flags = 65534;
            this.kryptonPageCalendarDetails.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPageCalendarDetails.ImageLarge")));
            this.kryptonPageCalendarDetails.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPageCalendarDetails.ImageMedium")));
            this.kryptonPageCalendarDetails.LastVisibleSet = true;
            this.kryptonPageCalendarDetails.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPageCalendarDetails.Name = "kryptonPageCalendarDetails";
            this.kryptonPageCalendarDetails.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPageCalendarDetails.Size = new System.Drawing.Size(514, 205);
            this.kryptonPageCalendarDetails.Text = "Calendar";
            this.kryptonPageCalendarDetails.TextTitle = "Calendar";
            this.kryptonPageCalendarDetails.ToolTipTitle = "Page ToolTip";
            this.kryptonPageCalendarDetails.UniqueName = "7E4DA62769154DBD7E4DA62769154DBD";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(5, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(504, 195);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // kryptonPageNotesDetails
            // 
            this.kryptonPageNotesDetails.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageNotesDetails.Controls.Add(this.listViewNotes);
            this.kryptonPageNotesDetails.Flags = 65534;
            this.kryptonPageNotesDetails.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPageNotesDetails.ImageLarge")));
            this.kryptonPageNotesDetails.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPageNotesDetails.ImageMedium")));
            this.kryptonPageNotesDetails.LastVisibleSet = true;
            this.kryptonPageNotesDetails.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPageNotesDetails.Name = "kryptonPageNotesDetails";
            this.kryptonPageNotesDetails.Padding = new System.Windows.Forms.Padding(3, 6, 0, 0);
            this.kryptonPageNotesDetails.Size = new System.Drawing.Size(514, 205);
            this.kryptonPageNotesDetails.Text = "Notes";
            this.kryptonPageNotesDetails.TextTitle = "Notes";
            this.kryptonPageNotesDetails.ToolTipTitle = "Page ToolTip";
            this.kryptonPageNotesDetails.UniqueName = "44092B31BDA3475E44092B31BDA3475E";
            // 
            // listViewNotes
            // 
            this.listViewNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewNotes.LargeImageList = this.imageListLarge;
            this.listViewNotes.Location = new System.Drawing.Point(3, 6);
            this.listViewNotes.Name = "listViewNotes";
            this.listViewNotes.Size = new System.Drawing.Size(511, 199);
            this.listViewNotes.TabIndex = 0;
            this.listViewNotes.UseCompatibleStateImageBehavior = false;
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "note.png");
            // 
            // kryptonButtonGroup
            // 
            this.kryptonButtonGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonButtonGroup.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonButtonGroup.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlAlternate;
            this.kryptonButtonGroup.Location = new System.Drawing.Point(0, 0);
            this.kryptonButtonGroup.Name = "kryptonButtonGroup";
            // 
            // kryptonButtonGroup.Panel
            // 
            this.kryptonButtonGroup.Panel.Controls.Add(this.kryptonGroupInner);
            this.kryptonButtonGroup.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonButtonGroup.Size = new System.Drawing.Size(516, 204);
            this.kryptonButtonGroup.TabIndex = 0;
            // 
            // kryptonGroupInner
            // 
            this.kryptonGroupInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupInner.Location = new System.Drawing.Point(5, 5);
            this.kryptonGroupInner.Name = "kryptonGroupInner";
            // 
            // kryptonGroupInner.Panel
            // 
            this.kryptonGroupInner.Panel.Controls.Add(this.radioOffice2010Black);
            this.kryptonGroupInner.Panel.Controls.Add(this.radioOffice2010Silver);
            this.kryptonGroupInner.Panel.Controls.Add(this.radioOffice2010Blue);
            this.kryptonGroupInner.Panel.Controls.Add(this.radioSparkle);
            this.kryptonGroupInner.Panel.Controls.Add(this.radioSystem);
            this.kryptonGroupInner.Panel.Controls.Add(this.radioOffice2003);
            this.kryptonGroupInner.Panel.Controls.Add(this.radioOffice2007Black);
            this.kryptonGroupInner.Panel.Controls.Add(this.radioOffice2007Silver);
            this.kryptonGroupInner.Panel.Controls.Add(this.radioOffice2007Blue);
            this.kryptonGroupInner.Size = new System.Drawing.Size(504, 192);
            this.kryptonGroupInner.TabIndex = 0;
            // 
            // radioSparkle
            // 
            this.radioSparkle.Location = new System.Drawing.Point(288, 44);
            this.radioSparkle.Name = "radioSparkle";
            this.radioSparkle.Size = new System.Drawing.Size(60, 19);
            this.radioSparkle.TabIndex = 7;
            this.radioSparkle.Values.Text = "Sparkle";
            this.radioSparkle.CheckedChanged += new System.EventHandler(this.radioSparkle_CheckedChanged);
            // 
            // radioSystem
            // 
            this.radioSystem.Location = new System.Drawing.Point(288, 68);
            this.radioSystem.Name = "radioSystem";
            this.radioSystem.Size = new System.Drawing.Size(59, 19);
            this.radioSystem.TabIndex = 8;
            this.radioSystem.Values.Text = "System";
            this.radioSystem.CheckedChanged += new System.EventHandler(this.radioSystem_CheckedChanged);
            // 
            // radioOffice2003
            // 
            this.radioOffice2003.Location = new System.Drawing.Point(288, 20);
            this.radioOffice2003.Name = "radioOffice2003";
            this.radioOffice2003.Size = new System.Drawing.Size(81, 19);
            this.radioOffice2003.TabIndex = 6;
            this.radioOffice2003.Values.Text = "Office 2003";
            this.radioOffice2003.CheckedChanged += new System.EventHandler(this.radioOffice2003_CheckedChanged);
            // 
            // radioOffice2007Black
            // 
            this.radioOffice2007Black.Location = new System.Drawing.Point(153, 68);
            this.radioOffice2007Black.Name = "radioOffice2007Black";
            this.radioOffice2007Black.Size = new System.Drawing.Size(119, 19);
            this.radioOffice2007Black.TabIndex = 5;
            this.radioOffice2007Black.Values.Text = "Office 2007 - Black";
            this.radioOffice2007Black.CheckedChanged += new System.EventHandler(this.radioOffice2007Black_CheckedChanged);
            // 
            // radioOffice2007Silver
            // 
            this.radioOffice2007Silver.Location = new System.Drawing.Point(153, 45);
            this.radioOffice2007Silver.Name = "radioOffice2007Silver";
            this.radioOffice2007Silver.Size = new System.Drawing.Size(120, 19);
            this.radioOffice2007Silver.TabIndex = 4;
            this.radioOffice2007Silver.Values.Text = "Office 2007 - Silver";
            this.radioOffice2007Silver.CheckedChanged += new System.EventHandler(this.radioOffice2007Silver_CheckedChanged);
            // 
            // radioOffice2007Blue
            // 
            this.radioOffice2007Blue.Location = new System.Drawing.Point(153, 20);
            this.radioOffice2007Blue.Name = "radioOffice2007Blue";
            this.radioOffice2007Blue.Size = new System.Drawing.Size(114, 19);
            this.radioOffice2007Blue.TabIndex = 3;
            this.radioOffice2007Blue.Values.Text = "Office 2007 - Blue";
            this.radioOffice2007Blue.CheckedChanged += new System.EventHandler(this.radioOffice2007Blue_CheckedChanged);
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "NewDataSet";
            this.dataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.dtDeletedItems,
            this.dtDrafts,
            this.dtFamily,
            this.dtOutbox,
            this.dtSentItems,
            this.dtFriends,
            this.dtWork,
            this.dtInbox});
            // 
            // dtDeletedItems
            // 
            this.dtDeletedItems.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4});
            this.dtDeletedItems.TableName = "Deleted Items";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Name";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "Subject";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Received";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Size";
            // 
            // dtDrafts
            // 
            this.dtDrafts.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8});
            this.dtDrafts.TableName = "Drafts";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Name";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "Subject";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "Received";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "Size";
            // 
            // dtFamily
            // 
            this.dtFamily.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12});
            this.dtFamily.TableName = "Family";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "Name";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "Subject";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "Received";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "Size";
            // 
            // dtOutbox
            // 
            this.dtOutbox.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16});
            this.dtOutbox.TableName = "Outbox";
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "Name";
            // 
            // dataColumn14
            // 
            this.dataColumn14.ColumnName = "Subject";
            // 
            // dataColumn15
            // 
            this.dataColumn15.ColumnName = "Received";
            // 
            // dataColumn16
            // 
            this.dataColumn16.ColumnName = "Size";
            // 
            // dtSentItems
            // 
            this.dtSentItems.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn19,
            this.dataColumn20});
            this.dtSentItems.TableName = "Sent Items";
            // 
            // dataColumn17
            // 
            this.dataColumn17.ColumnName = "Name";
            // 
            // dataColumn18
            // 
            this.dataColumn18.ColumnName = "Subject";
            // 
            // dataColumn19
            // 
            this.dataColumn19.ColumnName = "Received";
            // 
            // dataColumn20
            // 
            this.dataColumn20.ColumnName = "Size";
            // 
            // dtFriends
            // 
            this.dtFriends.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn24});
            this.dtFriends.TableName = "Friends";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "Name";
            // 
            // dataColumn22
            // 
            this.dataColumn22.ColumnName = "Subject";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "Received";
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "Size";
            // 
            // dtWork
            // 
            this.dtWork.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn25,
            this.dataColumn26,
            this.dataColumn27,
            this.dataColumn28});
            this.dtWork.TableName = "Work";
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "Work";
            // 
            // dataColumn26
            // 
            this.dataColumn26.ColumnName = "Subject";
            // 
            // dataColumn27
            // 
            this.dataColumn27.ColumnName = "Received";
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "Size";
            // 
            // dtInbox
            // 
            this.dtInbox.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn29,
            this.dataColumn30,
            this.dataColumn31,
            this.dataColumn32});
            this.dtInbox.TableName = "Inbox";
            // 
            // dataColumn29
            // 
            this.dataColumn29.ColumnName = "Name";
            // 
            // dataColumn30
            // 
            this.dataColumn30.ColumnName = "Subject";
            // 
            // dataColumn31
            // 
            this.dataColumn31.ColumnName = "Received";
            // 
            // dataColumn32
            // 
            this.dataColumn32.ColumnName = "Size";
            // 
            // radioOffice2010Black
            // 
            this.radioOffice2010Black.Location = new System.Drawing.Point(17, 68);
            this.radioOffice2010Black.Name = "radioOffice2010Black";
            this.radioOffice2010Black.Size = new System.Drawing.Size(119, 19);
            this.radioOffice2010Black.TabIndex = 2;
            this.radioOffice2010Black.Values.Text = "Office 2010 - Black";
            this.radioOffice2010Black.CheckedChanged += new System.EventHandler(this.radioOffice2010Black_CheckedChanged);
            // 
            // radioOffice2010Silver
            // 
            this.radioOffice2010Silver.Location = new System.Drawing.Point(17, 45);
            this.radioOffice2010Silver.Name = "radioOffice2010Silver";
            this.radioOffice2010Silver.Size = new System.Drawing.Size(120, 19);
            this.radioOffice2010Silver.TabIndex = 1;
            this.radioOffice2010Silver.Values.Text = "Office 2010 - Silver";
            this.radioOffice2010Silver.CheckedChanged += new System.EventHandler(this.radioOffice2010Silver_CheckedChanged);
            // 
            // radioOffice2010Blue
            // 
            this.radioOffice2010Blue.Checked = true;
            this.radioOffice2010Blue.Location = new System.Drawing.Point(17, 20);
            this.radioOffice2010Blue.Name = "radioOffice2010Blue";
            this.radioOffice2010Blue.Size = new System.Drawing.Size(114, 19);
            this.radioOffice2010Blue.TabIndex = 0;
            this.radioOffice2010Blue.Values.Text = "Office 2010 - Blue";
            this.radioOffice2010Blue.CheckedChanged += new System.EventHandler(this.radioOffice2010Blue_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 499);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(417, 308);
            this.Name = "Form1";
            this.Text = "Outlook 2003 Mockup";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel1)).EndInit();
            this.kryptonSplitContainerMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel2)).EndInit();
            this.kryptonSplitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain)).EndInit();
            this.kryptonSplitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigatorMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageMail)).EndInit();
            this.kryptonPageMail.ResumeLayout(false);
            this.kryptonPageMail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageCalendar)).EndInit();
            this.kryptonPageCalendar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageNotes)).EndInit();
            this.kryptonPageNotes.ResumeLayout(false);
            this.kryptonPageNotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerDetails.Panel1)).EndInit();
            this.kryptonSplitContainerDetails.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerDetails.Panel2)).EndInit();
            this.kryptonSplitContainerDetails.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerDetails)).EndInit();
            this.kryptonSplitContainerDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigatorDetails)).EndInit();
            this.kryptonNavigatorDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageMailDetails)).EndInit();
            this.kryptonPageMailDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageCalendarDetails)).EndInit();
            this.kryptonPageCalendarDetails.ResumeLayout(false);
            this.kryptonPageCalendarDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageNotesDetails)).EndInit();
            this.kryptonPageNotesDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonButtonGroup.Panel)).EndInit();
            this.kryptonButtonGroup.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonButtonGroup)).EndInit();
            this.kryptonButtonGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupInner.Panel)).EndInit();
            this.kryptonGroupInner.Panel.ResumeLayout(false);
            this.kryptonGroupInner.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupInner)).EndInit();
            this.kryptonGroupInner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDeletedItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDrafts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFamily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtOutbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSentItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFriends)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainerMain;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainerDetails;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonButtonGroup;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroupInner;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigatorMain;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPageMail;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPageCalendar;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPageNotes;
        private System.Windows.Forms.TreeView treeViewMailFolders;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigatorDetails;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPageMailDetails;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPageCalendarDetails;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPageNotesDetails;
        private System.Windows.Forms.ListView listViewNotes;
        private System.Windows.Forms.ImageList imageListLarge;
        private System.Windows.Forms.TreeView treeViewMailFavs;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeaderFolders;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeaderFavorites;
        private System.Windows.Forms.Panel panelFoldersSep;
        private System.Windows.Forms.Panel panelFavoriteSep;
        private System.Windows.Forms.TextBox textBox1;
        private ComponentFactory.Krypton.Navigator.ButtonSpecNavigator buttonSpecExpandCollapse;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioSystem;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioOffice2003;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioOffice2007Black;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioOffice2007Silver;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioOffice2007Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioFriends;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioFamily;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioProject;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private System.Data.DataSet dataSet;
        private System.Data.DataTable dtDeletedItems;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataTable dtDrafts;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataTable dtFamily;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn10;
        private System.Data.DataColumn dataColumn11;
        private System.Data.DataColumn dataColumn12;
        private System.Data.DataTable dtOutbox;
        private System.Data.DataColumn dataColumn13;
        private System.Data.DataColumn dataColumn14;
        private System.Data.DataColumn dataColumn15;
        private System.Data.DataColumn dataColumn16;
        private System.Data.DataTable dtSentItems;
        private System.Data.DataColumn dataColumn17;
        private System.Data.DataColumn dataColumn18;
        private System.Data.DataColumn dataColumn19;
        private System.Data.DataColumn dataColumn20;
        private System.Data.DataTable dtFriends;
        private System.Data.DataColumn dataColumn21;
        private System.Data.DataColumn dataColumn22;
        private System.Data.DataColumn dataColumn23;
        private System.Data.DataColumn dataColumn24;
        private System.Data.DataTable dtWork;
        private System.Data.DataColumn dataColumn25;
        private System.Data.DataColumn dataColumn26;
        private System.Data.DataColumn dataColumn27;
        private System.Data.DataColumn dataColumn28;
        private System.Data.DataTable dtInbox;
        private System.Data.DataColumn dataColumn29;
        private System.Data.DataColumn dataColumn30;
        private System.Data.DataColumn dataColumn31;
        private System.Data.DataColumn dataColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgReceived;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgSize;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioSparkle;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioOffice2010Black;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioOffice2010Silver;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton radioOffice2010Blue;
    }
}

