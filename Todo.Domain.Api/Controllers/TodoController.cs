using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    //[Authorize]
    public class TodoController : ControllerBase
    {
        
        [Route("")]
        [HttpGet]
        public IEnumerable<TodoItem> getAll(            
            [FromServices] ITodoRepository repository            
        )
        {
           //var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
           var user = "brunolima";
           return repository.GetAll(user);
        }

        [Route("mongo")]
        [HttpGet]
        public Task<IEnumerable<TodoItem>> getAllFromMongo(
            [FromServices] ITodoMongoRespository repository
        )
        {
            //var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            var user = "brunolima";
            return repository.GetAll(user);
        }

        [Route("")]
        [HttpPost]
        public GenericCommandResult Create(
            [FromBody] CreateTodoCommand command,
            [FromServices] TodoHandler handler
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            command.User = "brunolima";
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("")]
        [HttpPut]
        public GenericCommandResult Update(
            [FromBody] UpdateTodoCommand command,
            [FromServices] TodoHandler handler
        )
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("mark-as-done")]
        [HttpPut]
        public GenericCommandResult MaskAsDone(
            [FromBody] MarkTodoAsDoneCommand command,
            [FromServices] TodoHandler handler
        )
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("mark-as-undone")]
        [HttpPut]
        public GenericCommandResult MaskAsUndone(
            [FromBody] MarkTodoAsUndoneCommand command,
            [FromServices] TodoHandler handler
        )
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }

    }
}