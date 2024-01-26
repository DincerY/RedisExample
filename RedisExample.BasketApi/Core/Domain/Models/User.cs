namespace RedisExample.BasketApi.Core.Domain.Models;

public class User
{
    public User(string id)
    {
        Id = id;
    }

    public User()
    {
        
    }
    public string Id { get; set; }
    public List<ProductKey> ProductKeys { get; set; }
}