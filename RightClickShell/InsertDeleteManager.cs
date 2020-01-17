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
        DirectoryShellCursor Cursor;
        public InsertDeleteManager(DirectoryShell root)
        {
            Cursor = new DirectoryShellCursor(root);
        }
        public void Add(ref DirectoryShell parent,ref RightClickShell the_inserted)
        {
            parent.Children.Add(the_inserted);
            the_inserted.Parent = parent;
            inserted.Add(new Tuple<DirectoryShell, RightClickShell>(parent,the_inserted));
        }
        public void Delete(ref DirectoryShell parent, ref RightClickShell the_deleted)
        {
            deleted.Add(new Tuple<DirectoryShell, RightClickShell>(parent,the_deleted));
            parent.Children.Remove(the_deleted);
        }       
        public void ApplyChange()
        {

        }
    }
}
