namespace BlazorMenup.Data;
using MySqlConnector;
using System;
using System.Data;


public class MenuService
{
    private AppDb appDb;
    public MenuService(AppDb db){
        appDb = db;
    } 
    public async Task<MenuItem[]> GetMenuAsync()
    {
        if(appDb.Connection.State != ConnectionState.Open){        
            await appDb.Connection.OpenAsync();
        }

        using var command = new MySqlCommand("SELECT * FROM menuItems;", appDb.Connection);
        await using var reader = await command.ExecuteReaderAsync();
        var menuItems = new List<MenuItem>();

        while (await reader.ReadAsync()){
            var menuItem = new MenuItem()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                Price = reader.GetInt32(3)
            };
            menuItems.Add(menuItem);
        }
        return menuItems.ToArray();
    }
}
