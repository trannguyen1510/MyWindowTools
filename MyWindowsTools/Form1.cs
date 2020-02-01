using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MyWindowsTools
{
    public partial class Form1 : Form
    {
        private TreeView treeView;

        public TreeView tree_view { get => treeView; set => treeView = value; }

        public Form1()
        {
            InitializeComponent();
        }

        private static void PopulateTreeView(TreeView tree_view, string path, char path_separator)
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
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                lblTarget.Text = openFileDialog1.FileName;
            }
            Form1_Load(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = txtSource.Text;
            treeView1.PathSeparator = @"\";
            PopulateTreeView(treeView1, path, '\\');
            tree_view = treeView1;
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
            tree_view = treeView1;
        }

        private void btnAddfolder_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.IsSelected)
            {
                string t;
                if (string.IsNullOrWhiteSpace(txtName.Text))
                    MessageBox.Show("Fill name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    TreeNode current_node = treeView1.SelectedNode;
                    current_node.Nodes.Add(new TreeNode(txtName.Text));
                }
            }
            else
                MessageBox.Show("Select a node", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (tree_view == null)
            //{
            //    MessageBox.Show("There is no saved Tree", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else
            //{
            //    treeView1 = tree_view;
            //}
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
    }
}
