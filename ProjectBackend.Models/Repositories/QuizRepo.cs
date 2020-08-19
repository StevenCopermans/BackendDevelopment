using ProjectBackend.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackend.Models.Repositories
{
    public class QuizRepo : IQuizRepo
    {
        private readonly QuizAppContext context;
        public QuizRepo(QuizAppContext context)
        {
            this.context = context;
        }

        public async Task<Quiz> Add(Quiz @Quiz)
        {
            try
            {
                var result = context.Quizes.AddAsync(@Quiz); //ChangeTracking
                await context.SaveChangesAsync();
                return @Quiz;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException.Message);
                return null;
            }
        }

        public async Task Delete(Guid QuizId)
        {
            try
            {
                Quiz @Quiz = await GetQuizForIdAsync(QuizId);

                if (@Quiz == null)
                {
                    return;
                }

                var result = context.Quizes.Remove(@Quiz);
                await context.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return;
        }

        public async Task<bool> QuizExists(Guid id)
        {
            return await context.Quizes.AnyAsync(e => e.ID == id);
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizesAsync()
        {
            return await context.Quizes.ToListAsync();
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizesAsync(string search)
        {
            IEnumerable<Quiz> result = null;

            if (string.IsNullOrEmpty(search))
            {
                result = await context.Quizes.ToListAsync();
            }
            else
            {
                var query = context.Quizes.Where(e => e.Label.Contains(search));
                result = await query.ToListAsync();
            }
            return result.OrderBy(e => e.Label);
        }

        public async Task<Quiz> GetQuizForIdAsync(Guid id)
        {
            var result = await context.Quizes.AsNoTracking().SingleOrDefaultAsync<Quiz>(e => e.ID == id);
            return result;
        }

        public async Task<Quiz> Update(Quiz @Quiz)
        {
            try
            {
                context.Quizes.Update(@Quiz);
                await context.SaveChangesAsync();
                return @Quiz;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
        }
    }
}
