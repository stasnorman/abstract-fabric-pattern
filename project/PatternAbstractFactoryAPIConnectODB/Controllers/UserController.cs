using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatternAbstractFactoryAPIConnectODB.Factories;
using PatternAbstractFactoryAPIConnectODB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatternAbstractFactoryAPIConnectODB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IDatabaseFactory _sqlServerFactory;
        private readonly IDatabaseFactory _postgresFactory;

        public UsersController(Func<string, IDatabaseFactory> databaseFactoryResolver)
        {
            // Используем функцию-резолвер, чтобы получить нужные фабрики
            _sqlServerFactory = databaseFactoryResolver("SqlServer");
            _postgresFactory = databaseFactoryResolver("Postgres");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersFromBothDatabases()
        {
            List<User> allUsers = new List<User>();

            // Получение пользователей из SQL Server
            using (var sqlServerContext = _sqlServerFactory.GetContext())
            {
                var sqlServerUsers = await sqlServerContext.Set<User>().ToListAsync();
                allUsers.AddRange(sqlServerUsers);
            }

            // Получение пользователей из PostgreSQL
            using (var postgresContext = _postgresFactory.GetContext())
            {
                var postgresUsers = await postgresContext.Set<User>().ToListAsync();
                allUsers.AddRange(postgresUsers);
            }

            return Ok(allUsers);
        }
    }
}
