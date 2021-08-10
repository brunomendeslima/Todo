using System;
using Todo.Domain.Commands;
using Xunit;

namespace Todo.Domain.Tests.Commands
{
    public class CreateTodoCommandTest
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand(null,null,DateTime.Now);
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Lavar o carro", "bruno.lima", DateTime.Now);

        public CreateTodoCommandTest()
        {
            _invalidCommand.Validate();
            _validCommand.Validate();
        }

        [Fact]
        public void DeveCriarUmCommandInvalido()
        {
            Assert.Equal(_invalidCommand.Invalid, true);
        }

        [Fact]              
        public void DeveCriarUmCommandValido()
        {
            Assert.Equal(_validCommand.Valid, true);
        }
    }
}
