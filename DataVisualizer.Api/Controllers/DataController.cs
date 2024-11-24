using Microsoft.AspNetCore.Mvc;
using DataVisualizer.Api.Models;
using DataVisualizer.Api.Services;

namespace DataVisualizer.Api.Controllers;

[ApiController]
[Route("api/data")]
public class DataController : ControllerBase
{
    private readonly CsvService _csvService;

    public DataController(CsvService csvService)
    {
        _csvService = csvService;
    }

    [HttpGet]
    // getAll data fra csv filen
    public IActionResult GetAllData()
    {
        var data = _csvService.LoadCsvData();
        return Ok(data);
    }

    [HttpGet("search")]
    // search etter firstName i csv filen eks: /api/endpoint?firstName=John
    public IActionResult SearchByName([FromQuery] string firstName)
    {
        var data = _csvService.LoadCsvData();
        var results = data.Where(p => p.NameFirst.Equals(firstName, StringComparison.OrdinalIgnoreCase));
        return Ok(results);
    }

    [HttpGet("filter")]
    //search etter stat i csv filen eks: /api/endpoint?state=Florida
    public IActionResult FilterByState([FromQuery] string state)
    {
        var data = _csvService.LoadCsvData();
        var results = data.Where(p => p.State.Equals(state, StringComparison.OrdinalIgnoreCase));
        return Ok(results);
    }

    [HttpGet("statistics")]

    public IActionResult GetStatistics()
    {
        var data = _csvService.LoadCsvData();
            var stateStatistics = data.GroupBy(p => p.State)
                               .Select(g => new
                               {
                                   State = g.Key,
                                   AverageAge = g.Average(p => p.Age),
                                   MostCommonFirstName = g.GroupBy(p => p.NameFirst)
                                                          .OrderByDescending(nameGroup => nameGroup.Count())
                                                          .FirstOrDefault()?.Key,
                                   MostCommonLastName = g.GroupBy(p => p.NameLast)
                                                         .OrderByDescending(lastNameGroup => lastNameGroup.Count())
                                                         .FirstOrDefault()?.Key
                               })
                               .OrderBy(stat => stat.State); 
    return Ok(stateStatistics);
    }
}
