using Castle.DynamicProxy;

namespace PocoChangeTracking.ChangeTracking
{
    public class ChangeTracker : IChangeTracker
    {
        #region Member Variables
        private readonly ProxyGenerator _proxy = new ProxyGenerator();
        #endregion

        #region IChangeTracker Implimentation
        public TProxy GenerateProxy<TProxy>() where TProxy : class, IChangeTrackable
        {
            return _proxy.CreateClassProxy<TProxy>(new ChangeTrackingInterceptor());
        }

        public TProxy GenerateProxyFrom<TProxy>(TProxy target) where TProxy : class, IChangeTrackable
        {
            return _proxy.CreateClassProxyWithTarget(target, new ChangeTrackingInterceptor());
        }
        #endregion
    }
}
