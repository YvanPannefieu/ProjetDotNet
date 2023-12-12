using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Support
{
    [Flags]
    public enum Supports
    {
        Blurays = 0b_0000_0001,
        VHS = 0b_0000_0010
    }

    public class SupportsRepository : ISupportsRepository
    {
        public Task<List<SupportDto>> GetSupports()
        {
            var supportDtos = Enum.GetValues(typeof(Supports))
                .Cast<Supports>()
                .Select(support => new SupportDto
                {
                    Id = (int)support,
                    Name = support.ToString()
                })
                .ToList();

            return Task.FromResult(supportDtos);
        }

        public ValueTask<SupportDto?> GetSupport(int id)
        {
            var support = Enum.GetValues(typeof(Supports))
                .Cast<Supports>()
                .FirstOrDefault(s => (int)s == id);

            if (support == 0)
            {
                return new ValueTask<SupportDto?>((SupportDto?)null);
            }

            var supportDto = new SupportDto
            {
                Id = (int)support,
                Name = support.ToString()
            };

            return new ValueTask<SupportDto?>(supportDto);
        }
    }
}
