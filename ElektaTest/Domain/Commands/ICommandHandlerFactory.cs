
namespace ElektaTest.Domain.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> Create<TCommand>(TCommand command) where TCommand : ICommand;
    }
}