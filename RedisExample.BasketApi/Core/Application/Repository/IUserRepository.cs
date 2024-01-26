using RedisExample.BasketApi.Core.Domain.Models;

namespace RedisExample.BasketApi.Core.Application.Repository;

public interface IUserRepository
{
    Task<User> GetUserAsync(string userId);

    IEnumerable<string> GetUsers();

    Task<User> UpdateUserProductAsync(User user);

    Task<bool> DeleteUserAsync(string userId);
}