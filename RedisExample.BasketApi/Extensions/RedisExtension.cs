using StackExchange.Redis;

namespace RedisExample.BasketApi.Extensions;

public static class RedisExtension
{
    public static ConnectionMultiplexer ConfigureRedis(this IServiceProvider services, IConfiguration configuration)
    {
        var redisConfig = ConfigurationOptions.Parse(configuration["RedisSettings:ConnectionString"], true);

        redisConfig.ResolveDns = true;

        return ConnectionMultiplexer.Connect(redisConfig);
    }
}