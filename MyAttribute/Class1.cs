using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ExecuteMe : Attribute
    {
        
        public object[] Arguments { get; private set; }
            public ExecuteMe(params object[] arguments {
            Arguments = arguments;
        }
    }
}
