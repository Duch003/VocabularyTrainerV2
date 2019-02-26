using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Caliburn.Micro;
using UI.Models;

namespace UI.ViewModels
{
    public class DatabaseViewModel : Screen
    {   
        private string _pattern = "";
        private ICollectionView _repositoryView;
        private IVocabularyRepository _repositoryEngine;
        private BindableCollection<EntityModel> _repository;
        private EntityModel _selectedEntity;

        public EntityModel SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                if (value is null && _selectedEntity != null)
                {
                    _repositoryEngine.Delete(_selectedEntity);
                }
                else if (_selectedEntity != null && IsDifferent(_selectedEntity, value))
                {
                    _repositoryEngine.Edit(value);
                }
                else if (value != null && value.ID == -1)
                {
                    _repositoryEngine.Add(value);
                    value = _repositoryEngine.Vocabulary.OrderBy(item => item.ID).LastOrDefault();
                }
                _selectedEntity = value;
                NotifyOfPropertyChange(() => SelectedEntity);
            }
        }

        public string Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                NotifyOfPropertyChange(() => Pattern);
                RepositoryView.Refresh();
            }
        }

        public BindableCollection<EntityModel> Repository
        {
            get
            {
                return _repository;
            }
            set
            {
                _repository = value;
                NotifyOfPropertyChange(() => Repository);
            }
        }

        public ICollectionView RepositoryView
        {
            get { return _repositoryView; }
            set
            {
                _repositoryView = value;
                RepositoryView.Refresh();
                NotifyOfPropertyChange(() => RepositoryView);
                
            }
        }

        public DatabaseViewModel(IVocabularyRepository repository)
        {
            _repositoryEngine = repository;
            Repository = new BindableCollection<EntityModel>(_repositoryEngine.Vocabulary.OrderBy(item => item.ID));
            RepositoryView = CollectionViewSource.GetDefaultView(Repository);
            RepositoryView.Filter = Filter;
        }

        private bool Filter(object item)
        {
            EntityModel entity = item as EntityModel;

            if (entity is null)
                return false;

            if (string.IsNullOrEmpty(Pattern))
                return true;

            bool output =
                (entity.English.Contains(Pattern) || 
                 entity.Polish.Contains(Pattern) ||
                 entity.Book.Contains(Pattern) ||
                 entity.Chapter.Contains(Pattern) ||
                 entity.FormClass.Contains(Pattern));
                
            return output;
        }

        private bool IsDifferent(EntityModel originalEntity, EntityModel newEntity)
        {
            return originalEntity.Book !=  newEntity.Book ||
                   originalEntity.Chapter != newEntity.Chapter ||
                   originalEntity.FormClass != newEntity.FormClass ||
                   originalEntity.Polish != newEntity.Polish ||
                   originalEntity.English != newEntity.English;
        }

        public void CreateNewEntity()
        {

        }
    }
}
