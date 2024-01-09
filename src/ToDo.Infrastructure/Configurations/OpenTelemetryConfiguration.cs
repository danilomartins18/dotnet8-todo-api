using System.Diagnostics;

namespace ToDo.Infrastructure.Configurations
{
	public static class OpenTelemetryConfiguration
	{
		public static string ServiceName { get; }
		public static string ServiceVersion { get; }

		static OpenTelemetryConfiguration()
		{
			ServiceName = "todo-api";
			ServiceVersion = typeof(OpenTelemetryConfiguration).Assembly.GetName().Version!.ToString();
		}

		public static ActivitySource CreateActivitySource() => new(ServiceName, ServiceVersion);

	}
}