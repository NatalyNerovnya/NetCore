using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NetCoreMentoring.Filters
{
    public class LoggingFilter : IAsyncActionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly bool _isParametersNeeded;
        private const string IsParametersLoggingName = "IsParametersLogging";

        public LoggingFilter(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _loggerFactory = loggerFactory;
            _isParametersNeeded = configuration.GetChildren().Any(item => item.Key == IsParametersLoggingName) && configuration.GetValue<bool>(IsParametersLoggingName);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = _loggerFactory.CreateLogger(context.Controller.GetType());

            logger.LogDebug($"Action is started.");
            if (_isParametersNeeded)
            {
                LogParameters(context, logger);
            }

            await next();

            logger.LogDebug($"Action is ended");
        }

        private void LogParameters(ActionExecutingContext context, ILogger logger)
        {
            if (context.ActionArguments.Any())
            {
                var stringBuilder = new StringBuilder("Parameters: ");
                foreach (var parameter in context.ActionArguments)
                {
                    stringBuilder.Append($"{parameter.Key} = {parameter.Value}");
                }

                logger.LogDebug(stringBuilder.ToString());
            }
            else
            {
                logger.LogDebug("Parameterless method.");
            }
        }
    }
}
