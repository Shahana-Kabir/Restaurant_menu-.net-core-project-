namespace BlazorMenup.Data;
using System.Collections.Generic;
public class Order
{
    public int? Id { get; set; }
    public Dictionary<MenuItem, int> LineItems { get; set; }

    public Order()
    {
        LineItems = new Dictionary<MenuItem, int>();
    }

    public int Total()
    {
        var totalPrice = 0;
        foreach (var menuItem in LineItems.Keys)
        {
            totalPrice += menuItem.Price * LineItems[menuItem];

        }
        return totalPrice;

    }

} 