using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightClickShells
{
    public class InsertDeleteManager
    {
        public List<Tuple<DirectoryShell, RightClickShell>> inserted= new List<Tuple<DirectoryShell, RightClickShell>>();
        public List<Tuple<DirectoryShell,RightClickShell>> deleted = new List<Tuple<DirectoryShell, RightClickShell>>();
        public InsertDeleteManager(DirectoryShell root)
        {
        }
        /// <summary>
        /// Will list the key to insert to the registry
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="the_inserted"></param>
        public void Add(ref DirectoryShell parent,ref RightClickShell the_inserted)
        {
            parent.Children.Add(the_inserted);
            the_inserted.Parent = parent;
            inserted.Add(new Tuple<DirectoryShell, RightClickShell>(parent,the_inserted));
        }
        /// <summary>
        /// List of the key to delete in a registry
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="the_deleted"></param>
        public void Delete(ref DirectoryShell parent, ref RightClickShell the_deleted)
        {
            deleted.Add(new Tuple<DirectoryShell, RightClickShell>(parent,the_deleted));
            parent.Children.Remove(the_deleted);
        }       
        /// <summary>
        /// Change A registry and serialize a new tree of shells
        /// </summary>
        public void ApplyChange()
        {
            //Do some thing with registry
        }
    }
}
