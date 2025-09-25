namespace CCRS.Api.Configuration
{
    public static class HerokuConfig
    {
        public static void ConfigureForHeroku(this WebApplicationBuilder builder)
        {
            // Configure port for Heroku
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.UseUrls($"http://*:{port}");

            // Override configuration with environment variables
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DATABASE_URL")))
            {
                var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                var connectionString = ConvertDatabaseUrl(databaseUrl);
                builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("JWT_SECRET")))
            {
                builder.Configuration["AppSettings:Secret"] = Environment.GetEnvironmentVariable("JWT_SECRET");
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ELMAH_API_KEY")))
            {
                builder.Configuration["ElmahIo:ApiKey"] = Environment.GetEnvironmentVariable("ELMAH_API_KEY");
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ELMAH_LOG_ID")))
            {
                builder.Configuration["ElmahIo:LogId"] = Environment.GetEnvironmentVariable("ELMAH_LOG_ID");
            }
        }

        private static string ConvertDatabaseUrl(string databaseUrl)
        {
            try
            {
                var databaseUri = new Uri(databaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');
                
                // Convert to PostgreSQL connection string format
                return $"Host={databaseUri.Host};Port={databaseUri.Port};Database={databaseUri.LocalPath.Substring(1)};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
            }
            catch
            {
                // If conversion fails, return the original string
                return databaseUrl;
            }
        }

        public static bool IsPostgreSqlConnection(string connectionString)
        {
            return connectionString.Contains("Host=") || connectionString.Contains("Server=") && connectionString.Contains("Port=");
        }
    }
}