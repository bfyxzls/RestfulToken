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

        public Type Intercepted { get; set; }

        public InterceptType InterceptType { get; set; }
    }
    public enum InterceptType
    {
        /// <summary>
        /// 类拦截
        /// </summary>
        Class,
        /// <summary>
        /// 接口拦截
        /// </summary>
        Interface,
    }
}