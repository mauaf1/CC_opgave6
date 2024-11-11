using Azure.Data.Tables;
using Azure.Identity;
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


    //    public void OnGet()
    //    {
    //        var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
    //        var tableClient = new TableClient(connectionString, "WeeklyMenu");

    //        foreach (var entity in tableClient.Query<TableEntity>())
    //        {
    //            var menuItem = new MenuItem
    //            {
    //                Day = entity.RowKey,
    //                ColdDish = entity.GetString("ColdDish"),
    //                WarmDish = entity.GetString("WarmDish")
    //            };
    //            MenuItems.Add(menuItem);
    //        }
    //    }
    //}
    public void OnGet()
    {
        // Use DefaultAzureCredential to authenticate with the managed identity
        var credential = new DefaultAzureCredential();

        // Update the TableClient to use your storage account's endpoint directly
        string storageAccountUrl = "https://<ibasbikeproduction4>.table.core.windows.net";
        var tableServiceClient = new TableServiceClient(new Uri(storageAccountUrl), credential);

        var tableClient = tableServiceClient.GetTableClient("WeeklyMenu");

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
