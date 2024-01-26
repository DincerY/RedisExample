using Newtonsoft.Json;
using RedisExample.BasketApi.Core.Application.Repository;
using RedisExample.BasketApi.Core.Domain.Models;
using StackExchange.Redis;

namespace RedisExample.BasketApi.Infrastracture.Repository;

public class UserRepository : IUserRepository
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;
    

    public UserRepository(ConnectionMultiplexer redis)
    {
        _redis = redis;
        _database = _redis.GetDatabase();
    }

    public async Task<User> GetUserAsync(string userId)
    {
        var dataProdcutKey =await _database.StringGetAsync(userId);
        if (dataProdcutKey.IsNullOrEmpty)
        {
            return null;
        }

        User user = new(userId)
        {
            ProductKeys = JsonConvert.DeserializeObject<List<ProductKey>>(dataProdcutKey)
        };

        return user;
    }


    public IEnumerable<string> GetUsers()
    {
        var server = GetServer();
        var data = server.Keys();

        return data?.Select(x => x.ToString());
    }


    public async Task<User> UpdateUserProductAsync(User user)
    {
        bool created = await _database.StringSetAsync(user.Id, JsonConvert.SerializeObject(user.ProductKeys));

        if (!created)
        {
            return null;
        }

        return await GetUserAsync(user.Id);
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        return await _database.KeyDeleteAsync(userId);
    }

    private IServer GetServer()
    {
        var endpoint = _redis.GetEndPoints();
        return _redis.GetServer(endpoint.First());
    }
}