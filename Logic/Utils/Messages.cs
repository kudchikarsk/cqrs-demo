using Logic.AppServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Utils
{
    public sealed class Messages
    {
        private readonly IServiceProvider serviceProvider;

        public Messages(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TResult Dispatch<TResult>(ICommand command)
        {
            var argType = command.GetType();
            var type = typeof(ICommandHandler<,>);
            var handlerType = type.MakeGenericType(argType, typeof(TResult));
            dynamic handler = serviceProvider.GetService(handlerType);
            TResult result = handler.Handle(command);
            return result;
        }
}
}
