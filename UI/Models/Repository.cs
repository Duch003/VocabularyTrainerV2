using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace UI.Models
{
    public class Repository : IVocabularyRepository
    {
        public IQueryable<EntityModel> Vocabulary { get; private set; }

        private IQueryable<EntityModel> _vocabulary;

        public bool SaveChanges(IQueryable<EntityModel> vocabulary)
        {
            bool result = true;
            IQueryable<EntityModel> backup = vocabulary;
            try
            {
                using (var ctx = new TrainerContext())
                {
                    foreach (var entity in vocabulary)
                    {
                        EntityState state = ctx.Entry(entity).State;
                        switch (state)
                        {
                            case EntityState.Unchanged:
                            case EntityState.Added:
                                continue;
                            case EntityState.Modified:
                                var oldEntity = ctx.Vocabulary.Find(entity.ID) ;
                                oldEntity = entity;
                                break;
                            case EntityState.Detached:
                            case EntityState.Deleted:
                                ctx.Vocabulary.Add(entity);
                                break;
                        }
                    }

                    ctx.SaveChanges();

                    Vocabulary = ctx.Vocabulary;
                }
            } catch (Exception)
            {
                result = false;
                Vocabulary = vocabulary;
            }

            return result;

        }

        public Repository()
        {
            try
            {
                using (var ctx = new TrainerContext())
                {
                    Vocabulary = ctx.Vocabulary.AsQueryable();
                }
            }
            catch (Exception)
            {
                Vocabulary = new List<EntityModel>().AsQueryable();
            }
        }
    }
}
