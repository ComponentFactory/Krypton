namespace WorkspaceCellLayout
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
            this.kryptonWorkspace = new ComponentFactory.Krypton.Workspace.KryptonWorkspace();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelSingleCell = new System.Windows.Forms.Label();
            this.buttonNewSequences = new System.Windows.Forms.Button();
            this.buttonNewThreeCells = new System.Windows.Forms.Button();
            this.buttonNewSingleCell = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonAddPage = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonApplySingleColumn = new System.Windows.Forms.Button();
            this.buttonApplySingleRow = new System.Windows.Forms.Button();
            this.buttonApplyGrid = new System.Windows.Forms.Button();
            this.buttonApplySingleCell = new System.Windows.Forms.Button();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonWorkspace
            // 
            this.kryptonWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonWorkspace.Location = new System.Drawing.Point(265, 52);
            this.kryptonWorkspace.Name = "kryptonWorkspace";
            // 
            // 
            // 
            this.kryptonWorkspace.Root.UniqueName = "0B1488CA488E48AFC0885C55F1E201CA";
            this.kryptonWorkspace.Size = new System.Drawing.Size(497, 463);
            this.kryptonWorkspace.TabIndex = 5;
            this.kryptonWorkspace.TabStop = true;
            this.kryptonWorkspace.WorkspaceCellAdding += new System.EventHandler<ComponentFactory.Krypton.Workspace.WorkspaceCellEventArgs>(this.kryptonWorkspace_WorkspaceCellAdding);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelSingleCell);
            this.groupBox1.Controls.Add(this.buttonNewSequences);
            this.groupBox1.Controls.Add(this.buttonNewThreeCells);
            this.groupBox1.Controls.Add(this.buttonNewSingleCell);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 227);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Example Workspaces";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 53);
            this.label3.TabIndex = 5;
            this.label3.Text = "Creates embedded sequences. Sequences in the place of individual cells allows eve" +
                "n the most complex layouts to be quickly defined.\r\n";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Create 3 cells positioned horizontally.";
            // 
            // labelSingleCell
            // 
            this.labelSingleCell.Location = new System.Drawing.Point(14, 58);
            this.labelSingleCell.Name = "labelSingleCell";
            this.labelSingleCell.Size = new System.Drawing.Size(223, 25);
            this.labelSingleCell.TabIndex = 1;
            this.labelSingleCell.Text = "Create a single cell with several pages.";
            // 
            // buttonNewSequences
            // 
            this.buttonNewSequences.Location = new System.Drawing.Point(17, 141);
            this.buttonNewSequences.Name = "buttonNewSequences";
            this.buttonNewSequences.Size = new System.Drawing.Size(97, 26);
            this.buttonNewSequences.TabIndex = 4;
            this.buttonNewSequences.Text = "Sequences";
            this.buttonNewSequences.UseVisualStyleBackColor = true;
            this.buttonNewSequences.Click += new System.EventHandler(this.buttonNewSequences_Click);
            // 
            // buttonNewThreeCells
            // 
            this.buttonNewThreeCells.Location = new System.Drawing.Point(17, 86);
            this.buttonNewThreeCells.Name = "buttonNewThreeCells";
            this.buttonNewThreeCells.Size = new System.Drawing.Size(97, 26);
            this.buttonNewThreeCells.TabIndex = 2;
            this.buttonNewThreeCells.Text = "Three Cells";
            this.buttonNewThreeCells.UseVisualStyleBackColor = true;
            this.buttonNewThreeCells.Click += new System.EventHandler(this.buttonNewThreeCells_Click);
            // 
            // buttonNewSingleCell
            // 
            this.buttonNewSingleCell.Location = new System.Drawing.Point(17, 29);
            this.buttonNewSingleCell.Name = "buttonNewSingleCell";
            this.buttonNewSingleCell.Size = new System.Drawing.Size(97, 26);
            this.buttonNewSingleCell.TabIndex = 0;
            this.buttonNewSingleCell.Text = "Single Cell";
            this.buttonNewSingleCell.UseVisualStyleBackColor = true;
            this.buttonNewSingleCell.Click += new System.EventHandler(this.buttonNewSingleCell_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(346, 18);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 26);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonAddPage
            // 
            this.buttonAddPage.Location = new System.Drawing.Point(265, 18);
            this.buttonAddPage.Name = "buttonAddPage";
            this.buttonAddPage.Size = new System.Drawing.Size(75, 26);
            this.buttonAddPage.TabIndex = 2;
            this.buttonAddPage.Text = "Add Page";
            this.buttonAddPage.UseVisualStyleBackColor = true;
            this.buttonAddPage.Click += new System.EventHandler(this.buttonAddPage_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(687, 18);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 26);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "alarmclock.png");
            this.imageList.Images.SetKeyName(1, "apple.png");
            this.imageList.Images.SetKeyName(2, "banana.png");
            this.imageList.Images.SetKeyName(3, "baseball.png");
            this.imageList.Images.SetKeyName(4, "die_blue.png");
            this.imageList.Images.SetKeyName(5, "flower_yellow.png");
            this.imageList.Images.SetKeyName(6, "hammer2.png");
            this.imageList.Images.SetKeyName(7, "judge.png");
            this.imageList.Images.SetKeyName(8, "lemon.png");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.buttonApplySingleColumn);
            this.groupBox2.Controls.Add(this.buttonApplySingleRow);
            this.groupBox2.Controls.Add(this.buttonApplyGrid);
            this.groupBox2.Controls.Add(this.buttonApplySingleCell);
            this.groupBox2.Location = new System.Drawing.Point(12, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 276);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rearrange Workspace";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(14, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(223, 39);
            this.label6.TabIndex = 7;
            this.label6.Text = "One cell per page and arranged into a grid. It tries to make the grid as square a" +
                "s possible.";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(223, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "One cell per page arranged vertically.";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(223, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "One cell per page arranged horizontally.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Move all pages to a single cell.";
            // 
            // buttonApplySingleColumn
            // 
            this.buttonApplySingleColumn.Location = new System.Drawing.Point(17, 146);
            this.buttonApplySingleColumn.Name = "buttonApplySingleColumn";
            this.buttonApplySingleColumn.Size = new System.Drawing.Size(90, 26);
            this.buttonApplySingleColumn.TabIndex = 4;
            this.buttonApplySingleColumn.Text = "Single Column";
            this.buttonApplySingleColumn.UseVisualStyleBackColor = true;
            this.buttonApplySingleColumn.Click += new System.EventHandler(this.buttonApplySingleColumn_Click);
            // 
            // buttonApplySingleRow
            // 
            this.buttonApplySingleRow.Location = new System.Drawing.Point(16, 89);
            this.buttonApplySingleRow.Name = "buttonApplySingleRow";
            this.buttonApplySingleRow.Size = new System.Drawing.Size(90, 26);
            this.buttonApplySingleRow.TabIndex = 2;
            this.buttonApplySingleRow.Text = "Single Row";
            this.buttonApplySingleRow.UseVisualStyleBackColor = true;
            this.buttonApplySingleRow.Click += new System.EventHandler(this.buttonApplySingleRow_Click);
            // 
            // buttonApplyGrid
            // 
            this.buttonApplyGrid.Location = new System.Drawing.Point(17, 203);
            this.buttonApplyGrid.Name = "buttonApplyGrid";
            this.buttonApplyGrid.Size = new System.Drawing.Size(90, 26);
            this.buttonApplyGrid.TabIndex = 6;
            this.buttonApplyGrid.Text = "Grid";
            this.buttonApplyGrid.UseVisualStyleBackColor = true;
            this.buttonApplyGrid.Click += new System.EventHandler(this.buttonApplyGrid_Click);
            // 
            // buttonApplySingleCell
            // 
            this.buttonApplySingleCell.Location = new System.Drawing.Point(16, 32);
            this.buttonApplySingleCell.Name = "buttonApplySingleCell";
            this.buttonApplySingleCell.Size = new System.Drawing.Size(90, 26);
            this.buttonApplySingleCell.TabIndex = 0;
            this.buttonApplySingleCell.Text = "Single Cell";
            this.buttonApplySingleCell.UseVisualStyleBackColor = true;
            this.buttonApplySingleCell.Click += new System.EventHandler(this.buttonApplySingleCell_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 527);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonAddPage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.kryptonWorkspace);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(530, 563);
            this.Name = "Form1";
            this.Text = "Workspace Cell Layout";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Workspace.KryptonWorkspace kryptonWorkspace;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonAddPage;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button buttonNewSingleCell;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonApplyGrid;
        private System.Windows.Forms.Button buttonApplySingleCell;
        private System.Windows.Forms.Button buttonNewSequences;
        private System.Windows.Forms.Button buttonNewThreeCells;
        private System.Windows.Forms.Button buttonApplySingleColumn;
        private System.Windows.Forms.Button buttonApplySingleRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSingleCell;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
    }
}

