using System;

namespace MyAttribute
{
    public class ExecuteMe : Attribute
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
        public object[] Arguments { get; private set; }
            public ExecuteMe(params object[] arguments {
            Arguments = arguments;
        }
    }
}
