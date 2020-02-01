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
                MessageBox.Show("this is a new tree.","Caution",MessageBoxButtons.OK,MessageBoxIcon.Information);
                root = new DirectoryShell() { Name = "Directory\\Background" };// tạo cây mới.
            }
            root.SetParentForChild();// vì quá kém cõi nên phải set cha thủ công, ko thể tự động.
            cursor = (DirectoryShell)root;// đây là con trỏ hiện tại để tui test.
            insert_deleted = new InsertDeleteManager((DirectoryShell)root);
            view_root = new TreeNode() { Tag = root,Text = "Root"};
            CreateViewRoot();
            treeView1.Nodes.Add(view_root);
        }

        private void CreateViewRoot()
        {
            Stack<TreeNode> NextChild = new Stack<TreeNode>();
            NextChild.Push(view_root);
            while (NextChild.Count != 0)
            {
                TreeNode current = NextChild.Pop();
                if(((RightClickShell)current.Tag).Type == RightClickShellType.DirectoryShell)
                {
                    foreach (object child in ((DirectoryShell)current.Tag).Children)
                    {
                        TreeNode new_treenode = new TreeNode() { Tag = child, Text = ((RightClickShell)child).Name};
                        current.Nodes.Add(new_treenode);
                        NextChild.Push(new_treenode);
                    }
                }
                
            }
        }

        private  void PopulateTreeView(TreeView tree_view, string path, char path_separator)
        {
            TreeNode last_node = null;
            string sub_path_agg = string.Empty;
            foreach (string sub_path in path.Split(path_separator))
            {
                sub_path_agg += sub_path + path_separator;
                TreeNode[] nodes = tree_view.Nodes.Find(key: sub_path_agg, searchAllChildren: true);
                if (nodes.Length == 0)
                {
                    if (last_node == null)
                        last_node = tree_view.Nodes.Add(sub_path_agg, sub_path);
                    else
                        last_node = last_node.Nodes.Add(sub_path_agg, sub_path);
                }
                else
                    last_node = nodes[0];
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "ProgramFile (*.exe)|*.exe";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtSource.Text = openFileDialog1.FileName;
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
            
            if(((RightClickShell)treeView1.SelectedNode.Tag).Type != RightClickShellType.DirectoryShell)
            {
                MessageBox.Show("You Cannot Add into This.", "Error");
                return;
            }
            RightClickShellType AddType = CheckRadioButton();
            object tag = BackEndInsert(txtName.Text, AddType);
            if (treeView1.SelectedNode.IsSelected)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Fill name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                    
                else
                {
                    TreeNode current_node = treeView1.SelectedNode;
                    current_node.Nodes.Add(new TreeNode("*"+txtName.Text) { Tag = tag});
                }
            }
            else
            {
                MessageBox.Show("Select a node", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                

            
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
            if (treeView1.SelectedNode.IsSelected)
            {
                TreeNode current_node = treeView1.SelectedNode;
                current_node.Remove();
            }
            else
                MessageBox.Show("Select a node", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void CursorAccess(ref RightClickShell cursor)
        {
            PrintMap(cursor);
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case -2:
                    break;
                case -1:
                    cursor = cursor.Parent;
                    break;
                default:
                    cursor = ((DirectoryShell)cursor).Children[input];
                    break;
            }
        }

        private void PrintMap(RightClickShell cursor)
        {
            if (cursor.Type == RightClickShellType.ExecutableShell)
                return;
            foreach (RightClickShell x in ((DirectoryShell)cursor).Children)
            {
                Console.WriteLine(((DirectoryShell)cursor).Children.IndexOf(x).ToString() + ". " + x.Name);
            }
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
        private  void Delete()
        {
            DirectoryShell p = cursor.Parent;
            insert_deleted.Delete(ref cursor);
            //cursor = p;
            //cursor.Parent.Children.Remove(cursor);
        }

        private object BackEndInsert(String name,RightClickShellType type)
        {
            RightClickShell added;
            switch (type)
            {
                case RightClickShellType.DirectoryShell:
                    added = new DirectoryShell() { Name = name };
                    insert_deleted.Add(ref cursor, ref added);
                    break;
                case RightClickShellType.ExecutableShell:
                    added = new ExecutableShell() { Name = name, Command = InsertDeleteManager.CreateCommandFromSorceAndTarget(target: txtTarget.Text,source: txtSource.Text) };
                    insert_deleted.Add(ref cursor, ref added);
                    break;
                default:
                    added= added = new DirectoryShell() { Name = name };
                    throw new Exception("not a intended rightclickshelltype");
            }
            return added;
        }
        public  void PrintTree(DirectoryShell root, String level_header = "")
        {
            RightClickShell current;
            Stack<RightClickShell> queue = new Stack<RightClickShell>();
            queue.Push(root);
            while (queue.Count > 0)
            {
                current = queue.Pop();
                switch (current.Type)
                {
                    case RightClickShellType.DirectoryShell:
                        foreach (RightClickShell sub in ((DirectoryShell)current).Children)
                        {
                            queue.Push(sub);
                        }
                        break;
                    default:
                        break;

                }
                Console.WriteLine(current.getRegistryPath());

            }

        }

        public  string GetLevel(RightClickShell x)
        {
            String res = string.Empty;
            RightClickShell current = x.Parent;
            while (current != null)
            {
                res = current.Name + "\\" + res;
                current = current.Parent;
            }
            return res;
        }

        private  void PrintMenu()
        {
            Console.Write("Chọn 1 trong những tính năng sau:0\n" +
                          "1. Thêm vào\n" +
                          "2. Xóa \n" +
                          "0. Thoat\n" +
                          "---------------------------------\n");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Function is not complete yet");
        }
    }
}
