using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using DataVisualizer.Api.Models;

namespace DataVisualizer.Api.Services;

public class CsvService
{
    //Fil pathen til csv-filen
    private readonly string _filePath = "45784.csv";

    public IEnumerable<Person> LoadCsvData()
    {
        // leser filen og mapper til Person-objekter
        using var reader = new StreamReader(_filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        //Registrerer klassen PersonMap og mapper kolonnene til riktig property av Person-objektet
        csv.Context.RegisterClassMap<PersonMap>();

        // Automatisk konverterer hver rad i CSV rad i person objektet basert på liste Person-objekter
        return csv.GetRecords<Person>().ToList();
    }
}

public class PersonMap : ClassMap<Person>
{
    public PersonMap()
    {
        //Mapper kolonnene til riktig property
        Map(p => p.Seq).Name("seq");
        Map(p => p.NameFirst).Name("name/first");
        Map(p => p.NameLast).Name("name/last");
        Map(p => p.Age).Name("age");
        Map(p => p.Street).Name("street");
        Map(p => p.City).Name("city");
        Map(p => p.State).Name("state");
        Map(p => p.Latitude).Name("latitude");
        Map(p => p.Longitude).Name("longitude");
        Map(p => p.CCNumber).Name("ccnumber");
    }
}
