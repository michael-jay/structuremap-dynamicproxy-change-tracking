using Castle.DynamicProxy;
using StructureMap;
using StructureMap.Building.Interception;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using StructureMap.TypeRules;
using System;
using System.Linq;
using IInterceptor = StructureMap.Building.Interception.IInterceptor;

/*
 * This class is based on the DefaultConventionScanner that is part of the StructureMap code base:
 * https://github.com/structuremap/structuremap/blob/a6bf0af54e1b29a8944dded56303ce29f391ae23/src/StructureMap/Graph/DefaultConventionScanner.cs
 */

namespace NonPocoChangeTracking.ChangeTracking
{
    public class DefaultConventionWithProxyScanner : ConfigurableRegistrationConvention
    {
        #region Member Variables
        private readonly ProxyGenerator _proxy = new ProxyGenerator();
        private readonly Type _genericInterceptorPolicyType = typeof(InterceptorPolicy<>);
        private readonly Type _genericInterceptorType = typeof(ChangeTrackingFuncInterceptor<>);
        #endregion

        #region Base Class Overrides
        public override void ScanTypes(TypeSet types, Registry registry)
        {
            types.FindTypes(TypeClassification.Concretes).Where(type => type.HasConstructors()).ToList().ForEach(type =>
            {
                Type pluginType = FindPluginType(type);

                if(pluginType == null)
                    return;

                registry.AddType(pluginType, type);
                registry.Policies.Interceptors(CreatePolicy(pluginType));

                ConfigureFamily(registry.For(pluginType));
            });
        }

        public virtual Type FindPluginType(Type concreteType)
        {
            return concreteType.GetInterfaces().FirstOrDefault(t => t.Name == $"I{concreteType.Name}");
        }
        #endregion

        #region Utility Methods
        private IInterceptorPolicy CreatePolicy(Type pluginType)
        {
            return (IInterceptorPolicy)Activator.CreateInstance(
                _genericInterceptorPolicyType.MakeGenericType(pluginType), 
                CreateInterceptor(pluginType), null);
        }

        private IInterceptor CreateInterceptor(Type pluginType)
        {
            return (IInterceptor)Activator.CreateInstance(
                _genericInterceptorType.MakeGenericType(pluginType), 
                _proxy);
        }
        #endregion
    }
}
