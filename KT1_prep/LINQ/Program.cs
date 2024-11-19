using LINQ.Data;

var repository = new CustomerRepository();
var customers = repository.GetCustomers();


// ########### Alle Kunden die > 1 Mio. Umsatz haben (sortiert nach Umsatz absteigend)
var result1 = customers
    .Where(c => c.Revenue > 1_000_000)
    .OrderByDescending(c => c.Revenue);

var result1_1 =
        from c in customers
        where c.Revenue > 1_000_000
        orderby c.Revenue descending
        select c;

// foreach (var rc in result1_1)
// {
//     Console.Out.WriteLine(rc);
// }


// ########### Alle Kunden die in Österreich sind
var result2 = customers.Where(c => c.Location.Country == "Österreich");

var result2_1 =
    from c in customers
    where c.Location.Country == "Österreich"
    select c;

// foreach (var rc in result2_1)
// {
//     Console.Out.WriteLine(rc);
// }

// ########### Alle Kunden die ein Rating von A haben (sortiert nach Name aufsteigend)
var result3 = customers.Where(c => c.Rating == Rating.A).OrderBy(c => c.Name);

var result3_2 = 
    from c in customers 
    where c.Rating == Rating.A 
    orderby c.Name 
    select c;

// foreach (var rc in result3_2)
// {
//     Console.Out.WriteLine(rc);
// }

// ########### Alle Kunden, deren Namen mit "A" oder "a" beginnt
var result4 = customers.Where(c => c.Name.ToLower().StartsWith("a"));

var result4_1 = 
    from c in customers
    where c.Name.ToLower().StartsWith("a")
    select c;

// foreach (var rc in result4_1)
// {
//     Console.Out.WriteLine(rc);
// }

// ########### Kunde "corous GmbH"
var result5 = customers.Where(c => c.Name == "corous GmbH");

var result5_1 =
    from c in customers
    where c.Name == "corous GmbH"
    select c;

// foreach (var rc in result5)
// {
//     Console.Out.WriteLine(rc);
// }


// ########### Anzahl an Kunden pro Ländern
var result6 = customers
    .GroupBy(c => c.Location.Country)
    .Select(g => new {Country = g.Key, Number = g.Count()});

var result6_1 =
    from c in customers
    group c by c.Location.Country
    into g
    select new {Country = g.Key, Number = g.Count()};

// foreach (var rc in result6_1)
// {
//     Console.Out.WriteLine(rc);
// }


// ########### Drei Kunden mit dem höchsten Umsatz (es ist kein Umsatz doppelt). Aber nur Name, Umsatz und Rating
var result7 = customers
    .OrderByDescending(c => c.Revenue)
    .Take(3)
    .Select(c => new{c.Name, c.Revenue, c.Rating});

var result7_1 =
    (from c in customers
        orderby c.Revenue descending
        select new{ c.Name, c.Revenue, c.Rating }).Take(3);

// foreach (var rc in result7_1)
// {
//     Console.Out.WriteLine(rc);
// }


// ########### Umsatz pro Land
var result8 = customers
    .GroupBy(c => c.Location.Country)
    .Select(g => new {Country = g.Key, AvgRevenue = g.Average(c => c.Revenue)});

var result8_1 =
    from c in customers
    group c by c.Location.Country
    into g
    select new {Country = g.Key, AvgRevenue = g.Average(c => c.Revenue)};

// foreach (var rc in result8_1)
// {
//     Console.Out.WriteLine(rc);
// }


// ########### Durchschnittsumsatz von A-Kunden
var result9 = customers
    .Where(c => c.Rating == Rating.A)
    .Select(c => c.Revenue)
    .Average();

var result9_1 =
    (from c in customers
        where c.Rating == Rating.A
        select c.Revenue).Average();

// Console.Out.WriteLine("Avg of A-Customers: " + result9_1);


// #########################################################################################################################
var studentRepository = new StudentRepository();
var students = studentRepository.getStudents();

// ########### Alle Studenten aus SE (nur Matrikelnummer und Name)
var result10 = students
    .Where(s => s.Subject.ToLower() == "se")
    .Select(s => new {s.MatNr, s.Name});

var result10_1 =
    from s in students
    where s.Subject.ToLower() == "se"
    select new {s.MatNr, s.Name};

// foreach (var rc in result10_1)
// {
//     Console.Out.WriteLine(rc);
// }

// ########### Alle Studenten, die alle LVAs mit mindestens 3 abgeschlossen haben
var result11 = students.Where(s => s.Grades.All(g => g <= 3));

var result11_1 =
    from s in students
    where s.Grades.All(g => g <= 3)
    select s;

// foreach (var rc in result11_1)
// {
//     Console.Out.WriteLine(rc);
// }

// ########### Liste an Namen und Durchschnittsnote (Sortiert nach Durchschnittsnote Aufsteigend)
var result12 = students
    .Select(s => new {s.Name, AvgGrad = s.Grades.Average()}).OrderBy(i => i.AvgGrad);

var result12_1 =
    from s in students
    orderby s.Grades.Average()
    select new {s.Name, AvgGrade = s.Grades.Average()};

// foreach (var rc in result12_1)
// {
//     Console.Out.WriteLine(rc);
// }

// ########### Für Student s12321 eine Notenliste in Langform (Sehr Gut, Gut, ...)
var gradeStrings = new[] { "Sehr gut", "Gut", "Befriedigend", "Genügend", "Nicht genügend" };

var result13 = students.Single(s => s.MatNr == "s12321").Grades?.Select(g => gradeStrings[g-1]).ToList();

var result13_1 =
    from g in (
        from s in students
        where s.MatNr == "s12321"
        select s.Grades).Single()
    select gradeStrings[g - 1];

// foreach (var rc in result13_1)
// {
//     Console.Out.WriteLine(rc);
// }