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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTreeview = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panelFuntion = new System.Windows.Forms.Panel();
            this.panelButton = new System.Windows.Forms.Panel();
            this.rdBtnExecutable = new System.Windows.Forms.RadioButton();
            this.btnRevert = new System.Windows.Forms.Button();
            this.rdBtnDirectory = new System.Windows.Forms.RadioButton();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnExpandCollapse = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelView = new System.Windows.Forms.Panel();
            this.cbIcon = new System.Windows.Forms.CheckBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTarget = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApplyofEdit = new System.Windows.Forms.Button();
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
            this.panelButton.Controls.Add(this.btnApplyofEdit);
            this.panelButton.Controls.Add(this.btnCancel);
            this.panelButton.Controls.Add(this.rdBtnExecutable);
            this.panelButton.Controls.Add(this.btnRevert);
            this.panelButton.Controls.Add(this.rdBtnDirectory);
            this.panelButton.Controls.Add(this.btnApply);
            this.panelButton.Controls.Add(this.btnExpandCollapse);
            this.panelButton.Controls.Add(this.btnDelete);
            this.panelButton.Controls.Add(this.btnEdit);
            this.panelButton.Controls.Add(this.btnAdd);
            this.panelButton.Location = new System.Drawing.Point(6, 206);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(489, 235);
            this.panelButton.TabIndex = 4;
            // 
            // rdBtnExecutable
            // 
            this.rdBtnExecutable.AutoSize = true;
            this.rdBtnExecutable.Location = new System.Drawing.Point(16, 57);
            this.rdBtnExecutable.Name = "rdBtnExecutable";
            this.rdBtnExecutable.Size = new System.Drawing.Size(98, 21);
            this.rdBtnExecutable.TabIndex = 6;
            this.rdBtnExecutable.TabStop = true;
            this.rdBtnExecutable.Text = "Exacutable";
            this.rdBtnExecutable.UseVisualStyleBackColor = true;
            // 
            // btnRevert
            // 
            this.btnRevert.Location = new System.Drawing.Point(35, 179);
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(185, 38);
            this.btnRevert.TabIndex = 9;
            this.btnRevert.Text = "Revert";
            this.btnRevert.UseVisualStyleBackColor = true;
            this.btnRevert.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdBtnDirectory
            // 
            this.rdBtnDirectory.AutoSize = true;
            this.rdBtnDirectory.Location = new System.Drawing.Point(16, 20);
            this.rdBtnDirectory.Name = "rdBtnDirectory";
            this.rdBtnDirectory.Size = new System.Drawing.Size(86, 21);
            this.rdBtnDirectory.TabIndex = 6;
            this.rdBtnDirectory.TabStop = true;
            this.rdBtnDirectory.Text = "Directory";
            this.rdBtnDirectory.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(289, 179);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(169, 38);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnExpandCollapse
            // 
            this.btnExpandCollapse.Location = new System.Drawing.Point(289, 20);
            this.btnExpandCollapse.Name = "btnExpandCollapse";
            this.btnExpandCollapse.Size = new System.Drawing.Size(169, 43);
            this.btnExpandCollapse.TabIndex = 7;
            this.btnExpandCollapse.Text = "Expand/Collapse";
            this.btnExpandCollapse.UseVisualStyleBackColor = true;
            this.btnExpandCollapse.Click += new System.EventHandler(this.btnExpandCollapse_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(289, 105);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(169, 38);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(35, 105);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(185, 38);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(120, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 58);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelView
            // 
            this.panelView.Controls.Add(this.cbIcon);
            this.panelView.Controls.Add(this.txtTarget);
            this.panelView.Controls.Add(this.txtSource);
            this.panelView.Controls.Add(this.txtName);
            this.panelView.Controls.Add(this.lblName);
            this.panelView.Controls.Add(this.lblTarget);
            this.panelView.Controls.Add(this.lblSource);
            this.panelView.Controls.Add(this.btnBrowse);
            this.panelView.Location = new System.Drawing.Point(3, 3);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(495, 197);
            this.panelView.TabIndex = 3;
            // 
            // cbIcon
            // 
            this.cbIcon.AutoSize = true;
            this.cbIcon.Location = new System.Drawing.Point(241, 150);
            this.cbIcon.Name = "cbIcon";
            this.cbIcon.Size = new System.Drawing.Size(88, 21);
            this.cbIcon.TabIndex = 8;
            this.cbIcon.Text = "With Icon";
            this.cbIcon.UseVisualStyleBackColor = true;
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(111, 107);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(218, 22);
            this.txtTarget.TabIndex = 5;
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(111, 66);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(218, 22);
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
            this.lblTarget.Location = new System.Drawing.Point(35, 112);
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
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(345, 66);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(93, 63);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(155, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 38);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnApplyofEdit
            // 
            this.btnApplyofEdit.Location = new System.Drawing.Point(253, 179);
            this.btnApplyofEdit.Name = "btnApplyofEdit";
            this.btnApplyofEdit.Size = new System.Drawing.Size(92, 38);
            this.btnApplyofEdit.TabIndex = 11;
            this.btnApplyofEdit.Text = "Apply";
            this.btnApplyofEdit.UseVisualStyleBackColor = true;
            this.btnApplyofEdit.Click += new System.EventHandler(this.btnApplyofEdit_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panelTreeview.ResumeLayout(false);
            this.panelFuntion.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.panelButton.PerformLayout();
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
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnExpandCollapse;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnRevert;
        private System.Windows.Forms.RadioButton rdBtnExecutable;
        private System.Windows.Forms.RadioButton rdBtnDirectory;
        private System.Windows.Forms.CheckBox cbIcon;
        private System.Windows.Forms.Button btnApplyofEdit;
        private System.Windows.Forms.Button btnCancel;
    }
}

