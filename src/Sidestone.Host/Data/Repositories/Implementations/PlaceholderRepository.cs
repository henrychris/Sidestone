using Sidestone.Host.Data.Entities;
using Sidestone.Host.Data.Repositories.Base;
using Sidestone.Host.Data.Repositories.Contracts;

namespace Sidestone.Host.Data.Repositories.Implementations
{
    public class PlaceholderRepository(DataContext context) : BaseRepository<BaseEntity>(context), IPlaceholderRepository { }
}
