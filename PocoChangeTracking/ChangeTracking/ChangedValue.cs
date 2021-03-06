﻿using System;

namespace PocoChangeTracking.ChangeTracking
{
    public class ChangedValue
    {
        #region Properties
        public string PropertyName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
        public DateTime ChangedAt { get; set; }
        #endregion

        #region Base Class Overrides
        public override string ToString()
        {
            return $"{PropertyName} | {OldValue ?? "null"} -> {NewValue ?? "null"} @ {ChangedAt}";
        }
        #endregion
    }
}
