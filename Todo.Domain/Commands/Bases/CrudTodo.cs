using System;
using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.Bases
{
    public abstract class CrudTodo : Notifiable, ICommand
    {
        public string Title { get; set; }
        public string User { get; set; }    
        public DateTime Date { get; set; }

         public CrudTodo()
        { }

        public CrudTodo(string title, string user, DateTime date)
        {            
            Title = title;
            User = user;
            Date = date;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Title, 3, "Title", "Por favor, descreva melhor a tarefa")
                    .HasMinLen(User, 6, "User", "User inválido")
            );
        }
    }
}