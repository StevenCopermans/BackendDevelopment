using ProjectBackend.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackend.Models.Repositories
{
    public class AnswerRepo : IAnswerRepo
    {
        private readonly QuizAppContext context;
        public AnswerRepo(QuizAppContext context)
        {
            this.context = context;
        }

        public async Task<Answer> Add(Answer @answer)
        {
            try
            {
                var result = context.Answers.AddAsync(@answer); //ChangeTracking
                await context.SaveChangesAsync();
                return @answer;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException.Message);
                return null;
            }
        }

        public async Task Delete(Guid AnswerID)
        {
            try
            {
                Answer @Answer = await GetAnswerForIdAsync(AnswerID);

                if (@Answer == null)
                {
                    return;
                }

                var result = context.Answers.Remove(@Answer);
                await context.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return;
        }

        public async Task<bool> AnswerExists(Guid id)
        {
            return await context.Answers.AnyAsync(e => e.ID == id);
        }

        public async Task<IEnumerable<Answer>> GetAllAnswersAsync()
        {
            return await context.Answers.ToListAsync();
        }

        public async Task<Answer> GetAnswerForIdAsync(Guid id)
        {
            var result = await context.Answers.AsNoTracking().SingleOrDefaultAsync<Answer>(e => e.ID == id);
            return result;
        }

        public async Task<Answer> Update(Answer @Answer)
        {
            try
            {
                context.Answers.Update(@Answer);
                await context.SaveChangesAsync();
                return @Answer;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
        }
    }
}
