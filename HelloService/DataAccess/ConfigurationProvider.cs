using Catcher.DB.DAO;

namespace HelloService.DataAccess
{
    public class ConfigurationProvider : IAdvancedConfigurationProvider
    {
        public bool IsUseSsl => false;

        public string DBName => null;

        public string DBHost => null;

        public int DBPort => -1;

        public string DBUserName => null;

        public string DBPassword => null;

        public int CacheReleaseInterval => 60;

        public int MaxDtoCountInCache => 100;

        public int MaxTimeSearchingInCache => 1000;

        public string CacheDirectory => "/";

        public string ConnectionString => Startup.Configuration["DB:MongoDB"];
    }
}