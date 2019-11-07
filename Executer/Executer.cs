using System;
using System.Reflection;
using MyAttribute;

namespace Executer
{
    class Executer
    {
        public static void Main(string[] args)
        {
            const string mylibraryBinDebugMylibraryDll = @"C:\Users\unieuro\OneDrive\Desktop\Lab2TAP\MyLibrary\bin\Debug\netstandard2.0\MyLibrary.dll";
            var a = Assembly.LoadFrom(mylibraryBinDebugMylibraryDll);
            foreach (var type in a.GetTypes()) //type è uguale ai nomi delle classi in MyLibrary
             
                if (type.IsClass)  //se type è uguale al nome di una classe o di un delegate
                {
                    var instance = Activator.CreateInstance(type);  //instance è una instance di type(cioè è una instance della classe Foo)
                    foreach (var m in type.GetMethods())  //m è uguale ai nomi dei metodi in type (cioè in Foo) esempio: void M2(int 32)
                    {
                        foreach (var att in m.GetCustomAttributes<ExecuteMe>())  //att è uguale agli attributi trovati in MyLibrary
                        {
                            m.Invoke(instance, att.Arguments); //invoke prende instance (cioè una instance della classe Foo) e gli argomenti degli attributi trovati in MyLibrary e invoca i metodi trovati in Foo
                        }
                    }


                    Console.WriteLine(type.FullName); //stampa MyLibrary.Foo
                    Console.ReadLine();
                }
            }
        }
}
