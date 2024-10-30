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
    }
}
