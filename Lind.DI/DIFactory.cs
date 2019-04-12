using Autofac;
using Autofac.Builder;
using System;
using System.Linq;
using System.Reflection;

namespace Lind.DI
{

    /// <summary>
    /// DI工厂.
    /// </summary>
    public class DIFactory
    {

        static IContainer container;

        /// <summary>
        /// 手动注入.
        /// </summary>
        /// <returns>The resolve.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Resolve<T>()
        {
            if (container == null)
                throw new ArgumentException("please run DIFactory.Init().");
            return container.Resolve<T>();
        }

        /// <summary>
        /// 手动注入.
        /// </summary>
        /// <returns>The by named.</returns>
        /// <param name="named">Named.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T ResolveByNamed<T>(string named)
        {
            if (container == null)
                throw new ArgumentException("please run DIFactory.Init().");
            return container.ResolveNamed<T>(named);
        }


        /// <summary>
        /// 把对象里的Inject特性的对象注入.
        /// web环境下，应该使用filter拦截器将当前控制器传传InjectFromObject去注入它.
        /// </summary>
        /// <param name="obj">Object.</param>
        public static void InjectFromObject(object obj)
        {
            if (obj.GetType().IsClass && obj.GetType() != typeof(string))
                foreach (var field in obj.GetType().GetFields(
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                {
                    if (field.GetCustomAttributes(false).Select(i => i.GetType())
                    .Contains(typeof(InjectionAttribute)))
                    {
                        InjectionAttribute inject = (InjectionAttribute)field.GetCustomAttributes(false).FirstOrDefault(i => i.GetType() == typeof(InjectionAttribute));
                        if (inject != null && !String.IsNullOrWhiteSpace(inject.Named))
                        {
                            field.SetValue(obj, container.ResolveNamed(inject.Named, field.FieldType));
                        }
                        else
                        {
                            field.SetValue(obj, container.Resolve(field.FieldType));
                        }
                        //递归处理它的内部字段
                        InjectFromObject(field.GetValue(obj));
                    }

                }
        }

        /// <summary>
        /// 初始化.
        /// </summary>
        public static void Init()
        {
            var builder = new ContainerBuilder();
            var arr = AppDomain.CurrentDomain.GetAssemblies()
                 .SelectMany(x => x.GetTypes())
                 .Where(i => i.IsPublic && i.IsClass)
                 .ToList();
            foreach (var type in arr)
            {
                try
                {
                    if (type.GetCustomAttributes(false).Select(i => i.GetType()).Contains(typeof(ComponentAttribute)))
                    {
                        ComponentAttribute componentAttribute = (ComponentAttribute)type.GetCustomAttributes(false).FirstOrDefault(o => o.GetType() == typeof(ComponentAttribute));

                        if (type.GetInterfaces() != null && type.GetInterfaces().Any())
                        {
                            type.GetInterfaces().ToList().ForEach(o =>
                            {
                                registor(builder, type, o, componentAttribute);

                            });
                        }
                        else
                        {
                            registor(builder, type, type, componentAttribute);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception($"Lind.DI init {type.Name} error.");
                }
            }
            container = builder.Build();
        }

        /// <summary>
        /// 注册组件.
        /// </summary>
        /// <param name="builder">Builder.</param>
        /// <param name="typeImpl">Type impl.</param>
        /// <param name="type">Type.</param>
        /// <param name="componentAttribute">Component attribute.</param>
        static void registor(ContainerBuilder builder, Type typeImpl, Type type, ComponentAttribute componentAttribute)
        {

            if (componentAttribute.LifeCycle == LifeCycle.Global)
            {
                if (componentAttribute.Named != null)
                    builder.RegisterType(typeImpl).Named(componentAttribute.Named, type).SingleInstance();
                else
                    builder.RegisterType(typeImpl).As(type).SingleInstance();
            }
            else if (componentAttribute.LifeCycle == LifeCycle.CurrentScope)
            {
                if (componentAttribute.Named != null)
                    builder.RegisterType(typeImpl).Named(componentAttribute.Named, type).InstancePerDependency();
                else
                    builder.RegisterType(typeImpl).As(type).InstancePerDependency();
            }
            else
            {
                if (componentAttribute.Named != null)
                    builder.RegisterType(typeImpl).Named(componentAttribute.Named, type).InstancePerDependency();
                else
                    builder.RegisterType(typeImpl).As(type).InstancePerLifetimeScope();
            }
        }
    }
}
