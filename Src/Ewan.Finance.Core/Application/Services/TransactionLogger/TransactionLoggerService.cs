using AutoMapper;
using Ewan.Finance.Core.Application.Models.TransactionLogger;
using Ewan.Finance.Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Ewan.Finance.Core.Application.Services
{
    public class TransactionLoggerService : ITransactionLoggerService
    {
        private readonly IMongoCollection<TransactionLogger> _transactionLogger;
        private readonly IMapper _mapper;
        private IConfiguration _config;
        public TransactionLoggerService(IMapper mapper, IConfiguration config) 
        {
            _mapper = mapper;
            _config = config;
            var client = new MongoClient(_config["ConnectionStrings:TranactionLogger"]);
            var database = client.GetDatabase(_config["AppSettings:TransactionLogDatabaseName"]);
            _transactionLogger = database.GetCollection<TransactionLogger>(_config["AppSettings:TransactionLoggerCollectionName"]);
        }

        public void Add(TransactionLoggerModel transactionLoggerModel)
        {
            _transactionLogger.InsertOne(_mapper.Map<TransactionLogger>(transactionLoggerModel));
        }

        public List<TransactionLoggerModel> GetListPage(string sectorType, string module, string entityName,int pageNumber = 0,int pageSize = 10)
        {
           var transactions =  _transactionLogger.Find(t => t.SectorType.ToLower() == sectorType.ToLower()
                                                             && t.Module.ToLower() == module.ToLower()
                                                             && t.EntityName.ToLower() == entityName.ToLower())
                              .Skip(pageNumber)
                              .Limit(pageSize)
                              .ToList();

            return _mapper.Map<List<TransactionLoggerModel>>(transactions);

        }
    }
}
