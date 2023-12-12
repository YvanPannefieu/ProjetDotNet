using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Supports
{
    [Flags]
    public enum Supports
    {
        Blurays = 0b_0000_0001,  // 1
        VHS = 0b_0000_0010,  // 2
        DVD = 0b_0000_0100,  // 4
    }

    public class SupportsRepository
    {
        private readonly VideothequeDb _db;

        public SupportsRepository(VideothequeDb db)
        {
            _db = db;
        }

        public Task<List<SupportDto>> GetSupports()
        {
            // Vous pouvez récupérer les supports à partir de l'enum directement ici
            var supports = Enum.GetValues(typeof(Supports)).Cast<Supports>().ToList();

            var supportDtos = supports.Select(support => new SupportDto
            {
                Name = support.ToString() // Utilisez le nom de l'enum comme nom du support, vous pouvez ajuster selon vos besoins
            }).ToList();

            return Task.FromResult(supportDtos);
        }

        public ValueTask<SupportDto?> GetSupport(int id) => _db.Supports.FindAsync(id);

        public Task InsertSupport(Supports support)
        {
            var supportDto = new SupportDto
            {
                Name = support.ToString() // Utilisez le nom de l'enum comme nom du support, vous pouvez ajuster selon vos besoins
            };

            _db.Supports.AddAsync(supportDto);
            return _db.SaveChangesAsync();
        }

        public Task UpdateSupport(int id, SupportDto support)
        {
            var supportToUpdate = _db.Supports.FindAsync(id).Result;

            if (supportToUpdate is null)
            {
                throw new KeyNotFoundException($"Support '{id}' non trouvé");
            }

            supportToUpdate.Name = support.Name;
            return _db.SaveChangesAsync();
        }

        public Task DeleteSupport(int id)
        {
            var supportToDelete = _db.Supports.FindAsync(id).Result;

            if (supportToDelete is null)
            {
                throw new KeyNotFoundException($"Support '{id}' non trouvé");
            }

            _db.Supports.Remove(supportToDelete);
            return _db.SaveChangesAsync();
        }
    }
}
