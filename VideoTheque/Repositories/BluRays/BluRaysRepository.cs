using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.BluRays
{
    public class BluRaysRepository : IBluRaysRepository
    {
        private readonly VideothequeDb _db;

        public BluRaysRepository(VideothequeDb db)
        {
            _db = db;
        }
        public Task<List<BluRayDto>> GetBluRays() => _db.BluRays.ToListAsync();

        public ValueTask<BluRayDto?> GetBluRay(int id) => _db.BluRays.FindAsync(id);
    }
}
