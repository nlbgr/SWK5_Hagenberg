using LinqSamples.Data;

void PrintHeader(string topic, char ch = '-', int length = 80) {
    int lengthLeft = (length - topic.Length - 2) / 2;
    int lengthRight = length - topic.Length - 2 - lengthLeft;

    if (lengthLeft >= 1 && lengthRight >= 1) {
        Console.WriteLine($"{new string(ch, lengthLeft)} {topic} {new string(ch, lengthRight)}");
    } else {
        Console.WriteLine(topic);
    }
}

void Print(IEnumerable<Customer> customers) {
    foreach (var customer in customers){
        Console.WriteLine(customer);
    }
}

var repository = new CustomerRepository();
var customers = repository.GetCustomers();

PrintHeader("Kunden mit > 1 Mio. Jahresumsatz");

var customersByRevenue = 
    from c in customers
    where c.Revenue > 1_000_000
    orderby c.Revenue descending 
    select c;

Print(customersByRevenue);

PrintHeader("Kunden deren Namen mit 'A' oder 'a' beginnt");

var customersByName = 
    from c in customers
    where c.Name.StartsWith("A", StringComparison.InvariantCultureIgnoreCase)
    select c;

Print(customersByName);

PrintHeader("Kunde Famia");

var customer = 
    customers.FirstOrDefault(c => c.Name.ToLower().Equals("famia"));
if (customer is not null){
    Console.WriteLine($"Kunde \"famia\": {customer}");
}

PrintHeader("Kunden in Österreich");

var customersByCountry = customers.Where(c => c.Location.Country.Equals("Österreich"));

Print(customersByCountry);

PrintHeader("Top 3 largest customers");

var largestCustomers =
    customers.OrderByDescending(c => c.Revenue).Take(3);

Print(largestCustomers);

PrintHeader("Durchschnittlicher Umsatz von A-Kunden");

var revenueOfACustomers = customers.Where(c => c.Rating.Equals(Rating.A));
if (revenueOfACustomers.Any()) {
    Console.WriteLine($"Durchschnittlicher Umsatz von A-Kunden: {revenueOfACustomers.Average(c => c.Revenue):N2} Euro");
}

PrintHeader("Gruppierung nach Land");

var customersPerCountry =
    from c in customers
    group c by c.Location.Country
    into g
    select new {
        Country = g.Key,
        Customers = (IEnumerable<Customer>)g
    };

// alternativ
//var customersPerCountry = customers.GroupBy(c => c.Location.Country).Select(g => new { Country = g.Key, Customers = (IEnumerable<Customer>)g });

foreach (var group in customersPerCountry.OrderBy(g => g.Country)){
    Console.WriteLine($"Country: {group.Country}");
    foreach (var c in group.Customers){
        Console.WriteLine($"\t{c}");
    }
}

PrintHeader("Umsatz je Land");

var revenueByCountry = customers.GroupBy(c => c.Location.Country).Select(g => new {Country = g.Key, AverageRevenue = g.Average(c => c.Revenue)});

foreach (var countryData in revenueByCountry.OrderBy(cd => cd.Country)){
    Console.WriteLine($"{countryData.Country}: {countryData.AverageRevenue:N2} Euro");
}