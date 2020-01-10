using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Microsoft.Win32;
namespace RightClickShell
{
    [XmlInclude(typeof(Directory))]
    [Serializable]
    public class RightClickShell :ISerializable
    {
        [XmlAttribute]protected String name;
        [XmlAttribute]protected String icon;
        [XmlAttribute]protected String position;
        [XmlAttribute] protected String ShowWhileHold;
        [NonSerialized]public RightClickShellType type;
        public RightClickShell()
        {
   
        }
        public RightClickShell(SerializationInfo info, StreamingContext context)
        {
            name = info.GetString("Name");
            icon = info.GetString("Icon");
            ShowWhileHold = info.GetString("ShowWhileHold");
            position = info.GetString("position");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue("Name", name, name.GetType());
            //info.AddValue("Icon", name, name.GetType());
            //info.AddValue("Position", position, position.GetType());
            //info.AddValue("ShowWhileHold", ShowWhileHold, ShowWhileHold.GetType());
        }
        protected RightClickShellType GetTypeOfRegistryKey(RegistryKey input)
        {
            foreach (string s in input.GetSubKeyNames())
            {
                if (s == "shell" || s == "Shell")
                    return RightClickShellType.Directory;
                if (s == "command" || s == "command")
                {
                    return RightClickShellType.Executable;
                }
            }
            return RightClickShellType.Null;
        }
    }
    
}
