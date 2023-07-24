using EntityFrameworkCore.Triggered;
using Ewan.Finance.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SharedCoreLibrary.Domain.Abstractions;

namespace Ewan.Finance.Core.Application.Triggers
{
    public class UserTransactionTrigger : IAfterSaveTrigger<IAuditData>
    {
        private readonly IConfiguration configuration;
        private readonly ITransactionLoggerService transactionLoggerService;
        public UserTransactionTrigger(
            IConfiguration configuration,
            ITransactionLoggerService transactionLoggerService
            )
        {
            this.configuration = configuration;
            this.transactionLoggerService = transactionLoggerService;
        }

        public async Task AfterSave(
            ITriggerContext<IAuditData> context,
            CancellationToken cancellationToken)
        {
            var entity = context.Entity.GetType();
            var oldData = JsonConvert.SerializeObject(context.UnmodifiedEntity, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None
            });

            var newData = JsonConvert.SerializeObject(context.Entity, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None
            });

            if (context.ChangeType == ChangeType.Added
                || context.ChangeType == ChangeType.Modified
                || context.ChangeType == ChangeType.Deleted)
            {
                transactionLoggerService.Add(new Models.TransactionLogger.TransactionLoggerModel()
                {
                    UserId = context.ChangeType == ChangeType.Added ? context.Entity.CreatorId : context.Entity.ModifierId,
                    UserName = context.ChangeType == ChangeType.Added ? context.Entity.CreatorNameAr : context.Entity.ModifierNameAr,
                    OperationDate = DateTime.Now,
                    SectorType = "Finance",
                    Module = entity.Namespace.Substring(entity.Namespace.LastIndexOf('.') + 1),
                    EntityName = entity.Name,
                    OperationType = context.ChangeType.ToString(),
                    NewData = newData,
                    OldData = oldData,
                    EntityId = context.Entity.Id.ToString(),
                });
            }
        }
    }
}

