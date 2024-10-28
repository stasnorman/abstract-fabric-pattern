using Microsoft.EntityFrameworkCore;
using PatternAbstractFactoryAPIConnectODB.DatabaseContexts;

namespace PatternAbstractFactoryAPIConnectODB.Factories
{
    public class PostgreSqlFactory : IDatabaseFactory
    {
        private readonly IDbContextFactory<PostgreSqlDbContext> _contextFactory;

        public PostgreSqlFactory(IDbContextFactory<PostgreSqlDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public DbContext GetContext()
        {
            return _contextFactory.CreateDbContext();
        }
    }
}
