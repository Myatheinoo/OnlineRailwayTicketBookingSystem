using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public static class AppDbContextExtension
    {
        public static async Task<int> SaveAndDetachAsync(this DbContext db)
        {
            var res = await db.SaveChangesAsync();
            foreach (var entry in db.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }

            return res;
        }
    }
}
