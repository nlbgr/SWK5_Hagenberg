﻿using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Simple;

public class SimplePersonDao : IPersonDao {
  private static IList<Person> personList = new List<Person>
  {
    new (id: 1, firstName: "John", lastName: "Doe",        dateOfBirth: DateTime.Now.AddYears(-10)),
    new (id: 2, firstName: "Jane", lastName: "Doe",        dateOfBirth: DateTime.Now.AddYears(-20)),
    new (id: 3, firstName: "Max",  lastName: "Mustermann", dateOfBirth: DateTime.Now.AddYears(-30))
  };

  public Task<IEnumerable<Person>> FindAllAsync() {
      return Task.FromResult<IEnumerable<Person>>(personList);
  }

  public Task<Person?> FindByIdAsync(int id) {
      throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Person p) {
      throw new NotImplementedException();
  }


  public Task InsertAsync(Person p) {
      throw new NotImplementedException();
  }
}
