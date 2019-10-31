using System;
using System.Reflection;
using MyAttribute;
using MyLibrary;

namespace Executer
{
    class Executer
    {
        public static void Main(string[] args)
        {
            var a = Assembly.LoadFrom("MyLibrary.dll");
            foreach (var type in a.GetTypes())
                if (type.IsClass)
                {
                    var instance = Activator.CreateInstance(type);
                    foreach(var m in type.GetMethods()) 
                    {
                       foreach(var att in m.GetCustomAttributes<ExecuteMe>())
                        {
                            m.Invoke(instance, att.Arguments);
                        }
                    }


                    Console.WriteLine(type.FullName);
                    Console.ReadLine();
                }
        }
    }
}
