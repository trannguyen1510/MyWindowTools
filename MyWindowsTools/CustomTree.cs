using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsTools
{
    delegate void TreeVisitor<T>(T node_data);
    class CustomTree<T>
    {

        private T Data;
        private LinkedList<CustomTree<T>> Children;

        public CustomTree(T data)
        {
            Data = data;
            Children = new LinkedList<CustomTree<T>>();
        }

        public void AddChild (T data)
        {
            if (Check(data))
                Children.AddLast(new CustomTree<T>(data));
            else
            {
                // Add what happens when data conflicts
            }
        }

        public bool Check (T data)
        {
            foreach (CustomTree<T> child in Children)
                /*Add your condition here */ return false;
            return true;
        }

        public void Traverse (CustomTree<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.Data);
            foreach (CustomTree<T> child in node.Children)
                Traverse(child, visitor);
        }

    }
}
