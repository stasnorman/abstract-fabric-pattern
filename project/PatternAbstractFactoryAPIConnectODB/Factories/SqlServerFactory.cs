using Microsoft.EntityFrameworkCore;
using PatternAbstractFactoryAPIConnectODB.DatabaseContexts;

namespace PatternAbstractFactoryAPIConnectODB.Factories
{
    public class SqlServerFactory : IDatabaseFactory
    {
        private readonly IDbContextFactory<SqlServerDbContext> _contextFactory;

        public SqlServerFactory(IDbContextFactory<SqlServerDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public DbContext GetContext()
        {
            return _contextFactory.CreateDbContext();
        }
    }
}
