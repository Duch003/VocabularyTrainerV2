using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using UI.Models;

namespace UI.ViewModels
{
    public class PlaySettingsViewModel : Screen
    {
        private readonly string _shortAmountOfTime = "Time for single question must be greater than time for whole quiz.\n";
        private readonly string _timeEqualsZero = "Time must be greater than zero.\n";
        private readonly string _pointsEqualsZeroOrNegative = "Amount of points must be greater than zero.\n";
        private readonly string _pointsNegative = "Amount of points can not be lower than zero (zeroo allowed).\n";

        private GameSettingsModel _gameSettings;
        private IWindowManager _windowManager;
        private BindableCollection<string> _bookOption;
        private BindableCollection<string> _chapterOption;
        private BindableCollection<string> _formClassOption;
        private string _anyConstant = "-ALL-";
        private IVocabularyRepository _repository;
        private string _errorList = "";
        private string _rules = 
            "\nSUBJECT AREA: To start quiz select subject area by selecting Book, Chapter (once book is selected) " +
            "and Form Class. Leaving all of those on option -ALL- results with selecting all " +
            "available questions.\n\nSCORING SYSTEM: Each question starts with 0 points on it's account. For each " +
            "correct anwser or incorrect anwser player respectively is earning or losing points. The " +
            "questions goes to be repeated until player earns schceduled amount of points to pass." +
            "\n\nTIME CHALLANGE: Available is additional Time challange mode, in which player have time borders to finish" +
            "quiz or to pass highest possible amount of questions. Every thing can be set up above.\nHave fun!";

        public string Rules
        {
            get { return _rules; }
        }

        public string ErrorList
        {
            get { return _errorList; }
            set
            {
                _errorList = value;
                NotifyOfPropertyChange(() => ErrorList);
            }
        }

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
                if (!value)
                {
                    ErrorList = ErrorList.Replace("TimePerQuestion: " + _timeEqualsZero, "");
                    ErrorList = ErrorList.Replace("TimePerQuestion: " + _shortAmountOfTime, "");
                    ErrorList = ErrorList.Replace("TimePerGame: " + _shortAmountOfTime, "");
                    ErrorList = ErrorList.Replace("TimePerGame: " + _timeEqualsZero, "");
                }
                else
                {
                    TimePerGame = "0:15:0";
                    TimePerQuestion = "0:01:0";
                }
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

                if (_gameSettings.TimePerQuestion >= _gameSettings.TimePerGame && _gameSettings.EnableTimeChallange)
                    ErrorList += "TimePerQuestion: " + _shortAmountOfTime;
                else
                    ErrorList = ErrorList.Replace("TimePerQuestion: " + _shortAmountOfTime, "");

                if (_gameSettings.TimePerQuestion == TimeSpan.Zero && _gameSettings.EnableTimeChallange)
                    ErrorList += "TimePerQuestion: " + _timeEqualsZero;
                else
                    ErrorList = ErrorList.Replace("TimePerQuestion: " + _timeEqualsZero, "");
                
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

                if (_gameSettings.TimePerQuestion >= _gameSettings.TimePerGame && _gameSettings.EnableTimeChallange)
                {
                    var message = "TimePerQuestion: " + _shortAmountOfTime;
                    ErrorList += ErrorList.Contains(message) ? "" : message;
                }
                    
                else
                    ErrorList = ErrorList.Replace("TimePerQuestion: " + _shortAmountOfTime, "");

                if (_gameSettings.TimePerGame == TimeSpan.Zero && _gameSettings.EnableTimeChallange)
                {
                    var message = "TimePerGame: " + _timeEqualsZero;
                    ErrorList += ErrorList.Contains(message) ? "" : message;
                }
                else
                    ErrorList = ErrorList.Replace("TimePerGame: " + _timeEqualsZero, "");
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

                if (_gameSettings.PointsToPass <= 0)
                {
                    var message = "PointsToPass: " + _pointsEqualsZeroOrNegative;
                    ErrorList += ErrorList.Contains(message) ? "" : message;
                } 
                else
                    ErrorList = ErrorList.Replace("PointsToPass: " + _pointsEqualsZeroOrNegative, "");

                NotifyOfPropertyChange(() => PointsToPass);
            }
        }

        public string PointsPerGoodAnwser {
            get { return _gameSettings.PointsPerGoodAnwser.ToString("F"); }
            set {
                if (double.TryParse(value, out var points))
                    _gameSettings.PointsPerGoodAnwser = points;
                else
                    _gameSettings.PointsPerGoodAnwser = 0;

                if (_gameSettings.PointsPerGoodAnwser <= 0)
                {
                    var message = "PointsPerGoodAnwser: " + _pointsEqualsZeroOrNegative;
                    ErrorList += ErrorList.Contains(message) ? "" : message;
                }
                else
                    ErrorList = ErrorList.Replace("PointsPerGoodAnwser: " + _pointsEqualsZeroOrNegative, "");

                NotifyOfPropertyChange(() => PointsPerGoodAnwser);
            }
        }

        public string PointsPerBadAnwser {
            get { return _gameSettings.PointsPerBadAnwser.ToString("F"); }
            set {
                if (double.TryParse(value, out var points))
                    _gameSettings.PointsPerBadAnwser = points;
                else
                    _gameSettings.PointsPerBadAnwser = 0;

                if (_gameSettings.PointsPerBadAnwser < 0)
                {
                    var message = "PointsPerBadAnwser: " + _pointsNegative;
                    ErrorList += ErrorList.Contains(message) ? "" : message;
                }
                else
                    ErrorList = ErrorList.Replace("PointsPerBadAnwser: " + _pointsNegative, "");

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
            SelectedChapter = ChapterOption.First();
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
            if (!string.IsNullOrWhiteSpace(ErrorList))
                return;

            var questions = new List<EntityModel>();
            var bookRequirement = SelectedBook == _anyConstant ? "" : SelectedBook;
            var chapterReqirement = SelectedChapter == _anyConstant ? "" : SelectedChapter;
            var formClassRequirement = SelectedFormClass == _anyConstant ? "" : SelectedFormClass;

            if (string.IsNullOrEmpty(bookRequirement) && string.IsNullOrEmpty(chapterReqirement) &&
                string.IsNullOrEmpty(formClassRequirement))
            {
                questions = (from entity in Repository.Vocabulary
                    select entity).ToList();
            }
            else
            {
                questions = (from entity in Repository.Vocabulary
                    where entity.Book.Contains(bookRequirement) &&
                          entity.Chapter.Contains(chapterReqirement) &&
                          entity.FormClass.Contains(formClassRequirement)
                    select entity).ToList();
            }
            
            _windowManager = new WindowManager();
            _windowManager.ShowWindow(new GameWindowViewModel(questions, _gameSettings));
        }
    }
}
