using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tawsel.Models;





namespace ApplicationDbcontext
{
    public class TestClass : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TestClass(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "System";

            foreach (var entry in context.ChangeTracker.Entries())
            {
                var entityType = entry.Entity.GetType();

                if (entityType.BaseType != null && entityType.BaseType.IsGenericType &&
                    entityType.BaseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
                {

                    if (entry.State == EntityState.Added)
                    {
                        var createdAtProp = entityType.GetProperty(nameof(BaseEntity<int>.CreatedAt));
                        var createdByProp = entityType.GetProperty(nameof(BaseEntity<int>.CreatedBy));


                        createdAtProp?.SetValue(entry.Entity, DateTime.UtcNow);

                        if (!string.IsNullOrEmpty(userId))
                        {
                            createdByProp?.SetValue(entry.Entity, userId);
                        }
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        var updatedAtProp = entityType.GetProperty(nameof(BaseEntity<int>.EditedAt));
                        var updatedByProp = entityType.GetProperty(nameof(BaseEntity<int>.EditedBy));

                        updatedAtProp?.SetValue(entry.Entity, DateTime.UtcNow);

                        if (!string.IsNullOrEmpty(userId))
                            updatedByProp?.SetValue(entry.Entity, userId);
                    }
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

    }
}
