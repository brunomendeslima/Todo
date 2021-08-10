using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler : 
        Notifiable,
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>

    {
        private readonly ITodoRepository _repository;
        private readonly ITodoMongoRespository _mongoRepository;

        public TodoHandler(ITodoRepository repository, ITodoMongoRespository mongoRespository)
        {
            _repository = repository;
            _mongoRepository = mongoRespository;
        }

        public IcommandResult Handle(CreateTodoCommand command)
        {
            command.Validate();
            if(command.Invalid)            
                return new GenericCommandResult(
                    false, 
                    "Ops, parece que sua tarefa está inválida",
                    command.Notifications
                );                
                        
            var todo = new TodoItem(command.Title,command.Date,command.User);

            _repository.Create(todo);
            _mongoRepository.Create(todo);

            return new GenericCommandResult(
                    true, 
                    "tarefa salva com sucesso",
                    todo
                );
        }

        public IcommandResult Handle(UpdateTodoCommand command)
        {
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(
                    false,
                    "Ops, parece que sua tarefa está inválida", 
                    command.Notifications
                );

            var todo = _repository.GetById(command.Id, command.User);   
            
            todo.UpdateTitle(command.Title);
            
            _repository.Update(todo); 

            return new GenericCommandResult(
                    true, 
                    "tarefa atualizada com sucesso",
                    todo
                );
        }

        public IcommandResult Handle(MarkTodoAsDoneCommand command)
        {
           command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(
                    false,
                    "Ops, parece que sua tarefa está inválida", 
                    command.Notifications
                );

            var todo = _repository.GetById(command.Id, command.User);

            todo.MarkAsDone();

            _repository.Update(todo); 

            return new GenericCommandResult(
                    true, 
                    "tarefa atualizada com sucesso",
                    todo
                );
        }

        public IcommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(
                    false,
                    "Ops, parece que sua tarefa está inválida", 
                    command.Notifications
                );

            var todo = _repository.GetById(command.Id, command.User);

            todo.MarkAsUndone();

            _repository.Update(todo); 

            return new GenericCommandResult(
                    true, 
                    "tarefa atualizada com sucesso",
                    todo
                );
        }
    }
}