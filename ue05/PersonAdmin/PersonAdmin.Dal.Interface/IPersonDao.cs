using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Interface {
    public interface IPersonDao {
        Task<IEnumerable<Person>> FindAllAsync();
    }
}
