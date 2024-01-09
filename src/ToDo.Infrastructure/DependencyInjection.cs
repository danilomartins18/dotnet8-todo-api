using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using ToDo.Application.AutoMapper;
using ToDo.Application.Interfaces.Repositories;
using ToDo.Application.Interfaces.Services;
using ToDo.Application.Services;
using ToDo.Infrastructure.Configurations;
using ToDo.Infrastructure.Data.Repositories;

namespace ToDo.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, ILoggingBuilder logging)
		{
			AddTelemetry(services, configuration, logging);
			AddAutoMapper(services);
			AddConfigurations(services, configuration);
			AddRepositories(services);
			AddServices(services);
			AddMongoDb(services);

			return services;
		}

		private static void AddAutoMapper(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(TodoMappingProfile));
		}

		private static void AddConfigurations(IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDb"));
		}

		private static void AddMongoDb(IServiceCollection services)
		{
			BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(MongoDB.Bson.BsonType.String));
			services.AddSingleton(provider => provider.GetRequiredService<IOptions<MongoDbConfiguration>>().Value);
		}

		private static void AddServices(IServiceCollection services)
		{
			services.AddTransient<IToDoService, ToDoService>();
		}

		private static void AddRepositories(IServiceCollection services)
		{
			services.AddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));
		}

		private static void AddTelemetry(IServiceCollection services, IConfiguration configuration, ILoggingBuilder logging)
		{
			var resourceBuilder = ResourceBuilder.CreateDefault()
									.AddService(
										serviceName: OpenTelemetryConfiguration.ServiceName,
										serviceVersion: OpenTelemetryConfiguration.ServiceVersion
									);
			
			services.AddOpenTelemetry()
				  .ConfigureResource(resource => resource.AddService(OpenTelemetryConfiguration.ServiceName))
				  .WithTracing(tracing =>
				  {
					  tracing
							.AddSource(OpenTelemetryConfiguration.ServiceName)
							.AddAspNetCoreInstrumentation()
							.AddOtlpExporter()
							.SetResourceBuilder(resourceBuilder);
				  })
				  .WithMetrics(metrics =>
				  {
					  metrics
							.AddMeter("todo-api")
							.AddAspNetCoreInstrumentation()
							.AddOtlpExporter()
							.SetResourceBuilder(resourceBuilder);
				  });

			logging.ClearProviders();
			logging.AddOpenTelemetry(options =>
			{
				options
					.SetResourceBuilder(resourceBuilder)
					.AddOtlpExporter();
			});
		}
	}
}
