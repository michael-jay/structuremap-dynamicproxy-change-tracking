using System.Collections.Generic;

namespace NonPocoChangeTracking.ChangeTracking
{
    public interface IChangeTrackable
    {
        #region Properties
        bool IsDirty { get; }
        List<ChangedValue> ChangedValues { get; set; }
        #endregion

        #region Methods
        void MarkAsClean();
        string GetChangeLog();
        #endregion
    }
}
