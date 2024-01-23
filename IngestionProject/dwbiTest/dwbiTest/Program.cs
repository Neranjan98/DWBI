// See https://aka.ms/new-console-template for more information
using dwbiTest.data;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using RestSharp;
using System.ComponentModel.DataAnnotations.Schema;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var restClient = new RestClient();
        var request = new RestRequest("https://data.wa.gov/resource/f6w7-q2d2.json");
        var respose = await restClient.GetAsync(request);
        var responseDTOs = JsonConvert.DeserializeObject<List<responseDTO>>(respose.Content);

        await InsertRecords(responseDTOs);

        Console.ReadKey();
    }

    private static async Task InsertRecords(List<responseDTO> responseDTOs)
    {
        using (var context = new CloudDBContext())
        {
            foreach (var item in responseDTOs)
            {
                int.TryParse(item.model_year, out int year);
                var dto = new interimDto()
                {
                    id = Guid.NewGuid(),
                    city = item.city,
                    vin = item.vin_1_10,
                    make = item.make,
                    model = item.model,
                    model_year = year
                };
                await context.AddAsync(dto);
            }

            await context.SaveChangesAsync();
        }
    }
}

[Table("tbVehicle")]
public class interimDto
{
    public Guid id { get; set; }
    public string? vin { get; set; }
    public string? city { get; set; }
    public string? make { get; set; }
    public string? model { get; set; }
    public int? model_year { get; set; }

}