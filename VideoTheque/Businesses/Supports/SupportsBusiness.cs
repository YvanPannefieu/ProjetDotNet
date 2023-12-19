using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Support;

namespace VideoTheque.Businesses.Supports
{
    public class SupportsBusiness : ISupportsBusiness
    {
        private readonly ISupportsRepository _supportsRepository;

        public SupportsBusiness(ISupportsRepository supportsRepository)
        {
            _supportsRepository = supportsRepository;
        }

        public Task<List<SupportDto>> GetSupports() => _supportsRepository.GetSupports();

        public ValueTask<SupportDto?> GetSupport(int id) => _supportsRepository.GetSupport(id);
    }
}
