using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Infra.Repositories
{
    public class TodoMongoRepository : ITodoMongoRespository
    {
        private readonly MongoDataContext _mongoDataContext;

        public TodoMongoRepository(MongoDataContext mongoDataContext)
        {
            _mongoDataContext = mongoDataContext;
        }
        
        public async void Create(TodoItem todo)
        {
            await _mongoDataContext.TodoItens.InsertOneAsync(todo);
        }

        public async Task<IEnumerable<TodoItem>> GetAll(string user)
        {
            //var query = new QueryDocument(BsonSerializer.Deserialize<BsonDocument>(user));

            var filter = new BsonDocument
            {
                { "User", user }
            };

            return await  _mongoDataContext.TodoItens.Find(filter).ToListAsync();
        }

        Task<IEnumerable<TodoItem>> ITodoMongoRespository.GetAll(string user)
        {
            throw new NotImplementedException();
        }        
    }
}
