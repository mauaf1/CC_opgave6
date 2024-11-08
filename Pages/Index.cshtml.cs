using Azure.Data.Tables;
using IBAS_kantine;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    public List<MenuItem> MenuItems { get; set; } = new();

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        var tableClient = new TableClient(connectionString, "WeeklyMenu");

        foreach (var entity in tableClient.Query<TableEntity>())
        {
            var menuItem = new MenuItem
            {
                Day = entity.RowKey,
                ColdDish = entity.GetString("ColdDish"),
                WarmDish = entity.GetString("WarmDish")
            };
            MenuItems.Add(menuItem);
        }
    }
}
