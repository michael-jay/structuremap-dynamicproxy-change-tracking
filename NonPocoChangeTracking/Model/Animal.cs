namespace NonPocoChangeTracking.Model
{
    public interface IAnimal : IEntity
    {
        int Age { get; set; }
        string Type { get; set; }
    }

    public class Animal : Entity, IAnimal
    {
        #region Properties
        public int Age { get; set; }
        public string Type { get; set; }
        #endregion
    }
}
