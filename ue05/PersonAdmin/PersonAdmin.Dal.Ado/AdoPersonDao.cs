using System.Data;
using Dal.Common;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Ado {
    public class AdoPersonDao : IPersonDao {
        private Person MapRowToPerson(IDataRecord row) {
            return new Person(
                id: (int)row["Id"],
                firstName: (string)row["first_name"],
                lastName: (string)row["last_name"],
                dateOfBirth: (DateTime)row["date_of_birth"]
            );
        }

        private readonly AdoTemplate template;

        public AdoPersonDao(IConnectionFactory connectionFactory) {
            this.template = new(connectionFactory);
        }

        public async Task<IEnumerable<Person>> FindAllAsync() {
            return await template.QueryAsync<Person>("SELECT * FROM PERSON", MapRowToPerson);
        }

        public async Task<Person?> FindByIdAsync(int id) {
            return await template.QuerySingleAsnync<Person>("SELECT * FROM PERSON WHERE id=@id", MapRowToPerson, new QueryParameter("@id", id));
        }

        public async Task<bool> UpdateAsync(Person p) {
            return await template.ExecuteAsync(
                "UPDATE PERSON SET first_name=@fn,  last_name=@ln, date_of_birth=@dob WHERE id=@id",
                new QueryParameter("@fn", p.FirstName),
                new QueryParameter("@ln", p.LastName),
                new QueryParameter("@dob", p.DateOfBirth),
                new QueryParameter("@id", p.Id)
            ) == 1;
        }
    }
}
