using System;
using Todo.Domain.Commands.Bases;

namespace Todo.Domain.Commands
{
    public class CreateTodoCommand : CrudTodo
    {       
        public CreateTodoCommand()
        { }

        public CreateTodoCommand(            
            string title, 
            string user, 
            DateTime date
        ) : base(title, user, date)
        { }

        
    }
}