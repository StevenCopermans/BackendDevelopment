using ProjectBackend.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackend.Models.Repositories
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly QuizAppContext context;
        public QuestionRepo(QuizAppContext context)
        {
            this.context = context;
        }

        public async Task<Question> Add(Question @question)
        {
            try
            {
                var result = context.Questions.AddAsync(@question); //ChangeTracking
                await context.SaveChangesAsync();
                return @question;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.InnerException.Message);
                return null;
            }
        }

        public async Task Delete(Guid QuestionID)
        {
            try
            {
                Question question = await GetQuestionForIdAsync(QuestionID);

                if (question == null)
                {
                    return;
                }

                var result = context.Questions.Remove(question);
                await context.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return;
        }

        public async Task<bool> QuestionExists(Guid id)
        {
            return await context.Questions.AnyAsync(e => e.ID == id);
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await context.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestionForIdAsync(Guid id)
        {
            var result = await context.Questions.AsNoTracking().SingleOrDefaultAsync<Question>(e => e.ID == id);
            return result;
        }

        public async Task<Question> Update(Question @question)
        {
            try
            {
                context.Questions.Update(@question);
                await context.SaveChangesAsync();
                return @question;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
        }
    }
}
