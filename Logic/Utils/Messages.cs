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

        public TResult Dispatch<TResult>(ICommand<TResult> command)
        {
            var type = typeof(ICommandHandler<,>);
            var argTypes = new Type[] { command.GetType(), typeof(TResult) };
            var handlerType = type.MakeGenericType(argTypes);
            dynamic handler = serviceProvider.GetService(handlerType);
            TResult result = handler.Handle((dynamic)command);
            return result;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            var type = typeof(IQueryHandler<,>);
            var argTypes = new Type[] { query.GetType(), typeof(TResult) };
            var handlerType = type.MakeGenericType(argTypes);
            dynamic handler = serviceProvider.GetService(handlerType);
            TResult result = handler.Handle((dynamic)query);
            return result;
        }
    }
}
