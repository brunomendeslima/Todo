using System;
using Todo.Domain.Commands.Bases;

namespace Todo.Domain.Commands
{
    public class MarkTodoAsUndoneCommand : MarkTodo
    {   
        public MarkTodoAsUndoneCommand()
        { }

        public MarkTodoAsUndoneCommand(Guid id, string user) : base(id, user)
        { }      

    }
}