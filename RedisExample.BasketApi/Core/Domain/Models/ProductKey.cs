namespace RedisExample.BasketApi.Core.Domain.Models;

public class ProductKey
{
    public string Key { get; set; }
    public string ProductId { get; set; }
    public DateTime ExpireDate { get; set; }
}