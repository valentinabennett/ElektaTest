
namespace ElektaTest.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> Create<TCommand>(TCommand command) where TCommand : ICommand;
    }
}