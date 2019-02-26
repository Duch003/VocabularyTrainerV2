using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace UI.Models
{
    public class Repository : IVocabularyRepository
    {
        public List<EntityModel> Vocabulary { get; private set; }

        private List<EntityModel> _vocabulary;

       

        public Repository()
        {
            try
            {
                using (var ctx = new TrainerContext())
                {
                    Vocabulary = ctx.Vocabulary.ToList();
                }
            }
            catch (Exception)
            {
                Vocabulary = new List<EntityModel>().ToList();
            }
        }

        public void Add(EntityModel model)
        {
            using (var ctx = new TrainerContext())
            {
                ctx.Vocabulary.Add(model);
                ctx.SaveChanges();
                Vocabulary = ctx.Vocabulary.ToList();
            }
        }

        public void Delete(EntityModel model)
        {
            using (var ctx = new TrainerContext())
            {
                ctx.Vocabulary.Remove(ctx.Vocabulary.FirstOrDefault(item => item.ID == model.ID));
                ctx.SaveChanges();
                Vocabulary = ctx.Vocabulary.ToList();
            }
        }

        public void Edit(EntityModel model)
        {
            using (var ctx = new TrainerContext())
            {
                ctx.Vocabulary.Find(model.ID).Book = model.Book;
                ctx.Vocabulary.Find(model.ID).Chapter = model.Chapter;
                ctx.Vocabulary.Find(model.ID).FormClass = model.FormClass;
                ctx.Vocabulary.Find(model.ID).English = model.English;
                ctx.Vocabulary.Find(model.ID).Polish = model.Polish;

                ctx.SaveChanges();
                Vocabulary = ctx.Vocabulary.ToList();
            }
        }
    }
}
