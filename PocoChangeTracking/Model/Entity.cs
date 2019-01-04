using System;
using System.Collections.Generic;
using System.Linq;
using PocoChangeTracking.ChangeTracking;

namespace PocoChangeTracking.Model
{
    public abstract class Entity : IChangeTrackable
    {
        #region Properties
        public virtual Guid Id { get; set; }
        #endregion

        #region Wrapper Properties
        public virtual bool IsDirty => ChangedValues.Any();
        #endregion

        #region Navigation Properties
        public virtual List<ChangedValue> ChangedValues { get; set; } = new List<ChangedValue>();
        #endregion

        #region Utility Methods
        public void MarkAsClean()
        {
            ChangedValues.Clear();
        }

        public string GetChangeLog()
        {
            return ChangedValues
                .OrderBy(cv => cv.ChangedAt)
                .Select(cv => cv.ToString())
                .Aggregate(
                    (Index:0,Desc:$"{GetType().Name} Change Log:"), 
                    (a, c) => (++a.Index, $"{a.Desc}{Environment.NewLine}{a.Index,2}. {c}"))
                .Desc;
        }
        #endregion
    }
}
