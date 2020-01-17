namespace MyWindowsTools
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
<<<<<<< HEAD
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTreeview = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panelFuntion = new System.Windows.Forms.Panel();
            this.panelButton = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAddShortcut = new System.Windows.Forms.Button();
            this.btnAddfolder = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panelView = new System.Windows.Forms.Panel();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTarget = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.brnCollapseAll = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelTreeview.SuspendLayout();
            this.panelFuntion.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.panelView.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelTreeview);
            this.panel1.Controls.Add(this.panelFuntion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // panelTreeview
            // 
            this.panelTreeview.Controls.Add(this.treeView1);
            this.panelTreeview.Location = new System.Drawing.Point(3, 3);
            this.panelTreeview.Name = "panelTreeview";
            this.panelTreeview.Size = new System.Drawing.Size(290, 444);
            this.panelTreeview.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(290, 444);
            this.treeView1.TabIndex = 1;
            // 
            // panelFuntion
            // 
            this.panelFuntion.Controls.Add(this.panelButton);
            this.panelFuntion.Controls.Add(this.panelView);
            this.panelFuntion.Location = new System.Drawing.Point(299, 3);
            this.panelFuntion.Name = "panelFuntion";
            this.panelFuntion.Size = new System.Drawing.Size(498, 444);
            this.panelFuntion.TabIndex = 0;
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.brnCollapseAll);
            this.panelButton.Controls.Add(this.btnDelete);
            this.panelButton.Controls.Add(this.btnEdit);
            this.panelButton.Controls.Add(this.btnAddShortcut);
            this.panelButton.Controls.Add(this.btnAddfolder);
            this.panelButton.Controls.Add(this.btnBrowse);
            this.panelButton.Location = new System.Drawing.Point(6, 206);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(489, 235);
            this.panelButton.TabIndex = 4;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(383, 95);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 38);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(277, 94);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 38);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAddShortcut
            // 
            this.btnAddShortcut.Location = new System.Drawing.Point(148, 94);
            this.btnAddShortcut.Name = "btnAddShortcut";
            this.btnAddShortcut.Size = new System.Drawing.Size(98, 40);
            this.btnAddShortcut.TabIndex = 2;
            this.btnAddShortcut.Text = "Add Shortcut";
            this.btnAddShortcut.UseVisualStyleBackColor = true;
            // 
            // btnAddfolder
            // 
            this.btnAddfolder.Location = new System.Drawing.Point(23, 93);
            this.btnAddfolder.Name = "btnAddfolder";
            this.btnAddfolder.Size = new System.Drawing.Size(96, 40);
            this.btnAddfolder.TabIndex = 1;
            this.btnAddfolder.Text = "Add folder";
            this.btnAddfolder.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(383, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 41);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panelView
            // 
            this.panelView.Controls.Add(this.txtTarget);
            this.panelView.Controls.Add(this.txtSource);
            this.panelView.Controls.Add(this.txtName);
            this.panelView.Controls.Add(this.lblName);
            this.panelView.Controls.Add(this.lblTarget);
            this.panelView.Controls.Add(this.lblSource);
            this.panelView.Location = new System.Drawing.Point(3, 3);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(495, 197);
            this.panelView.TabIndex = 3;
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(111, 121);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(327, 22);
            this.txtTarget.TabIndex = 5;
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(111, 66);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(327, 22);
            this.txtSource.TabIndex = 4;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(111, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(327, 22);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(35, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(35, 124);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(50, 17);
            this.lblTarget.TabIndex = 2;
            this.lblTarget.Text = "Target";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(35, 69);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(53, 17);
            this.lblSource.TabIndex = 1;
            this.lblSource.Text = "Source";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // brnCollapseAll
            // 
            this.brnCollapseAll.Location = new System.Drawing.Point(23, 25);
            this.brnCollapseAll.Name = "brnCollapseAll";
            this.brnCollapseAll.Size = new System.Drawing.Size(101, 41);
            this.brnCollapseAll.TabIndex = 5;
            this.brnCollapseAll.Text = "Collapse All";
            this.brnCollapseAll.UseVisualStyleBackColor = true;
            this.brnCollapseAll.Click += new System.EventHandler(this.brnCollapseAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panelTreeview.ResumeLayout(false);
            this.panelFuntion.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.panelView.ResumeLayout(false);
            this.panelView.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelTreeview;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panelFuntion;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAddShortcut;
        private System.Windows.Forms.Button btnAddfolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button brnCollapseAll;
=======
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
>>>>>>> 10f9f24589d4ff616739b028a66ca456ac7b4b24
    }
}

