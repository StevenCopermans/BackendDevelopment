using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackend.Models.Repositories
{
    public interface IDifficultyRepo
    {
        Task<Difficulty> Add(Difficulty @difficulty);

        Task<IEnumerable<Difficulty>> GetAllDifficultiesAsync();
        Task<Difficulty> GetDifficultyForIdAsync(Guid id);

        Task<Difficulty> Update(Difficulty @difficulty);

        Task Delete(Guid DifficultyID);

        Task<bool> DifficultyExists(Guid id);
    }
}
