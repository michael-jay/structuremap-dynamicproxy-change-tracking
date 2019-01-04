namespace NonPocoChangeTracking.Model
{
    public interface IPerson : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int Age { get; set; }
    }

    public class Person : Entity, IPerson
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        #endregion
    }
}
