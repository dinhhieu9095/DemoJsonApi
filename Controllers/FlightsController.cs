using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private string GetJsonDataFromPath(string path)
    {
        // Specify the path to your JSON file (e.g., inside the App_Data folder)
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), path);

        // Read the file content
        return System.IO.File.ReadAllText(jsonFilePath);
    }

    [HttpPost("searchmin")]
    public IActionResult searchmin()
    {
        var jsonData = GetJsonDataFromPath("JsonTemplate/1.Search/Search-Min-Response.js");
        return Ok(jsonData);
    }

    [HttpPost("search")]
    public IActionResult search([FromBody] JsonElement model)
    {
        if (model.TryGetProperty("ListFlight", out JsonElement listFlightElement))
        {
            // Check if ListFlight is an array and get its length
            if (listFlightElement.ValueKind == JsonValueKind.Array)
            {
                int listLength = listFlightElement.GetArrayLength();
                string jsonData = "";
                if (listLength == 1)
                {
                    jsonData = GetJsonDataFromPath("JsonTemplate/1.Search/Search-Oneway-Response.js");
                } else
                if (listLength == 2)
                {
                    jsonData = GetJsonDataFromPath("JsonTemplate/1.Search/Search-RoundTrip-Response.js");
                } else {
                    jsonData = GetJsonDataFromPath("JsonTemplate/1.Search/Search-Mutil-Response.js");
                }

                return Ok(jsonData);
            }
        }
        return BadRequest("");
        
    }

    [HttpPost("searchmonth")]
    public IActionResult searchmonth()
    {
        var jsonData = GetJsonDataFromPath("JsonTemplate/1.Search/Search-Month-Response.js");
        return Ok(jsonData);
    }
}