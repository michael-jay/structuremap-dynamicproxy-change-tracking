using System;
using NonPocoChangeTracking.ChangeTracking;
using NonPocoChangeTracking.Model;
using StructureMap;

namespace NonPocoChangeTracking
{
    class Program
    {
        private readonly IContainer _container = new Container();

        private void Init()
        {
            _container.Configure(_ =>
            {
                _.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.AddAllTypesOf<IEntity>();
                    s.Convention<DefaultConventionWithProxyScanner>();
                });
            });
        }

        private void DemoPerson()
        {
            IPerson person = _container.GetInstance<IPerson>();

            person.LastName = "Smith";
            person.LastName = "Peterson";
            person.FirstName = "Bob";

            if(person.IsDirty)
                Console.WriteLine(person.GetChangeLog());
        }

        private void DemoAnimal()
        {
            IAnimal animal = _container.GetInstance<IAnimal>();

            animal.Age = 2;
            animal.Type = "cat";

            if(animal.IsDirty)
                Console.WriteLine(animal.GetChangeLog());
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
