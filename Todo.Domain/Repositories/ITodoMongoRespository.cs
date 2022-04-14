using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.Domain.Repositories
{
    public interface ITodoMongoRespository
    {
        void Create(TodoItem todo);       
       
        Task<IEnumerable<TodoItem>> GetAll(string user);
    }
}
