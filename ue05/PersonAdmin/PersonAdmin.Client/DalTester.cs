using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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

        public async Task TestTransactionsAsync() {
            // Transaktionen sind auf Geschäftslogik-Schicht angesiedelt
            Person person1 = await personDao.FindByIdAsync(1);
            Person person2 = await personDao.FindByIdAsync(2);

            if (person1 == null || person2 == null)
                throw new ArgumentException("person is null");

            DateTime oldDate1 = person1.DateOfBirth;
            DateTime oldDate2 = person2.DateOfBirth;
            DateTime newDate1 = DateTime.MinValue;
            DateTime newDate2 = DateTime.MinValue;

            try {
                using (TransactionScope scope =
                       new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) {
                    person1.DateOfBirth = newDate1 = oldDate1.AddDays(1);
                    person2.DateOfBirth = newDate2 = oldDate2.AddDays(1);
                    await personDao.UpdateAsync(person1);
                    throw new ArgumentException(); // comment this out to rollback transaction
                    await personDao.UpdateAsync(person2);
                    scope.Complete();
                }
            } catch { }

            person1 = await personDao.FindByIdAsync(1);
            person2 = await personDao.FindByIdAsync(2);

            if (oldDate1 == person1.DateOfBirth && oldDate2 == person2.DateOfBirth)
                Console.WriteLine("Transaction was ROLLED BACK.");
            else if (newDate1 == person1.DateOfBirth && newDate2 == person2.DateOfBirth)
                Console.WriteLine("Transaction was COMMITTED.");
            else
                Console.WriteLine("No Transaction.");
        }

    }
}
