using System;
using Todo.Domain.Commands.Bases;

namespace Todo.Domain.Commands
{
    public class UpdateTodoCommand : CrudTodo
    {
        public Guid Id { get; set; }
        public UpdateTodoCommand()
        { }

        public UpdateTodoCommand(
            Guid id,
            string title, 
            string user, 
            DateTime date
        ) : base(title, user, date)
        { 
            Id = id;
        }
    }
}