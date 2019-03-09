using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Settings
{
    public class ConfigurationSettings : ISettings
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ConfigurationSettings> _logger;

        public ConfigurationSettings(IConfiguration configuration, ILogger<ConfigurationSettings> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public int GetMaxProductsNumber()
        {
            var maxProductNumber = _configuration.GetValue<int>("ProductsNumber");
            _logger.LogInformation($"Read ProductsNumber: {maxProductNumber}");

            return maxProductNumber;
        }
    }
}
