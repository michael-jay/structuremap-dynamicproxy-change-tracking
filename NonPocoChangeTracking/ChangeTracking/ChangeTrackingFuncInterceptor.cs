using Castle.DynamicProxy;
using StructureMap.Building.Interception;

namespace NonPocoChangeTracking.ChangeTracking
{
    public class ChangeTrackingFuncInterceptor<T> : FuncInterceptor<T> where T : class
    {
        #region Constructor
        public ChangeTrackingFuncInterceptor(ProxyGenerator proxy)
            : base(x => proxy.CreateInterfaceProxyWithTarget(x, new ChangeTrackingInterceptor()))
        {
        }
        #endregion
    }
}
