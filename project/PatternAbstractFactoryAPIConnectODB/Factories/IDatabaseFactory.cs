using Microsoft.EntityFrameworkCore;

namespace PatternAbstractFactoryAPIConnectODB.Factories
{
    public interface IDatabaseFactory
    {
        DbContext GetContext(); 
    }
}
