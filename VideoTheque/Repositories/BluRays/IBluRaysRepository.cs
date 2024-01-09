using VideoTheque.DTOs;

namespace VideoTheque.Repositories.BluRays
{
    public interface IBluRaysRepository
    {
        Task<List<BluRayDto>> GetBluRays();

        ValueTask<BluRayDto?> GetBluRay(int id);
    }
}
