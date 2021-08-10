using System;
using Todo.Domain.Commands.Bases;

namespace Todo.Domain.Commands
{
    public class MarkTodoAsDoneCommand : MarkTodo
    {   
        public MarkTodoAsDoneCommand()
        { }

        public MarkTodoAsDoneCommand(Guid id, string user) : base(id, user)
        { }      

    }
}