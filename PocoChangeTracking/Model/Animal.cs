namespace PocoChangeTracking.Model
{
    public class Animal : Entity
    {
        #region Properties
        public virtual int Age { get; set; }
        public virtual string Type { get; set; }
        #endregion
    }
}
