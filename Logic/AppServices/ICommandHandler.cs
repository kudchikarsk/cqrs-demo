using LaYumba.Functional;
using System.Threading.Tasks;

namespace Logic.AppServices
{
    public interface ICommandHandler<T, TOutput> where T : ICommand
    {
        TOutput Handle(T command);
    }
}