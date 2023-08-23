using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace SharedInfraStructureLibrary.Interceptors
{
    public sealed class UpdateAuditDataInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        //public UpdateAuditDataInterceptor(IHttpContextAccessor httpContextAccessor)
        //{
        //    this.httpContextAccessor = httpContextAccessor;
        //}

        //public override InterceptionResult<int> SavingChanges(
        //    DbContextEventData eventData, 
        //    InterceptionResult<int> result)
        //{
        //    AuditData(eventData.Context);
        //    return base.SavingChanges(eventData, result);
        //}

        //public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        //    DbContextEventData eventData, 
        //    InterceptionResult<int> result, 
        //    CancellationToken cancellationToken = default)
        //{
        //    AuditData(eventData.Context);
        //    return base.SavingChangesAsync(eventData, result, cancellationToken);
        //}

        //private void AuditData(DbContext dbContext)
        //{
        //    var userId = GetCurrentUserId();
        //    var NameAr = GetCurrentUserNameAr();
        //    var NameEn = GetCurrentUserNameEn();

        //    dbContext.ChangeTracker.DetectChanges();
        //    var entries = dbContext.ChangeTracker.Entries<IAuditData>();


        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatorId = userId;
        //            entry.Entity.CreationDate = DateTime.Now;
        //            entry.Entity.CreatorNameAr = NameAr;
        //            entry.Entity.CreatorNameEn = NameEn;
        //        }
        //        else if (entry.State == EntityState.Modified)
        //        {
        //            entry.Property(p => p.CreatorId).IsModified = false;
        //            entry.Property(p => p.CreatorNameAr).IsModified = false;
        //            entry.Property(p => p.CreatorNameEn).IsModified = false;
        //            entry.Property(p => p.CreationDate).IsModified = false;

        //            entry.Entity.ModifierId = userId;
        //            entry.Entity.ModificationDate = DateTime.Now;
        //            entry.Entity.ModifierNameAr = NameAr;
        //            entry.Entity.ModifierNameEn = NameEn;
        //        }
        //    }
        //}

        //private string GetCurrentUserId()
        //{
        //    if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        return httpContextAccessor.HttpContext.User.Claims
        //            .FirstOrDefault(c => c.Type.ToLower().Contains("userid")).Value;
        //    }

        //    return null;
        //}

        //private string GetCurrentUserNameAr()
        //{
        //    if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        return httpContextAccessor.HttpContext.User.Claims
        //            .FirstOrDefault(c => c.Type.ToLower() == "namear").Value.ToString();
        //    }

        //    return null;
        //}

        //private string GetCurrentUserNameEn()
        //{
        //    if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        return httpContextAccessor.HttpContext.User.Claims
        //            .FirstOrDefault(c => c.Type.ToLower() == "nameen").Value.ToString();
        //    }

        //    return null;
        //}
    }
}
