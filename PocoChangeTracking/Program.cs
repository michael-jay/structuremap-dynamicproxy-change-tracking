using System;
using PocoChangeTracking.ChangeTracking;
using PocoChangeTracking.Model;
using StructureMap;

namespace PocoChangeTracking
{
    class Program
    {
        private readonly IContainer _container = new Container();

        private IChangeTracker _tracker;

        private void Init()
        {
            _container.Configure(_ =>
            {
                _.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.WithDefaultConventions();
                });

                _.For<IChangeTracker>().Singleton();
            });

            _tracker = _container.GetInstance<IChangeTracker>();
        }

        private void DemoPerson()
        {
            Person person = _tracker.GenerateProxy<Person>();

            person.LastName = "Smith";
            person.LastName = "Peterson";
            person.FirstName = "Bob";

            if(person.IsDirty)
                Console.WriteLine(person.GetChangeLog());

            person.MarkAsClean();
        }

        private void DemoAnimal()
        {
            Animal animal = _tracker.GenerateProxyFrom(
                new Animal
                {
                    Age = 1,
                    Type = "dog"
                });

            animal.Age = 2;
            animal.Type = "cat";

            if(animal.IsDirty)
                Console.WriteLine(animal.GetChangeLog());

            animal.MarkAsClean();
        }

        static void Main()
        {
            Program program = new Program();

            program.Init();

            program.DemoPerson();
            Console.WriteLine();

            program.DemoAnimal();
            Console.WriteLine();

            Console.Write("Press ENTER to exit: ");
            Console.ReadLine();
        }
    }
}
