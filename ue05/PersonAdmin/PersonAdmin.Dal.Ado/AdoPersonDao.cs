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
    }
}
