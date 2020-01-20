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
        public Queue<Tuple<DirectoryShell, RightClickShell,RightClickShellActionType>> changes= new Queue<Tuple<DirectoryShell, RightClickShell, RightClickShellActionType>>();
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
            if (parent.type == RightClickShellType.ExecutableShell)
                return;
            ((DirectoryShell)parent).Children.Add(the_inserted);
            the_inserted.Parent = (DirectoryShell)parent;
            changes.Enqueue(new Tuple<DirectoryShell, RightClickShell, RightClickShellActionType>((DirectoryShell)parent, the_inserted, RightClickShellActionType.Add));
        }
        /// <summary>
        /// List of the key to delete in a registry
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="the_deleted"></param>
        public void Delete(ref RightClickShell the_deleted)
        {
            changes.Enqueue(new Tuple<DirectoryShell, RightClickShell, RightClickShellActionType>(the_deleted.Parent, the_deleted, RightClickShellActionType.Delete));
            the_deleted.Parent.Children.Remove(the_deleted);
        }
        /// <summary>
        /// Change A registry and serialize a new tree of shells
        /// </summary>
        public void ApplyChange()
        {
            while (changes.Count>0)
            {
                DirectoryShell parent;
                RightClickShell affected;
                RightClickShellActionType type;
                (parent,affected,type)=changes.Dequeue();
                switch (type)
                {
                    case RightClickShellActionType.Add:
                        RegistryAdd(parent, affected);
                        break;
                    case RightClickShellActionType.Delete:
                        RegistryDelete(parent, affected);
                        break;
                }
            }
        }

        private void RegistryDelete(DirectoryShell parent, RightClickShell affected)
        {
            throw new NotImplementedException();
        }

        private void RegistryAdd(DirectoryShell parent, RightClickShell affected)
        {
            RegistryKey root = Registry.ClassesRoot.OpenSubKey(parent.getRegistryPath()+"\\shell",true);
            if (root != null)
            {
                switch (affected.type)
                {
                    case RightClickShellType.DirectoryShell:
                        root.CreateSubKey(affected.name);
                        root = root.OpenSubKey(affected.name,true);
                        root.SetValue("subcommands", "");
                        root.CreateSubKey("shell");
                        break;
                    case RightClickShellType.ExecutableShell:
                        root.CreateSubKey(affected.name);
                        root = root.OpenSubKey(affected.name,true);
                        root = root.CreateSubKey("command",true);
                        root.SetValue("", "My command");
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
