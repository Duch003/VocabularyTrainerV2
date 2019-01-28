using System.Linq;

namespace UI.Models
{
    public interface IVocabularyRepository
    {
        IQueryable<EntityModel> Vocabulary { get; }

        bool SaveChanges(IQueryable<EntityModel> vocabulary);
    }
}
