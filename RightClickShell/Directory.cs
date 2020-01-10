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
    [XmlInclude(typeof(ExecuteAble))]
    [Serializable]
    public class Directory : RightClickShell,ISerializable
    {
        public List<RightClickShell> Children= new List<RightClickShell>();
        public Directory()
        {
            this.type = RightClickShellType.Directory;
        }
        public Directory(Microsoft.Win32.RegistryKey registryKey)
        {
            //Sub key named shell is a children container
            if(GetTypeOfRegistryKey(registryKey)==RightClickShellType.Directory)
            {
                RegistryKey ShellKey = registryKey.OpenSubKey(@"shell");
                if (ShellKey != null)
                {
                    foreach (string subkey in ShellKey.GetSubKeyNames())
                    {
                        RegistryKey childkey = ShellKey.OpenSubKey(subkey);
                        switch (GetTypeOfRegistryKey(childkey))
                        {
                            case RightClickShellType.Directory:
                                Children.Add(new Directory(childkey));
                                break;
                            case RightClickShellType.Executable:
                                Children.Add(new ExecuteAble(childkey));
                                break;

                        }
                    }
                }

            }
            
            this.type = RightClickShellType.Directory;
        }
        Directory(SerializationInfo info, StreamingContext context):base(info,context)
        {
            this.type = RightClickShellType.Directory;
            this.Children.Add((RightClickShell)info.GetValue("Children",typeof(RightClickShell)));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Children", Children, Children.GetType());
                   
        }
    }
}
