namespace BlazorMenup.Data;

using MySqlConnector;
using System;
using System.Data;


public class OrderService
{
    private AppDb appDb;
    public OrderService(AppDb db)
    {
        appDb = db;
    }
    public async Task ConfirmOrderAsync(Order order)
    {
        if (appDb.Connection.State != ConnectionState.Open)
        {
            await appDb.Connection.OpenAsync();
        }
        var id = await InsertOrderAsync();
        order.Id = id;
        await InsertLineItemsAsync(order);
    }
    public async Task<int> InsertOrderAsync()
    {
        using var cmd = appDb.Connection.CreateCommand();
        cmd.CommandText = @"INSERT INTO `orders` VALUES ();";
        await cmd.ExecuteNonQueryAsync();
        var id = (int)cmd.LastInsertedId;
        return id;
    }

    public async Task InsertLineItemsAsync(Order order)
    {
        using var cmd = appDb.Connection.CreateCommand();
        cmd.CommandText = @"INSERT INTO `lineItems` (`OrderId`, `MenuItemId`, `Quantity`) VALUES (@orderId, @menuItemId, @quantity);";
        cmd.Parameters.Add(new MySqlParameter
        {
            ParameterName = "@orderId",
            DbType = DbType.Int32,
            Value = order.Id,
        });

        var menuItemParam = new MySqlParameter
        {
            ParameterName = "@menuItemId",
            DbType = DbType.Int32,
        };

        cmd.Parameters.Add(menuItemParam);

        var quantityParam = new MySqlParameter
        {
            ParameterName = "@quantity",
            DbType = DbType.Int32,
        };
        cmd.Parameters.Add(quantityParam);

        foreach (var menuItem in order.LineItems.Keys)
        {
            menuItemParam.Value = menuItem.Id;
            quantityParam.Value = order.LineItems[menuItem];

            await cmd.ExecuteNonQueryAsync();
        }
    }

}
