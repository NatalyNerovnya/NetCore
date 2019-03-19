using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetCoreMentoring.Filters
{
    public class LoggingFilterFactory : IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new LoggingFilter(serviceProvider.GetService<ILoggerFactory>(), serviceProvider.GetService<IConfiguration>());
        }

        public bool IsReusable { get; }
    }
}
