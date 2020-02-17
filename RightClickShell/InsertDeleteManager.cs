using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RightClickShells
{
    public class InsertDeleteManager
    {
        private Queue<Tuple<DirectoryShell, RightClickShell,RightClickShellActionType>> changes= new Queue<Tuple<DirectoryShell, RightClickShell, RightClickShellActionType>>();

        public Queue<Tuple<DirectoryShell, RightClickShell, RightClickShellActionType>> Changes { get => changes; }

        public void Revert()
        {
            changes.Clear();
        }

        public InsertDeleteManager(DirectoryShell root)
        {
        }
        /// <summary>
        /// Will list the key to insert to the registry
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="the_inserted"></param>
        public void Add(ref RightClickShell parent,ref RightClickShell the_inserted)
        {
            if (parent.Type == RightClickShellType.ExecutableShell)
                return;
            ((DirectoryShell)parent).Children.Add(the_inserted);
            the_inserted.Parent = (DirectoryShell)parent;
            Changes.Enqueue(new Tuple<DirectoryShell, RightClickShell, RightClickShellActionType>((DirectoryShell)parent, the_inserted, RightClickShellActionType.Add));
        }
        /// <summary>
        /// List of the key to delete in a registry
        /// </summary>
        /// <param name="parent">
        /// 
        /// </param>
        /// <param name="the_deleted">
        /// 
        /// </param>
        public void Delete(ref RightClickShell the_deleted)
        {
            Changes.Enqueue(new Tuple<DirectoryShell, RightClickShell, RightClickShellActionType>(the_deleted.Parent, the_deleted, RightClickShellActionType.Delete));
            the_deleted.Parent.Children.Remove(the_deleted);
        }
        public void ChangeName(ref RightClickShell change_node,String new_name) 
        {
            Changes.Enqueue(new Tuple<DirectoryShell, RightClickShell, RightClickShellActionType>(null, change_node, RightClickShellActionType.ChangeName));
            change_node.Name = new_name;
        }
        public void ChangeCommand(ref ExecutableShell change_node, String new_command)
        {
            Changes.Enqueue(new Tuple<DirectoryShell, RightClickShell, RightClickShellActionType>(null, change_node, RightClickShellActionType.ChangeCommand));
            change_node.Command = new_command;
        }
        /// <summary>
        /// Change A registry and serialize a new tree of shells
        /// </summary>
        public void ApplyChange()
        {
            while (Changes.Count>0)
            {
                DirectoryShell parent;
                RightClickShell affected;
                RightClickShellActionType type;
                (parent,affected,type)=Changes.Dequeue();
                switch (type)
                {
                    case RightClickShellActionType.Add:
                        RegistryAdd(parent, affected);
                        break;
                    case RightClickShellActionType.Delete:
                        RegistryDelete(parent, affected);
                        break;
                    case RightClickShellActionType.ChangeName:
                        RegistryChangeName(affected);
                        break;
                    case RightClickShellActionType.ChangeCommand:
                        RegistryChangeCommand((ExecutableShell)affected);
                        break;
                }
            }
        }

        private void RegistryChangeCommand(ExecutableShell affected)
        {
            RegistryKey x = Registry.ClassesRoot.OpenSubKey(affected.getRegistryPath() + "\\command", true);
            x.SetValue("", affected.Command);
            x.Close();
        }

        private void RegistryChangeName(RightClickShell affected)
        {
            RegistryKey x = Registry.ClassesRoot.OpenSubKey(affected.getRegistryPath(), true);
            x.SetValue("MUIVerb", x.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent">
        /// 
        /// </param>
        /// <param name="affected">
        /// 
        /// </param>
        private void RegistryDelete(DirectoryShell parent, RightClickShell affected)
        {
            RegistryKey root = Registry.ClassesRoot.OpenSubKey(parent.getRegistryPath()+"\\shell", true);
            root.DeleteSubKeyTree(affected.Name);
            root.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent">
        /// 
        /// </param>
        /// <param name="affected">
        /// 
        /// </param>
        private void RegistryAdd(DirectoryShell parent, RightClickShell affected)
        {
            RegistryKey root = Registry.ClassesRoot.OpenSubKey(parent.getRegistryPath()+"\\shell",true);
            if (root != null)
            {
                switch (affected.Type)
                {
                    case RightClickShellType.DirectoryShell:
                        root.CreateSubKey(affected.Name);
                        root = root.OpenSubKey(affected.Name,true);
                        root.SetValue("subcommands", "");
                        root.CreateSubKey("shell");
                        break;
                    case RightClickShellType.ExecutableShell:
                        root.CreateSubKey(affected.Name);
                        root = root.OpenSubKey(affected.Name,true);
                        if (affected.HaveIcon != null)
                        {
                            root.SetValue("Icon", affected.HaveIcon);
                        }
                        root = root.CreateSubKey("command",true);
                        root.SetValue("", ((ExecutableShell)affected).Command);
                        break;
                }
            }
            else
            {
                throw new Exception("Cannot find the registry for Parent with that DirectoryShell");
            }
        }

        
        
    }
}
