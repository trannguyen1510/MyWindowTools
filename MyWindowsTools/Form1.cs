using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using RightClickShells;

namespace MyWindowsTools
{
    public partial class Form1 : Form
    {
        RightClickShell cursor;
        DirectoryShell root;
        InsertDeleteManager insert_deleted;
        FileStream fs;
        TreeNode view_root;

        public Form1()
        {
            InitializeComponent();
            try
            {
                root = DeSerializeTree(); //lấy cây đã lưu.

            }
            catch
            {
                fs.Close();
                MessageBox.Show("this is a new tree.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                root = new DirectoryShell() { Name = "Directory\\Background" };// tạo cây mới.
            }
            root.SetParentForChild();// vì quá kém cõi nên phải set cha thủ công, ko thể tự động
            insert_deleted = new InsertDeleteManager((DirectoryShell)root);
            view_root = new TreeNode() { Tag = root, Text = "Root" };
            
            treeView1.Nodes.Add(view_root);
            treeView1.ImageList = new ImageList();
            treeView1.ImageList.Images.Add(new Bitmap(1, 1));
            btnCancel.Hide();
            btnApplyofEdit.Hide();
            CreateViewRoot();
        }

        private void CreateViewRoot()
        {
            Stack<TreeNode> NextChild = new Stack<TreeNode>();
            NextChild.Push(view_root);
            while (NextChild.Count != 0)
            {
                TreeNode current = NextChild.Pop();
                if (((RightClickShell)current.Tag).Type == RightClickShellType.DirectoryShell)
                {
                    foreach (object child in ((DirectoryShell)current.Tag).Children)
                    {
                        TreeNode new_treenode = new TreeNode() { Tag = child, Text = ((RightClickShell)child).Name };
                        if (((RightClickShell)child).HaveIcon !=null && ((RightClickShell)child).HaveIcon != "")
                        {
                            Icon icon = Icon.ExtractAssociatedIcon(((RightClickShell)child).HaveIcon);
                            treeView1.ImageList.Images.Add(icon);
                            new_treenode.SelectedImageIndex = treeView1.ImageList.Images.Count - 1;
                            new_treenode.ImageIndex = treeView1.ImageList.Images.Count - 1;
                        }
                        current.Nodes.Add(new_treenode);
                        NextChild.Push(new_treenode);
                        
                    }
                }

            }
        }

        //private  void PopulateTreeView(TreeView tree_view, string path, char path_separator)
        //{
        //    TreeNode last_node = null;
        //    string sub_path_agg = string.Empty;
        //    foreach (string sub_path in path.Split(path_separator))
        //    {
        //        sub_path_agg += sub_path + path_separator;
        //        TreeNode[] nodes = tree_view.Nodes.Find(key: sub_path_agg, searchAllChildren: true);
        //        if (nodes.Length == 0)
        //        {
        //            if (last_node == null)
        //                last_node = tree_view.Nodes.Add(sub_path_agg, sub_path);
        //            else
        //                last_node = last_node.Nodes.Add(sub_path_agg, sub_path);
        //        }
        //        else
        //            last_node = nodes[0];
        //    }
        //}

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "ProgramFile (*.exe)|*.exe";
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                string[] sub_path = path.Split('\\');
                string folder_path = sub_path[0];
                for (int i = 1; i < sub_path.Length - 1; i++)
                {
                    folder_path += "\\" + sub_path[i];
                }
                
                txtTarget.Text = sub_path[sub_path.Length-1];
                txtSource.Text = folder_path;
            }
        }
        private void btnExpandCollapse_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                bool IsExpand = false;
                foreach (TreeNode node in treeView1.Nodes)
                {
                    if (node.IsExpanded)
                    {
                        IsExpand = true;
                        break;
                    }
                }
                if (IsExpand)
                    treeView1.CollapseAll();
                else
                    treeView1.ExpandAll();
            }
            else
            {
                if (treeView1.SelectedNode.IsExpanded)
                    treeView1.SelectedNode.Collapse();
                else
                    treeView1.SelectedNode.ExpandAll();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SerializeTree();// lưu lại.
            insert_deleted.ApplyChange();// ghi vào registry
            Stack<TreeNode> applied = new Stack<TreeNode>();
            applied.Push(treeView1.Nodes[0]);
            while (applied.Count != 0)
            {
                TreeNode current = applied.Pop();
                if (current.Text[0] == '*')
                {
                    current.Text = current.Text.Substring(1);
                }
                foreach(TreeNode child in current.Nodes)
                {
                    applied.Push(child);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode!= null)
            {
                if (((RightClickShell)treeView1.SelectedNode.Tag).Type != RightClickShellType.DirectoryShell)
                {
                    MessageBox.Show("You Cannot Add into This.", "Error");
                    return;
                }
                RightClickShellType AddType = CheckRadioButton();
                RightClickShell p = (DirectoryShell)treeView1.SelectedNode.Tag;
                object tag = insert_deleted.AddWithInformations(txtName.Text,txtTarget.Text ,txtSource.Text,AddType,ref p,cbIcon.Checked);
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Fill name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                    
                else
                {
                    TreeNode current_node = treeView1.SelectedNode;
                    TreeNode target_node = new TreeNode();
                    target_node.Text = "*" + txtName.Text;
                    target_node.Tag = tag;
                    current_node.Nodes.Add(target_node);
                    if (cbIcon.Checked)
                    {
                        Icon icon = Icon.ExtractAssociatedIcon(txtSource.Text + "\\" + txtTarget.Text);
                        treeView1.ImageList.Images.Add(icon);
                        target_node.SelectedImageIndex = treeView1.ImageList.Images.Count - 1;
                        target_node.ImageIndex = treeView1.ImageList.Images.Count - 1;
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a node", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtName.ResetText();
            txtSource.ResetText();
            txtTarget.ResetText();
            cbIcon.Checked = false; 
        }

        private RightClickShellType CheckRadioButton()
        {
            if (rdBtnDirectory.Checked)
            {
                return RightClickShellType.DirectoryShell;
            }
            if (rdBtnExecutable.Checked)
            {
                return RightClickShellType.ExecutableShell;
            }
            return RightClickShellType.Null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            insert_deleted.Changes.Clear();
            try
            {
                root = DeSerializeTree(); //lấy cây đã lưu.

            }
            catch
            {
                fs.Close();
                MessageBox.Show("this is a new tree.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                root = new DirectoryShell() { Name = "Directory\\Background" };// tạo cây mới.
            }
            root.SetParentForChild();// vì quá kém cõi nên phải set cha thủ công, ko thể tự động.
            cursor = (DirectoryShell)root;// đây là con trỏ hiện tại để tui test.
            insert_deleted = new InsertDeleteManager((DirectoryShell)root);
            view_root = new TreeNode() { Tag = root, Text = "Root" };
            CreateViewRoot();
            treeView1.Nodes.Add(view_root);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            RightClickShell current = ((RightClickShell)treeView1.SelectedNode.Tag);
            insert_deleted.Delete(ref current);
            if (treeView1.SelectedNode.IsSelected)
            {
                TreeNode current_node = treeView1.SelectedNode;
                current_node.Remove();
            }
            else
                MessageBox.Show("Select a node", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }
        public  void SerializeTree()
        {
            File.WriteAllText("BackGroundShortcuts.xml", "");
            fs = new FileStream("BackGroundShortcuts.xml", FileMode.OpenOrCreate);
            XmlSerializer xmlSerializer_background = new XmlSerializer(typeof(DirectoryShell));
            xmlSerializer_background.Serialize(fs, root);
            fs.Close();
        }

        public  DirectoryShell DeSerializeTree()
        {
            XmlSerializer xmlSerializer_background = new XmlSerializer(typeof(DirectoryShell));
            fs = new FileStream("BackGroundShortcuts.xml", FileMode.OpenOrCreate);
            DirectoryShell res = (DirectoryShell)xmlSerializer_background.Deserialize(fs);
            fs.Close();
            return res;
        }
        private object BackEndInsert(String name,RightClickShellType type, String Target,String Source)
        {
            RightClickShell added;
            RightClickShell parent=(DirectoryShell)treeView1.SelectedNode.Tag;
            switch (type)
            {
                case RightClickShellType.DirectoryShell:
                    added = new DirectoryShell() { Name = name };
                    insert_deleted.Add(ref parent, ref added);
                    break;
                case RightClickShellType.ExecutableShell:
                    added = new ExecutableShell() { Name = name, Command = ExecutableShell.CreateCommandFromSorceAndTarget(target: txtTarget.Text,source: txtSource.Text) ,HaveIcon = txtSource.Text+"\\"+txtTarget.Text};
                    insert_deleted.Add(ref parent, ref added);
                    break;
                default:
                    added = new DirectoryShell() { Name = name };
                    throw new Exception("not a intended rightclickshelltype");
            }
            return added;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            TreeNode current_node = treeView1.SelectedNode;
            txtName.Text = current_node.Text;
            if (((RightClickShell)treeView1.SelectedNode.Tag).Type.ToString() == (rdBtnDirectory.Text) + "Shell")
                rdBtnDirectory.Checked = true;
            else
            {
                rdBtnExecutable.Checked = true;
                (txtTarget.Text,txtSource.Text) = ((ExecutableShell)treeView1.SelectedNode.Tag).GetSourceAndTarget();
                cbIcon.Checked = (((ExecutableShell)current_node.Tag).HaveIcon != "");
            }
            rdBtnDirectory.Enabled = false;
            rdBtnExecutable.Enabled = false;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnExpandCollapse.Enabled = false;
            btnRevert.Hide();
            btnApply.Hide();
            btnApplyofEdit.Show();
            btnCancel.Show();
        }

        private void btnIcon_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveIcon_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode.ImageIndex = 0;
            treeView1.SelectedNode.SelectedImageIndex = 0;
        }

        private void btnApplyofEdit_Click(object sender, EventArgs e)
        {
            TreeNode current_node = treeView1.SelectedNode;
            RightClickShellType AddType = CheckRadioButton();
            /* Back-end stuff*/
            RightClickShell current_shell = (RightClickShell)current_node.Tag;
            insert_deleted.EditNode(ref current_shell, name: txtName.Text, target: txtTarget.Text, source: txtSource.Text,cbIcon.Checked);
            current_node.Text = txtName.Text;
            if (cbIcon.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtSource.Text) || string.IsNullOrWhiteSpace(txtTarget.Text))
                {
                    MessageBox.Show("Missing source or target", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Icon icon = Icon.ExtractAssociatedIcon(txtSource.Text + "\\" + txtTarget.Text);
                treeView1.ImageList.Images.Add(icon);
                current_node.SelectedImageIndex = treeView1.ImageList.Images.Count - 1;
                current_node.ImageIndex = treeView1.ImageList.Images.Count - 1;
            }
            else
            {
                treeView1.ImageList.Images.RemoveAt(treeView1.ImageList.Images.Count - 1);
                current_node.SelectedImageIndex = -1;
                current_node.ImageIndex = -1;
            }

            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnExpandCollapse.Enabled = true;
            btnCancel.Hide();
            btnApplyofEdit.Hide();
            btnRevert.Show();
            btnApply.Show();
            rdBtnDirectory.Checked = false;
            rdBtnExecutable.Checked = false;
            rdBtnDirectory.Enabled = true;
            rdBtnExecutable.Enabled = true;
            cbIcon.Checked = false;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnExpandCollapse.Enabled = true;
            btnCancel.Hide();
            btnApplyofEdit.Hide();
            btnRevert.Show();
            btnApply.Show();
            txtName.ResetText();
            txtSource.ResetText();
            txtTarget.ResetText();
            cbIcon.Checked = false;
            if (rdBtnDirectory.Checked == true)
                rdBtnDirectory.Checked = false;
            else rdBtnExecutable.Checked = false;
        }

        private void rdBtnDirectory_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                txtSource.ResetText();
                txtSource.Enabled = false;
                txtTarget.ResetText();
                txtTarget.Enabled = false;
                cbIcon.Checked = false;
                cbIcon.Enabled = false;
                btnBrowse.Enabled = false;
            }
            else
            {
                txtSource.Enabled = true;
                txtTarget.Enabled = true;
                cbIcon.Enabled = true;
                btnBrowse.Enabled = true;
            }
        }
    }
}
