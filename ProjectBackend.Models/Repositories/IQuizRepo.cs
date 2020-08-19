using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackend.Models.Repositories
{
    public interface IQuizRepo
    {
        Task<Quiz> Add(Quiz @quiz);

        Task<IEnumerable<Quiz>> GetAllQuizesAsync();
        Task<IEnumerable<Quiz>> GetAllQuizesAsync(string search);
        Task<Quiz> GetQuizForIdAsync(Guid id);

        Task<Quiz> Update(Quiz @quiz);

        Task Delete(Guid QuizId);

        Task<bool> QuizExists(Guid id);
    }
}
