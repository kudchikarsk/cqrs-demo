namespace Logic
{
    public interface ICommand<TOutput>
    {
    }

    public interface ICommandHandler<TCommand, TOutput> where TCommand : ICommand<TOutput>
    {
        TOutput Handle(TCommand command);
    }
}