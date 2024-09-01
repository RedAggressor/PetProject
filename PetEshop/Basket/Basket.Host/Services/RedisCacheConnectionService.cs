using Basket.Host.Configurations;
using Basket.Host.Services.Abstractions;
using StackExchange.Redis;

namespace Basket.Host.Services
{
    public class RedisCacheConnectionService : IRedisCacheConnectionService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionLazy;
        private bool _disposed;

        public RedisCacheConnectionService(
            IOptions<RedisConfig> config)
        {
            //var redisConfigurationOptions = new ConfigurationOptions
            //{
            //    EndPoints = { "localhost:6380" },
            //    ConnectTimeout = 5000,
            //};
            //delete after test!!!

            var redisConfigurationOptions = ConfigurationOptions.Parse(config.Value.Host);

            _connectionLazy =
            new Lazy<ConnectionMultiplexer>(()
               => ConnectionMultiplexer.Connect(redisConfigurationOptions));
        }

        public IConnectionMultiplexer Connection => _connectionLazy.Value;

        public void Dispose()
        {
            if (!_disposed)
            {
                Connection.Dispose();
                _disposed = true;
            }
        }
    }
}