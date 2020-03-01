using System;
using Logic.Utils;

namespace Logic.Decorators
{
    public sealed class DatabaseRetryDecorator<TCommand, TOutput> : ICommandHandler<TCommand, TOutput>
        where TCommand : ICommand<TOutput>
    {
        private readonly ICommandHandler<TCommand, TOutput> _handler;
        private readonly Config _config;

        public DatabaseRetryDecorator(ICommandHandler<TCommand, TOutput> handler, Config config)
        {
            _config = config;
            _handler = handler;
        }

        public TOutput Handle(TCommand command)
        {
            for (int i = 0; ; i++)
            {
                try
                {
                    TOutput result = _handler.Handle(command);
                    return result;
                }
                catch (Exception ex)
                {
                    if (i >= _config.NumberOfDatabaseRetries || !IsDatabaseException(ex))
                        throw;
                }
            }
        }

        private bool IsDatabaseException(Exception exception)
        {
            string message = exception.InnerException?.Message;

            if (message == null)
                return false;

            return message.Contains("The connection is broken and recovery is not possible")
                || message.Contains("error occurred while establishing a connection");
        }
    }
}
