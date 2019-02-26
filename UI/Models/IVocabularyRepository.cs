using System.Collections.Generic;

namespace UI.Models
{
    public interface IVocabularyRepository
    {
        List<EntityModel> Vocabulary { get; }

        void Add(EntityModel model);
        void Delete(EntityModel model);
        void Edit(EntityModel model);
    }
}
