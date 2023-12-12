using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Support
{
    public interface ISupportsRepository
    {
        Task<List<SupportDto>> GetSupports();

        ValueTask<SupportDto?> GetSupport(int id);
    }
}
