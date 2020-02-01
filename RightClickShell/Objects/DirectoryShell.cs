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
    [XmlInclude(typeof(RightClickShell))]
    [XmlInclude(typeof(ExecutableShell))]
    [Serializable]
    public class DirectoryShell : RightClickShell, ISerializable
    {
        [XmlElement("Children")]
        public List<RightClickShell> Children { get => children; }

        [XmlIgnore]private List<RightClickShell> children;
        public DirectoryShell()
        {
            this.children = new List<RightClickShell>();
            this.type = RightClickShellType.DirectoryShell;
        }
        public DirectoryShell(Microsoft.Win32.RegistryKey registryKey)
        {
            //Sub key named shell is a children container
            if (GetTypeOfRegistryKey(registryKey) == RightClickShellType.DirectoryShell)
            {
                this.name = registryKey.Name.Split('\\')[registryKey.Name.Split('\\').Length-1];
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
                                Children.Add(new ExecutableShell(childkey));
                                break;

                        }
                    }
                }
            }

            this.type = RightClickShellType.DirectoryShell;
        }
        DirectoryShell(SerializationInfo info, StreamingContext context)
        {
            this.name = info.GetString("Name");
            this.type = RightClickShellType.DirectoryShell;
            this.children = (List<RightClickShell>)info.GetValue("Children", typeof(List<RightClickShell>));
            //this.SetParentForChild();
        }

        public void SetParentForChild()
        {

            foreach (RightClickShell child in children)
            {
                child.Parent = this;
                if (child.Type == RightClickShellType.DirectoryShell)
                {
                    ((DirectoryShell)child).SetParentForChild();
                }
                
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Children", Children, Children.GetType());
        }
        [OnDeserialized()]
        public void KnowingFather(StreamingContext context)
        {
            Console.WriteLine(context.Context);
            Console.WriteLine(context.State);
            //FileStream fs = new FileStream("Output.txt", FileMode.OpenOrCreate);
            //byte[] state_bytes = new UTF8Encoding(true).GetBytes(context.State.ToString() + "\n");
            //byte[] Context_bytes = new UTF8Encoding(true).GetBytes(context.Context.ToString() + "\n");
            //fs.Write(state_bytes, 0, state_bytes.Length);
            //fs.Write(Context_bytes, state_bytes.Length, Context_bytes.Length);
            //fs.Close();
        }
    }
}
