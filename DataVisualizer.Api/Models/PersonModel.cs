namespace DataVisualizer.Api.Models;

public class Person
{
    public int Seq { get; set; }
    public required string NameFirst { get; set; }
    public required string NameLast { get; set; }
    public int Age { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public required string CCNumber { get; set; }
}
