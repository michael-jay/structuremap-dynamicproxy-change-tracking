namespace PocoChangeTracking.Model
{
    public class Person : Entity
    {
        #region Properties
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }
        #endregion
    }
}
