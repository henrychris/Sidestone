using Sidestone.Host.Data.Repositories.Contracts;
using Sidestone.Host.Data.Repositories.Implementations;

namespace Sidestone.Host.Data.Repositories.Uow
{
    public class UnitOfWork(DataContext context) : IUnitOfWork
    {
        public IPlaceholderRepository Placeholders => new PlaceholderRepository(context);

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}
