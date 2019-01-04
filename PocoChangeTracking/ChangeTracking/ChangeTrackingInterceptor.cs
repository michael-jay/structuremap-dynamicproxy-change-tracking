using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Castle.DynamicProxy;

namespace PocoChangeTracking.ChangeTracking
{
    public class ChangeTrackingInterceptor : IInterceptor
    {
        #region Member Variables
        private readonly Regex _setterRegex = 
            new Regex(
                @"set_(?<property>\w+)", 
                RegexOptions.Singleline | RegexOptions.Compiled);
        #endregion

        #region IInterceptor Implementation
        public void Intercept(IInvocation invocation)
        {
            Match match = _setterRegex.Match(invocation.Method.Name);

            // we only want to intercept set calls
            if(!match.Success)
            {
                invocation.Proceed();
                return;
            }

            IChangeTrackable obj = (IChangeTrackable)invocation.InvocationTarget;
            string property = match.Groups["property"].Value;
            MethodInfo getterMethod = invocation.TargetType.GetMethod($"get_{property}");

            // get the old and new values
            object oldValue = getterMethod != null ? getterMethod.Invoke(invocation.InvocationTarget, null) : "?";
            object newValue = invocation.Arguments[0];

            // check to see if something changed
            if(!AreEqual(oldValue, newValue))
            {
                if(obj.ChangedValues == null)
                    obj.ChangedValues = new List<ChangedValue>();

                obj.ChangedValues.Add(
                    new ChangedValue
                    {
                        PropertyName = property,
                        OldValue = oldValue,
                        NewValue = newValue,
                        ChangedAt = DateTime.Now
                    });
            }

            invocation.Proceed();
        }
        #endregion

        #region Utility Methods
        private bool AreEqual(object a, object b)
        {
            if(a == null && b == null)
                return true;

            if(a == null || b == null)
                return false;

            return a.Equals(b);
        }
        #endregion
    }
}
