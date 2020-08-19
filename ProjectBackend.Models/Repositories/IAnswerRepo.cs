using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBackend.Models.Repositories
{
    public interface IAnswerRepo
    {
        Task<Answer> Add(Answer @answer);

        Task<IEnumerable<Answer>> GetAllAnswersAsync();
        Task<Answer> GetAnswerForIdAsync(Guid id);

        Task<Answer> Update(Answer @answer);

        Task Delete(Guid AnswerID);

        Task<bool> AnswerExists(Guid id);
    }
}
