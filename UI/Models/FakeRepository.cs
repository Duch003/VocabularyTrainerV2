using System.Collections.Generic;
using System.Linq;

namespace UI.Models
{
    public class FakeRepository : IVocabularyRepository
    {
        private IQueryable<EntityModel> _vocabulary;

        public FakeRepository()
        {
            _vocabulary = new List<EntityModel>()
            {
                new EntityModel
                {
                    ID = 0,
                    Polish = "aktywa",
                    English = "Assets",
                    Book = "English4IT",
                    Chapter = "1 - What is information technology?",
                    FormClass = "Noun",
                    //Set = ""
                },
                new EntityModel
                {
                    ID = 1,
                    Polish = "odnosić się do czegoś",
                    English = "to apply to something",
                    Book = "English4IT",
                    Chapter = "1 - What is information technology?",
                    FormClass = "Verb",
                    //Set = ""
                },
                new EntityModel
                {
                    ID = 2,
                    Polish = "natychmiast",
                    English = "at once",
                    Book = "English4IT",
                    Chapter = "1 - What is information technology?",
                    FormClass = "Adverb",
                    //Set = ""
                },
                new EntityModel
                {
                    ID = 3,
                    Polish = "zwiększać efektywność",
                    English = "to boost effectiveness",
                    Book = "English4IT",
                    Chapter = "1 - What is information technology?",
                    FormClass = "Verb",
                    //Set = ""
                },
                new EntityModel
                {
                    ID = 4,
                    Polish = "więzy integralności domeny",
                    English = "domain integrity constraints",
                    Book = "English4IT",
                    Chapter = "2 - Databases",
                    FormClass = "Noun",
                    //Set = ""
                },
                new EntityModel
                {
                    ID = 5,
                    Polish = "sterownik",
                    English = "driver",
                    Book = "English4IT",
                    Chapter = "2 - Databases",
                    FormClass = "Noun",
                    //Set = ""
                },
                new EntityModel
                {
                    ID = 6,
                    Polish = "nadający się do wykorzystania",
                    English = "exploitable",
                    Book = "Grammar book",
                    Chapter = "1 - Databases",
                    FormClass = "Noun",
                    //Set = ""
                },
                new EntityModel
                {
                    ID = 7,
                    Polish = "zacząć",
                    English = "begin",
                    Book = "Grammar book",
                    Chapter = "English grammar",
                    FormClass = "Irregular verbs",
                    //Set = "begin"
                },
                new EntityModel
                {
                    ID = 8,
                    Polish = "zaczął się",
                    English = "began",
                    Book = "Grammar book",
                    Chapter = "English grammar",
                    FormClass = "Irregular verbs",
                    //Set = "begin"
                },
                new EntityModel
                {
                    ID = 9,
                    Polish = "zaczął",
                    English = "begun",
                    Book = "Grammar book",
                    Chapter = "English grammar",
                    FormClass = "Irregular verbs",
                    //Set = "begin"
                }
            }.AsQueryable();
        }

        public IQueryable<EntityModel> Vocabulary
        {
            get { return _vocabulary; }
            private set { _vocabulary = value; }
        }
    

        public bool SaveChanges(IQueryable<EntityModel> vocabulary)
        {
            Vocabulary = vocabulary;
            return true;
        }
    }
}

