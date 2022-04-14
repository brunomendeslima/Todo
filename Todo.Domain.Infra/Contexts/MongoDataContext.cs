using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Conventions;

namespace Todo.Domain.Infra.Contexts
{
    public class MongoDataContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;        

        public MongoDataContext(IConfiguration configuration)
        {                                    
            RegisterConventions();

            _client = new MongoClient(configuration["ConnectionStrings:MongoDb:ConnectionString"]);
            _database = _client.GetDatabase(configuration["ConnectionStrings:MongoDb:DatabaseName"]);

            OnModelCreating();
        }

        internal IMongoCollection<TodoItem> TodoItens
        {
            get
            {
                return _database.GetCollection<TodoItem>("todos");
            }
        }

        private void OnModelCreating()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(TodoItem)))
            {
                BsonClassMap.RegisterClassMap<TodoItem>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    cm.MapIdMember(x => x.Id);
                    cm.MapMember(x => x.Title);
                    cm.MapMember(x => x.User);
                    cm.MapMember(x => x.Done);
                    cm.MapMember(x => x.Date);
                });
            }
        }

        private static void RegisterConventions()
        {
            ConventionRegistry.Register(nameof(IgnoreValidationResultConvention), 
                new ConventionPack { 
                    new IgnoreValidationResultConvention() 
                }, t => true);
        }

        
    }
}
