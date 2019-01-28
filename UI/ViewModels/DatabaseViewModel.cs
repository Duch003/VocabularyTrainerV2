using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Caliburn.Micro;
using UI.Models;

namespace UI.ViewModels
{
    public class DatabaseViewModel : Screen
    {
        /*TODO Grid nie odświeża się automatycznie kiedy przypięty do ICollectionView
         *Z drugiej strony, przypięty tylko do BindableCollection nie ma możliwości filtrowania
         * ponieważ aplikacja filtrowania w Property zaburza pracę Caliburn.micro i też przestaje sie odświeżać
         *
         *
         */

        /*
         * TODO The easiest way to handle null property within item (for example, when new record has been created, but not all properties are filled with data
         * application crashes in Filter method.
         */
        
        private BindableCollection<string> _book;
        private BindableCollection<string> _formClass;
        private BindableCollection<string> _chapter;
        private BindableCollection<string> _set;
        private int _id = 0;
        private string _pattern = "";
        private string _selectedBook;
        private string _selectedFormClass;
        private string _selectedChapter;
        private string _selectedSet;
        private string _emptySelection = "-None-";
        private ICollectionView _repositoryView;
        private IVocabularyRepository _inputRepository;

        public string Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                NotifyOfPropertyChange(() => Pattern);
                NotifyOfPropertyChange(() => RepositoryView);
            }
        }
        public BindableCollection<EntityModel> Repository { get; set; }
        public BindableCollection<string> Book {
            get { return _book; }
            set {
                _book = value;
                NotifyOfPropertyChange(() => Book);
                NotifyOfPropertyChange(() => Repository);
            }
        }
        public BindableCollection<string> FormClass {
            get { return _formClass; }
            set {
                _formClass = value;
                NotifyOfPropertyChange(() => FormClass);
                NotifyOfPropertyChange(() => Repository);
            }
        }
        public BindableCollection<string> Chapter {
            get { return _chapter; }
            set {
                _chapter = value;
                NotifyOfPropertyChange(() => Chapter);
                NotifyOfPropertyChange(() => Repository);
            }
        }
        public BindableCollection<string> Set {
            get { return _set; }
            set {
                _set = value;
                NotifyOfPropertyChange(() => Set);
                NotifyOfPropertyChange(() => Repository);
            }
        }
        public string SelectedBook
        {
            get { return string.IsNullOrEmpty(_selectedBook) ? _emptySelection : _selectedBook; }
            set
            {
                _selectedBook = value;
                NotifyOfPropertyChange(() => SelectedBook);
                NotifyOfPropertyChange(() => Repository);
            }
        }
        public string SelectedFormClass {
            get { return string.IsNullOrEmpty(_selectedFormClass) ? _emptySelection : _selectedFormClass; }
            set {
                _selectedFormClass = value;
                NotifyOfPropertyChange(() => SelectedFormClass);
                NotifyOfPropertyChange(() => Repository);
            }
        }
        public string SelectedChapter {
            get { return string.IsNullOrEmpty(_selectedChapter) ? _emptySelection : _selectedChapter; }
            set {
                _selectedChapter = value;
                NotifyOfPropertyChange(() => SelectedChapter);
                NotifyOfPropertyChange(() => Repository);
            }
        }
        public string SelectedSet {
            get { return string.IsNullOrEmpty(_selectedSet) ? _emptySelection : _selectedSet; }
            set {
                _selectedSet = value;
                NotifyOfPropertyChange(() => SelectedSet);
                NotifyOfPropertyChange(() => Repository);
            }
        }
        public ICollectionView RepositoryView
        {
            get { return _repositoryView; }
            set
            {
                _repositoryView = value;
                NotifyOfPropertyChange(() => RepositoryView);
                NotifyOfPropertyChange(() => Repository);
                RepositoryView.Refresh();
            }
        }

        public DatabaseViewModel(IVocabularyRepository repository)
        {
            _inputRepository = repository;
            PrepareControl();
        }

        private void PrepareControl()
        {
            Repository = new BindableCollection<EntityModel>(_inputRepository.Vocabulary.OrderBy(item => item.ID));
            RepositoryView = CollectionViewSource.GetDefaultView(Repository);
            RepositoryView.Filter = Filter;

            Book = new BindableCollection<string>(Repository.Select(item => item.Book).Distinct());
            FormClass = new BindableCollection<string>(Repository.Select(item => item.FormClass).Distinct());
            Chapter = new BindableCollection<string>(Repository.Select(item => item.Chapter).Distinct());
            //Set = new BindableCollection<string>(Repository.Select(item => item.Set).Distinct());

            EnsureEmptyChoice();
        }

        private bool Filter(object item)
        {
            EntityModel entity = item as EntityModel;

            bool output =
                (entity.English.Contains(Pattern) || entity.Polish.Contains(Pattern))
                && entity.Book.Contains(SelectedBook == _emptySelection ? "" : SelectedBook)
                && entity.FormClass.Contains(SelectedFormClass == _emptySelection ? "" : SelectedFormClass)
                && entity.Chapter.Contains(SelectedChapter == _emptySelection ? "" : SelectedChapter)
                /*&& entity.Set.Contains(SelectedSet == _emptySelection ? "" : SelectedSet)*/;
            //NotifyOfPropertyChange(() => RepositoryView);
            return output;
        }

        private void EnsureEmptyChoice()
        {
            List<BindableCollection<string>> collections = new List<BindableCollection<string>>()
            {
                Book,
                Chapter,
                FormClass
                //Set
            };

            foreach (var collection in collections)
            {
                if (!collection.Contains(_emptySelection))
                    collection.Add(_emptySelection);
            }
        }

        public void RefreshTable()
        {
            this.Refresh();
            Repository.Refresh();
            RepositoryView.Refresh();
        }

        public void Save()
        {
            _inputRepository.SaveChanges(Repository.AsQueryable());
        }

        public void Cancel()
        {
            PrepareControl();
        }
    }
}
