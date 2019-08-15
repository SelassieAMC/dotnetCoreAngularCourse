using System.Threading.Tasks;

namespace Vegas.Core
{

    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}