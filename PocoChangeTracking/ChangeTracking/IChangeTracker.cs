namespace PocoChangeTracking.ChangeTracking
{
    public interface IChangeTracker
    {
        TProxy GenerateProxy<TProxy>() where TProxy : class, IChangeTrackable;
        TProxy GenerateProxyFrom<TProxy>(TProxy target) where TProxy : class, IChangeTrackable;
    }
}
