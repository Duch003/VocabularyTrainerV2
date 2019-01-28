using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Caliburn.Micro;
using UI.Models;
using UI.Views;

namespace UI.ViewModels
{
    public class PlaySettingsViewModel : Screen
    {
        private GameSettingsModel _gameSettings;
        private IWindowManager _windowManager;
        private BindableCollection<string> _bookOption;
        private BindableCollection<string> _chapterOption;
        private BindableCollection<string> _formClassOption;
        private string _anyConstant = "-ALL-";
        private IVocabularyRepository _repository;

        public IVocabularyRepository Repository
        {
            get { return _repository;}
            set { _repository = value; }
        }

        public BindableCollection<string> BookOption
        {
            get { return _bookOption; }
            set
            {
                _bookOption = value;
                NotifyOfPropertyChange(() => BookOption);
            }
        }

        public BindableCollection<string> ChapterOption {
            get { return _chapterOption; }
            set {
                _chapterOption = value;
                NotifyOfPropertyChange(() => ChapterOption);
            }
        }

        public BindableCollection<string> FormClassOption {
            get { return _formClassOption; }
            set {
                _formClassOption = value;
                NotifyOfPropertyChange(() => FormClassOption);
            }
        }

        public string SelectedBook
        {
            get { return _gameSettings.Book; }
            set
            {
                _gameSettings.Book = value; 
                if(CanChapterComboBox())
                    LoadChapters();
                else
                    ClearChapters();
                
                NotifyOfPropertyChange(() => SelectedBook);
            }
        }

        public string SelectedChapter {
            get { return _gameSettings.Chapter; }
            set {
                _gameSettings.Chapter = value;
                NotifyOfPropertyChange(() => SelectedChapter);
            }
        }

        public string SelectedFormClass {
            get { return _gameSettings.FormClass; }
            set {
                _gameSettings.FormClass = value;
                NotifyOfPropertyChange(() => SelectedFormClass);
            }
        }

        public bool EnableTimeChallange
        {
            get { return _gameSettings.EnableTimeChallange; }
            set
            {
                _gameSettings.EnableTimeChallange = value;
                NotifyOfPropertyChange(() => EnableTimeChallange);
            }
        }

        public string TimePerQuestion
        {
            get { return _gameSettings.TimePerQuestion.ToString("g"); }
            set
            {
                if(TimeSpan.TryParse(value, out var time))
                    _gameSettings.TimePerQuestion = time;
                else
                    _gameSettings.TimePerQuestion = new TimeSpan(0,0,0);
                NotifyOfPropertyChange(() => TimePerQuestion);
            }
        }

        public string TimePerGame {
            get { return _gameSettings.TimePerGame.ToString("g"); }
            set {
                if (TimeSpan.TryParse(value, out var time))
                    _gameSettings.TimePerGame = time;
                else
                    _gameSettings.TimePerGame = new TimeSpan(0, 0, 0);
                NotifyOfPropertyChange(() => TimePerGame);
            }
        }

        public string PointsToPass
        {
            get { return _gameSettings.PointsToPass.ToString("F"); }
            set
            {
                if (double.TryParse(value, out var points))
                    _gameSettings.PointsToPass = points;
                else
                    _gameSettings.PointsToPass = 0;
                NotifyOfPropertyChange(() => PointsToPass);
            }
        }

        public string PointsPerGoodAnwser {
            get { return _gameSettings.PointsPerGoodAnwser.ToString("F"); }
            set {
                if (double.TryParse(value, out var points))
                    _gameSettings.PointsPerGoodAnwser = points;
                else
                    _gameSettings.PointsToPass = 0;
                NotifyOfPropertyChange(() => PointsPerGoodAnwser);
            }
        }

        public string PointsPerBadAnwser {
            get { return _gameSettings.PointsPerBadAnwser.ToString("F"); }
            set {
                if (double.TryParse(value, out var points))
                    _gameSettings.PointsPerBadAnwser = points;
                else
                    _gameSettings.PointsToPass = 0;
                NotifyOfPropertyChange(() => PointsPerBadAnwser);
            }
        }

        [ImportingConstructor]
        public PlaySettingsViewModel(IVocabularyRepository repo)
        {
            _gameSettings = new GameSettingsModel();
            Repository = repo;

            LoadBooks();
            LoadFormClasses();
        }

        private void LoadChapters()
        {

            var allChapters = (from item in Repository.Vocabulary
                where item.Book == SelectedBook
                select item.Chapter).Distinct();
            ChapterOption = new BindableCollection<string>(allChapters);
            ChapterOption.Add(_anyConstant);
            ChapterOption.Move(ChapterOption.IndexOf(_anyConstant), 0);
            
        }

        public bool CanChapterComboBox()
        {
            return _gameSettings.Book != _anyConstant && !string.IsNullOrEmpty(_gameSettings.Book);
        }

        private void ClearChapters()
        {
            ChapterOption = new BindableCollection<string>();
        }

        private void LoadBooks()
        {
            var allBooks = (from item in Repository.Vocabulary
                select item.Book).Distinct();
            BookOption = new BindableCollection<string>(allBooks);
            BookOption.Add(_anyConstant);
            BookOption.Move(BookOption.IndexOf(_anyConstant), 0);
            SelectedBook = BookOption.First();
        }

        private void LoadFormClasses()
        {
            var allFormClasses = (from item in Repository.Vocabulary
                select item.FormClass).Distinct();
            FormClassOption = new BindableCollection<string>(allFormClasses);
            FormClassOption.Add(_anyConstant);
            FormClassOption.Move(FormClassOption.IndexOf(_anyConstant), 0);
            SelectedFormClass = FormClassOption.First();
        }

        public void StartButton()
        {
            _windowManager = new WindowManager();
            _windowManager.ShowWindow(new GameWindowViewModel(null, null));
        }

        public void ResetButton()
        {
            _gameSettings = new GameSettingsModel();
            MessageBox.Show("Reset");
        }
    }
}
