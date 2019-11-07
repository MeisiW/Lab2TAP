using System;
using System.Reflection;
using MyAttribute;

namespace Executer
{
    class Executer
    {

        static bool VerifyParameters(ParameterInfo[] formal, object[] actual)
        {
            var paramNumber = formal.Length;
            if (paramNumber != actual.Length)
                return false;
            for(int i = 0; i < paramNumber; i++)
            {
                if (null != actual[i])
                {
                    var type = actual[i].GetType();
                    if (!formal[i].ParameterType.IsAssignableFrom(type))
                        return false;
                }
                else if (formal[i].ParameterType.IsValueType)
                    return false;
            }
            return true;
        }
        public static void Main(string[] args)
        {
            const string mylibraryBinDebugMylibraryDll = @"C:\Users\unieuro\OneDrive\Desktop\Lab2TAP\MyLibrary\bin\Debug\netstandard2.0\MyLibrary.dll";
            var a = Assembly.LoadFrom(mylibraryBinDebugMylibraryDll);
            foreach (var type in a.GetTypes()) //type è uguale ai nomi delle classi in MyLibrary

                if (type.IsClass)  //se type è uguale al nome di una classe o di un delegate
                {
                    Console.WriteLine(type.FullName);  //stampa MyLibrary.Foo...
                    try
                    {
                        var instance = Activator.CreateInstance(type);  //instance è una instance di type(cioè è una instance della classe Foo)
                        foreach (var m in type.GetMethods())  //m è uguale ai nomi dei metodi in type (cioè in Foo) esempio: void M2(int 32)
                        {
                            foreach (var att in m.GetCustomAttributes<ExecuteMe>())  //att è uguale agli attributi trovati in MyLibrary
                            {
                                if (VerifyParameters(m.GetParameters(), att.Arguments))
                                    m.Invoke(instance, att.Arguments); //invoke prende instance (cioè una instance della classe Foo) e gli argomenti degli attributi trovati in MyLibrary e invoca i metodi trovati in Foo
                                else
                                    Console.WriteLine($"Wrong parameters for method {m.Name}");
                            }
                        }
                    }

                    catch (MissingMethodException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                    Console.ReadLine();
                
            }
        }
}
