using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Microsoft.Win32;
namespace RightClickShells
{
    [XmlInclude(typeof(RightClickShell))]
    [XmlInclude(typeof(ExecuteAbleShell))]
    [Serializable]
    public class DirectoryShell : RightClickShell,ISerializable
    {
        public List<RightClickShell> Children= new List<RightClickShell>();
        public DirectoryShell()
        {
            this.type = RightClickShellType.DirectoryShell;
        }
        public DirectoryShell(Microsoft.Win32.RegistryKey registryKey)
        {
            //Sub key named shell is a children container
            if(GetTypeOfRegistryKey(registryKey)==RightClickShellType.DirectoryShell)
            {
                RegistryKey ShellKey = registryKey.OpenSubKey(@"shell");
                if (ShellKey != null)
                {
                    foreach (string subkey in ShellKey.GetSubKeyNames())
                    {
                        RegistryKey childkey = ShellKey.OpenSubKey(subkey);
                        switch (GetTypeOfRegistryKey(childkey))
                        {
                            case RightClickShellType.DirectoryShell:
                                Children.Add(new DirectoryShell(childkey));
                                break;
                            case RightClickShellType.ExecutableShell:
                                Children.Add(new ExecuteAbleShell(childkey));
                                break;

                        }
                    }
                }

            }
            
            this.type = RightClickShellType.DirectoryShell;
        }
        DirectoryShell(SerializationInfo info, StreamingContext context):base(info,context)
        {
            this.type = RightClickShellType.DirectoryShell;
            this.Children.Add((RightClickShell)info.GetValue("Children",typeof(RightClickShell)));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Children", Children, Children.GetType());
                   
        }
    }
}
