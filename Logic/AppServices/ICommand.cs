namespace Logic
{
    public interface ICommand<TOutput>
    {
    }

    public interface ICommandHandler<T, TOutput> where T : ICommand<TOutput>
    {
        TOutput Handle(T command);
    }
}