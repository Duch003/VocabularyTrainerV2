using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using Caliburn.Micro;
using UI.Models;
using Screen = Caliburn.Micro.Screen;

namespace UI.ViewModels
{
    public class InputOutputViewModel : Screen
    {
        private ICollectionView _repositoryView;
        private BindableCollection<EntityModel> _repository;
        private string _selectedPathToLoad;
        private string _selectedPathToSave;

        public InputOutputViewModel()
        {
            Repository = new BindableCollection<EntityModel>();
        }

        public BindableCollection<EntityModel> Repository
        {
            get { return _repository; }
            set
            {
                _repository = value;
                RepositoryView = CollectionViewSource.GetDefaultView(_repository);
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
            }
        }

        public string SelectedPathToLoad
        {
            get { return _selectedPathToLoad; }
            set
            {
                _selectedPathToLoad = value;
                NotifyOfPropertyChange(() => SelectedPathToLoad);
            }
        }

        public string SelectedPathToSave
        {
            get
            {
                return _selectedPathToSave;
            }
            set
            {
                _selectedPathToSave = value;
                NotifyOfPropertyChange(() => SelectedPathToSave);
            }
        }

        public void SelectPathToSave()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select folder where You want to save the file.";
            
            var result = dialog.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    SelectedPathToSave = dialog.SelectedPath;
                    break;

                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.No:
                    SelectedPathToSave = "";
                    break;
            }
        }

        public bool CanSaveRecords() => string.IsNullOrEmpty(SelectedPathToSave);

        public async void SaveRecords()
        {
            List<EntityModel> vocabulary = new List<EntityModel>();
            IOEngine engine = new IOEngine();

            using (var ctx = new TrainerContext())
            {
                vocabulary = ctx.Vocabulary.ToList();
            }
            await engine.Serialize(SelectedPathToSave, vocabulary);
            SelectedPathToSave = "";
        }

        public void SelectPathToLoad()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "XML File|*.xml";

            var result = dialog.ShowDialog();

            switch (result)
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    SelectedPathToLoad = dialog.FileName;
                    break;

                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.No:
                    SelectedPathToLoad = "";
                    break;
            }
        }

        public void CanLoadRecords() => string.IsNullOrEmpty(SelectedPathToLoad);

        public async void LoadRecords()
        {
            IOEngine engine = new IOEngine();
            List<EntityModel> localVocabulary = new List<EntityModel>();

            localVocabulary = await engine.Deserialize(SelectedPathToLoad);
            Repository = new BindableCollection<EntityModel>(localVocabulary);
        }

        public bool CanLoadIntoDatabase() => Repository.Any();

        public void LoadIntoDatabase()
        {
            Repository repo = new Repository();
            repo.SaveChanges(Repository.AsQueryable());
        }

        public bool CanClearTable()
        {
            return Repository.Any();
        }

        public void ClearTable()
        {
            Repository = new BindableCollection<EntityModel>();
        }
    }
}
