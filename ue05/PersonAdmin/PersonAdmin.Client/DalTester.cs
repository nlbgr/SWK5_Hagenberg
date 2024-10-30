using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.Client {
    internal class DalTester {
        private IPersonDao personDao;

        public DalTester(IPersonDao personDao) {
            this.personDao = personDao;
        }

        public async Task TestFindAllAsync() {
            (await personDao.FindAllAsync()).ToList().ForEach(p => Console.WriteLine($"{p.Id,5} | {p.FirstName,-10} | {p.LastName,-15} | {p.DateOfBirth,-5:yyyy-MM-dd}"));
        }

        public async Task TestFindByIdAsync() {
            Person? p1 = await personDao.FindByIdAsync(1);
            Console.WriteLine($"FindById(1) --> {p1?.ToString() ?? "null"}");
        }

        public async Task TestUpdateAsync() {
            Person? person = await personDao.FindByIdAsync(1);
            Console.WriteLine($"before update: person -> {person?.ToString() ?? "<null>"}");
            if (person == null) return;

            person.DateOfBirth = DateTime.Now.AddYears(-100);
            await personDao.UpdateAsync(person);

            person = await personDao.FindByIdAsync(1);
            Console.WriteLine($"after update:  person -> {person?.ToString() ?? "<null>"}");
        }

        public async Task TestInsertAsync() {
            Person newPerson = new Person (
                id: -1,
                firstName: "John",
                lastName: "Doe",
                dateOfBirth: DateTime.Now
            );
            await personDao.InsertAsync(newPerson);
            Console.WriteLine($"person inserted -> {newPerson}");
        }

    }
}
