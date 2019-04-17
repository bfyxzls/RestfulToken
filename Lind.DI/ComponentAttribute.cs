using System;

namespace Lind.DI
{
    /// <summary>
    /// 注册组件特性.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public LifeCycle LifeCycle { get; set; } = LifeCycle.CurrentScope;

        public String Named { get; set; }
    }
}