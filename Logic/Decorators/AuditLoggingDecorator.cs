using Newtonsoft.Json;
using System;

namespace Logic.Decorators
{
    public sealed class AuditLoggingDecorator<TCommand, TOutput> : ICommandHandler<TCommand, TOutput>
        where TCommand : ICommand<TOutput>
    {
        private readonly ICommandHandler<TCommand, TOutput> _handler;

        public AuditLoggingDecorator(ICommandHandler<TCommand, TOutput> handler)
        {
            _handler = handler;
        }

        public TOutput Handle(TCommand command)
        {
            string commandJson = JsonConvert.SerializeObject(command);

            // Use proper logging here
            Console.WriteLine($"Command of type {command.GetType().Name}: {commandJson}");

            return _handler.Handle(command);
        }
    }
}
