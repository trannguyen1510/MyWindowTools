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
        [XmlIgnore] protected String name;


        [XmlAttribute] public virtual String Name { get => name; set =>name= value; }
        //[XmlAttribute] public String icon;
        //[XmlAttribute] public String position;
        //[XmlAttribute] public String ShowWhileHold;
        protected DirectoryShell parent = null;
        [XmlIgnore] public virtual DirectoryShell Parent { get => parent; set => parent = value; }
        protected RightClickShellType type;
        [XmlIgnore] public virtual RightClickShellType Type { get => type;}


        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", name, name.GetType());
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
            String res = "\\" + this.name;
            DirectoryShell trace_back = this.Parent;
            while (trace_back != null)
            {
                res = "\\" + trace_back.name +"\\shell" + res;
                trace_back = trace_back.Parent;
            }
            return res;
        }
    }
}
