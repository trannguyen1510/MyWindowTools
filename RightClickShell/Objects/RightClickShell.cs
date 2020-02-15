using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Microsoft.Win32;
using System.IO;

namespace RightClickShells
{
    //[XmlInclude(typeof(Directory))]
    [Serializable]
    public abstract class RightClickShell
    {
        static public int count=0;
        [XmlIgnore] protected String id="";
        [XmlAttribute] public virtual String ID { get => id; set => id = value; }
        [XmlIgnore] protected String name;
        [XmlAttribute] public virtual String Name { get => name; set =>name= value; }
        [XmlAttribute] public String HaveIcon = null;
        //[XmlAttribute] public String position;
        //[XmlAttribute] public String ShowWhileHold;
        protected DirectoryShell parent = null;
        [XmlIgnore] public virtual DirectoryShell Parent { get => parent; set => parent = value; }
        protected RightClickShellType type;
        [XmlIgnore] public virtual RightClickShellType Type { get => type;}


        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ID,id.GetType());
            info.AddValue("Name", name, name.GetType());
            info.AddValue("HaveIcon", HaveIcon, typeof(String));
            //info.AddValue("Icon", name, name.GetType());
            //info.AddValue("Position", position, position.GetType());
            //info.AddValue("ShowWhileHold", ShowWhileHold, ShowWhileHold.GetType());
        }
        
        protected virtual RightClickShellType GetTypeOfRegistryKey(RegistryKey input)
        {
            foreach (string s in input.GetSubKeyNames())
            {
                if (s == "shell" || s == "Shell")
                    return RightClickShellType.DirectoryShell;
                if (s == "command" || s == "Command")
                {
                    return RightClickShellType.ExecutableShell;
                }
            }
            return RightClickShellType.Null;
        }
        public String getFullPath()
        {
            String res = "\\" + this.name;
            DirectoryShell trace_back = this.Parent;
            while (trace_back != null)
            {
                res = "\\" + trace_back.name + res;
                trace_back = trace_back.Parent;
            }
            return res;
        }
        public String getRegistryPath()
        {
            String res = "";
            if (this.id != "")
            {
                res = "\\" + this.id;
                DirectoryShell trace_back = this.Parent;
                while (trace_back.Parent != null)
                {
                    res = "\\" + trace_back.id + "\\shell" + res;
                    trace_back = trace_back.Parent;
                }
                res =trace_back.name + "\\shell" + res;
            }
            else
            {
                res =this.name;
            }
            return res;
        }
    }
}
