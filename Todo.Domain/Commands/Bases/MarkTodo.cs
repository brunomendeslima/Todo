using System;
using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands.Bases
{
    public abstract class MarkTodo : Notifiable, ICommand
    {
        public Guid Id { get; set; }
        public string User { get; set; }

        public MarkTodo()
        {}

        public MarkTodo(Guid id, string user)
        {
            Id = id;
            User = user;
        }
        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(User, 6, "User", "User inválido")
            );
        }
    }
}