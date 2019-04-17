using System;

namespace Lind.DI
{
    /// <summary>
    /// 注入一对象.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectionAttribute : Attribute
    {
        public string Named{get;set;}
    }
}