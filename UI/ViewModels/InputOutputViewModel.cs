using System;
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
            dialog.Description = "Select folder which You want to save the file in.";
            
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

            NotifyOfPropertyChange(() => CanSaveRecords);
        }

        public bool CanSaveRecords
        {
             get
             {
                return !string.IsNullOrEmpty(SelectedPathToSave);
             } 
            
        } 

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

            NotifyOfPropertyChange(() => CanSaveRecords);
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

            if (string.IsNullOrEmpty(SelectedPathToLoad))
            {
                if (!IOEngine.IsValid(SelectedPathToLoad))
                {
                    SelectedPathToLoad = "";
                    MessageBox.Show("Selected file is not valid/readable. Select proper file.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                    
            }

            NotifyOfPropertyChange(() => CanLoadRecords);
        }

        public bool CanLoadRecords
        {
            get
            {
                return !string.IsNullOrEmpty(SelectedPathToLoad); 
            }
        }
        

        public async void LoadRecords()
        {
            IOEngine engine = new IOEngine();
            List<EntityModel> localVocabulary = new List<EntityModel>();

            try
            {
                localVocabulary = await engine.Deserialize(SelectedPathToLoad);
                Repository = new BindableCollection<EntityModel>(localVocabulary);
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error occured while deserializing file. Message:\n{e.Message}\n\nChoose proper xml file.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SelectedPathToLoad = "";
            NotifyOfPropertyChange(() => CanLoadRecords);
            NotifyOfPropertyChange(() => CanLoadIntoDatabase);
            NotifyOfPropertyChange(() => CanClearTable);
        }

        public bool CanLoadIntoDatabase
        {
            get 
            {
                return Repository.Any();
            }
        }
        

        public void LoadIntoDatabase()
        {
            Repository repo = new Repository();
            foreach (var item in Repository)
            {
                if (repo.Vocabulary.Exists(z => z.ID == item.ID) && item.ID != 0)
                {
                    repo.Edit(item);
                }
                else
                {
                    repo.Add(item);
                }
            }
        }

        public bool CanClearTable
        {
            get 
            {
                return Repository.Any();
            }
        }
        

        public void ClearTable()
        {
            Repository = new BindableCollection<EntityModel>();
            NotifyOfPropertyChange(() => CanClearTable);
            NotifyOfPropertyChange(() => CanLoadIntoDatabase);
        }
    }
}
