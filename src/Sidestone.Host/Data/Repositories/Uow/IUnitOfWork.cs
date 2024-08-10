using Sidestone.Host.Data.Repositories.Contracts;

namespace Sidestone.Host.Data.Repositories.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IPlaceholderRepository Placeholders { get; }
        Task<int> CompleteAsync();
    }
}
