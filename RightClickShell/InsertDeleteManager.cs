using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightClickShells
{
    public class InsertDeleteManager
    {
        public List<Tuple<String, String>> inserted= new List<Tuple<String, String>>();
        public List<Tuple<String,String>> deleted = new List<Tuple<String, String>>();
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
            inserted.Add(new Tuple<String, String>(parent.getFullPath(),the_inserted.name));
        }
        /// <summary>
        /// List of the key to delete in a registry
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="the_deleted"></param>
        public void Delete(ref RightClickShell the_deleted)
        {
            deleted.Add(new Tuple<String, String>(the_deleted.Parent.getFullPath(),the_deleted.name));
            the_deleted.Parent.Children.Remove(the_deleted);
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
