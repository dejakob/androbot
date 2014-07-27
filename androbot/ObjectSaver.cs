using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace androbot
{
    class ObjectSaver
    {
        public static void Save(String name, ObservableCollection<AndroProperty> properties, String path)
        {
            path += "\\src\\"+ name + ".java";
            if (File.Exists(path))
            {
                MessageBox.Show("File already exists...");
            }
            else
            {
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine("public class " + name + " {");
                sw.WriteLine(CreateLocals(properties));
                sw.WriteLine(CreateCtor(properties, name));
                sw.WriteLine(CreateGetterAndSetters(properties));
                sw.WriteLine("}");
                sw.Close();
            }
        }

        private static String CreateLocals(IEnumerable<AndroProperty> properties)
        {
            String locals = "";
            foreach (var property in properties)
            {
                locals += "private " + property.Type + " " + property.Name.ToLower() + ";\n";
            }
            return locals;
        }

        private static String CreateCtor(IEnumerable<AndroProperty> properties, String name)
        {
            String ctor = "public " + name + "(";
            String headers = "";
            String content = "";
            foreach (var property in properties)
            {
                if (property.AddToCtor)
                {
                    headers += "," + property.Type + " " + property.Name.ToLower();
                    content += "\tthis." + property.Name.ToLower() + " = " + property.Name.ToLower() + ";\n";
                }
            }
            if (headers.StartsWith(",")) headers = headers.Remove(0, 1);
            ctor += headers;
            ctor += ") {\n";
            ctor += content;
            ctor += "\n}";
            return ctor;
        }

        private static String CreateGetterAndSetters(IEnumerable<AndroProperty> properties)
        {
            String getters = "";
            String setters = "";
            foreach (var property in properties)
            {
                if (property.Getter)
                {
                    getters += "\tpublic " + property.Type + " get" + property.Name + "() {\n";
                    getters += "\t\treturn this." + property.Name.ToLower() + ";\n";  
                    getters += "\t}\n";
                }
                if (property.Setter)
                {
                    setters += "\tpublic void set" + property.Name + "(" + property.Type + " " + property.Name.ToLower() + ") {\n";
                    setters += "\t\tthis." + property.Name.ToLower() +" = " + property.Name.ToLower() + ";\n";
                    setters += "\t}\n";
                }
            }
            getters += "\n" + setters;
            return getters;
        }
    }
}
