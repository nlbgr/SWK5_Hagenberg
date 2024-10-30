using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Interface {
    public interface IPersonDao {
        Task<IEnumerable<Person>> FindAllAsync();
        Task<Person?> FindByIdAsync(int id);
    }
}
