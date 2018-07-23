namespace ExternalDragToDocking
{
    partial class ContentTreeView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Document");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Property Grid");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Input Form");
            this.treeView1 = new ExternalDragToDocking.DraggableTreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Form1 = null;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Document";
            treeNode1.Text = "Document";
            treeNode2.Name = "Property Grid";
            treeNode2.Text = "Property Grid";
            treeNode3.Name = "Input Form";
            treeNode3.Text = "Input Form";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView1.Size = new System.Drawing.Size(150, 150);
            this.treeView1.TabIndex = 0;
            // 
            // ContentTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView1);
            this.Name = "ContentTreeView";
            this.ResumeLayout(false);

        }

        #endregion

        private DraggableTreeView treeView1;
    }
}
