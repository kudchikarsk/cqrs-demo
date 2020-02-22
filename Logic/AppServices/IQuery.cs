using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.AppServices
{
    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<T, TResult> where T : IQuery<TResult>
    {
        TResult Handle(T query);
    }
}
