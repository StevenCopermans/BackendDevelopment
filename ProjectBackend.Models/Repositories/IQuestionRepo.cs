using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackend.Models.Repositories
{
    public interface IQuestionRepo
    {
        Task<Question> Add(Question @question);

        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionForIdAsync(Guid id);

        Task<Question> Update(Question @question);

        Task Delete(Guid QuestionID);

        Task<bool> QuestionExists(Guid id);
    }
}
