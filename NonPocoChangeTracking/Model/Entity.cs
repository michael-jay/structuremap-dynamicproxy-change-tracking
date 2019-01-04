using System;
using System.Collections.Generic;
using System.Linq;
using NonPocoChangeTracking.ChangeTracking;

namespace NonPocoChangeTracking.Model
{
    public abstract class Entity : IEntity
    {
        #region Properties
        public Guid Id { get; set; }
        #endregion

        #region Wrapper Properties
        public bool IsDirty => ChangedValues.Any();
        #endregion

        #region Navigation Properties
        public List<ChangedValue> ChangedValues { get; set; } = new List<ChangedValue>();
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
