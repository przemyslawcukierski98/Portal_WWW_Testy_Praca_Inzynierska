using Microsoft.EntityFrameworkCore;
using OnlineTesty_Library.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library
{
    public class DbSeeder
    {
        private readonly EFDatabaseContext _efDatabaseContext;

        public DbSeeder(EFDatabaseContext efDatabaseContext)
        {
            _efDatabaseContext = efDatabaseContext;
        }

        public void Seed()
        {
            if(_efDatabaseContext.Database.CanConnect())
            {
                var pendingMigrations = _efDatabaseContext.Database.GetPendingMigrations();
                if(pendingMigrations != null && pendingMigrations.Any())
                {
                    _efDatabaseContext.Database.Migrate();
                }
            }
        }
    }
}
