using IhxPayerIntegrationDBRetry.Enum;
using LinqToDB.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DownloadAttachmentConsole
{
    public class SqlDbSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";

        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return new ConnectionStringSettings
                {
                    ConnectionString = ConfigurationManager.AppSettings.Get("IHXProviderConnectionString"),
                    Name = DbType.IHXPROVIDER.ToString(),
                    ProviderName = "SqlServer"
                };

            }
        }
    }
}
