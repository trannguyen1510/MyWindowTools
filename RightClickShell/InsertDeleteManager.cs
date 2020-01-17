using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightClickShells
{
    class InsertDeleteManager
    {
        List<Tuple<DirectoryShell, RightClickShell>> inserted= new List<Tuple<DirectoryShell, RightClickShell>>();
        List<Tuple<DirectoryShell,RightClickShell>> deleted = new List<Tuple<DirectoryShell, RightClickShell>>();
        RightClickShell Current;
        InsertDeleteManager(RightClickShell root)
        {
            Current = root;
        }
        public void Add(ref DirectoryShell parent,ref RightClickShell the_inserted)
        {
            parent.Children.Add(the_inserted);
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
