using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetCoreProject.Domain.Interfaces;

namespace NetCoreProject.Domain.Infrastructure
{
    public class ConfigurationSettings : ISettings
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ConfigurationSettings> _logger;
        private readonly string _maxProductNumberName = "ProductsNumber";

        public ConfigurationSettings(IConfiguration configuration, ILogger<ConfigurationSettings> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public int GetMaximumProductNumber()
        {
            var maxProductNumber = _configuration.GetValue<int>(_maxProductNumberName);
            _logger.LogInformation($"Read {_maxProductNumberName}: {maxProductNumber}");

            return maxProductNumber;

        }
    }
}
