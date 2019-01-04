using System;
using NonPocoChangeTracking.ChangeTracking;

namespace NonPocoChangeTracking.Model
{
    public interface IEntity : IChangeTrackable
    {
        #region Properties
        Guid Id { get; set; }
        #endregion
    }
}
