using Dal.Common;
using Microsoft.Extensions.Configuration;
using PersonAdmin.Client;
using PersonAdmin.Dal.Ado;
using PersonAdmin.Dal.Simple;

static void PrintTitle(string text = "", int length = 60, char ch = '-') {
  int preLen = (length - (text.Length + 2)) / 2;
  int postLen = length - (preLen + text.Length + 2);
  Console.WriteLine($"{new String(ch, preLen)} {text} {new String(ch, postLen)}");
}

// Concrete top level components should only be created in one place
// Usually a container is responsible for that
// Here we create the component tree in the main program

//var tester1 = new DalTester(new SimplePersonDao());

//PrintTitle("SimplePersonDao.FindAllAsync");
//await tester1.TestFindAllAsync();

IConfiguration configuration = ConfigurationUtil.GetConfiguration();
IConnectionFactory connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "PersonDbConnection", "ProviderName");

var tester1 = new DalTester(new AdoPersonDao(connectionFactory));

PrintTitle("AdoPersonDao.FindAllAsync");
await tester1.TestFindAllAsync();

PrintTitle("AdoPersonDao.FindAllAsync");
await tester1.TestFindByIdAsync();

PrintTitle("AdoPersonDao.UpdateAsync");
await tester1.TestUpdateAsync();

PrintTitle("AdoPersonDao.InsertAsync");
await tester1.TestInsertAsync();