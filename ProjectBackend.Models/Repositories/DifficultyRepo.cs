using ProjectBackend.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackend.Models.Repositories
{
    public class DifficultyRepo : IDifficultyRepo
    {
        private readonly QuizAppContext context;
        public DifficultyRepo(QuizAppContext context)
        {
            this.context = context;
        }

        public async Task<Difficulty> Add(Difficulty @Difficulty)
        {
            try
            {
                var result = context.Difficulties.AddAsync(@Difficulty); //ChangeTracking
                await context.SaveChangesAsync();
                return @Difficulty;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException.Message);
                return null;
            }
        }

        public async Task Delete(Guid DifficultyID)
        {
            try
            {
                Difficulty @Difficulty = await GetDifficultyForIdAsync(DifficultyID);

                if (@Difficulty == null)
                {
                    return;
                }

                var result = context.Difficulties.Remove(@Difficulty);
                await context.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return;
        }

        public async Task<bool> DifficultyExists(Guid id)
        {
            return await context.Difficulties.AnyAsync(e => e.ID == id);
        }

        public async Task<IEnumerable<Difficulty>> GetAllDifficultiesAsync()
        {
            return await context.Difficulties.ToListAsync();
        }

        public async Task<Difficulty> GetDifficultyForIdAsync(Guid id)
        {
            var result = await context.Difficulties.AsNoTracking().SingleOrDefaultAsync<Difficulty>(e => e.ID == id);
            return result;
        }

        public async Task<Difficulty> Update(Difficulty @Difficulty)
        {
            try
            {
                context.Difficulties.Update(@Difficulty);
                await context.SaveChangesAsync();
                return @Difficulty;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
        }
    }
}
