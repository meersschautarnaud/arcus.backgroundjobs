﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Arcus.BackgroundJobs.AzureActiveDirectory;
using GuardNet;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions on the <see cref="IServiceCollection"/> to add ClientSecretExpiration background jobs.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="ClientSecretExpirationJob"/> scheduled job
        /// which will query Azure Active Directory for applications that have expired or soon to be expired secrets and send a CloudEvent to an Event Grid Topic.
        /// </summary>
        /// <param name="services">The services to add the background job to.</param>
        /// <param name="configureOptions">The optional additional customized user configuration of options for this background job.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> is <c>null</c>.</exception>
        public static IServiceCollection AddClientSecretExpirationJob(
            this IServiceCollection services, 
            Action<ClientSecretExpirationJobOptions> configureOptions = null)
        {
            Guard.NotNull(services, nameof(services));

            return services.AddScheduler(builder =>
            {
                builder.AddJob<ClientSecretExpirationJob, ClientSecretExpirationJobSchedulerOptions>(options =>
                {
                    var additionalOptions = new ClientSecretExpirationJobOptions();
                    configureOptions?.Invoke(additionalOptions);

                    options.SetUserOptions(additionalOptions);
                });
                builder.UnobservedTaskExceptionHandler = (sender, args) =>  UnobservedExceptionHandler(args, services);
            });
        }

        private static void UnobservedExceptionHandler(UnobservedTaskExceptionEventArgs eventArgs, IServiceCollection services)
        {
            ServiceDescriptor logger = services.FirstOrDefault(service => service.ServiceType == typeof(ILogger));
            var loggerInstance = (ILogger) logger?.ImplementationInstance;

            loggerInstance?.LogCritical(eventArgs.Exception, "Unhandled exception in job {JobName}", nameof(ClientSecretExpirationJob));
            eventArgs.SetObserved();
        }
    }
}
