using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using UI.Models;

namespace UI.ViewModels
{
    public class GameWindowViewModel : Screen
    {
        #region Constants
        private readonly Brush _correctAnwserBrush = Brushes.LawnGreen;
        private readonly Brush _badAnwserBrush = Brushes.Red;
        private readonly Brush _defaultBrush = Brushes.White;
        private readonly Brush _visibleForegroundBrush = Brushes.Black;
        private DispatcherTimer _gameCounter = new DispatcherTimer();
        private DispatcherTimer _questionCounter = new DispatcherTimer();
        private string _checkButtonContent = "Check anwser";
        #endregion

        #region Private fields
        private List<EntityModel> _questions;
        private GameSettingsModel _gameSettings;
        private TimeSpan _gameTimeLeft;
        private TimeSpan _questionTimeLeft;
        private Brush _anwserIndicatorBrush;
        private Brush _anwserBelowForegroundBrush;
        
        private int _id;
        private string _polish;
        private string _book;
        private string _chapter;
        private string _formClass;
        private string _anwserText;
        private string _correctAnwser;
        private double _points;
        #endregion

        #region Properties
        public string CheckButtonContent
        {
            get { return _checkButtonContent; }
            set
            {
                _checkButtonContent = value; 
                NotifyOfPropertyChange(() => CheckButtonContent);
            }
        }

        public double PresentPoints
        {
            get { return _points; }
            set
            {
                _points = value;
                _questions.First(item => item.ID == ID).Points = value;
                NotifyOfPropertyChange(() => PresentPoints);
            }
        }

        public double RequiredPoints {
            get { return GameSettings.PointsToPass; }
            set {
                GameSettings.PointsToPass = value;
                NotifyOfPropertyChange(() => RequiredPoints);
            }
        }

        public Brush AnwserBelowForegroundBrush
        {
            get { return _anwserBelowForegroundBrush; }
            set
            {
                _anwserBelowForegroundBrush = value;
                NotifyOfPropertyChange(() => AnwserBelowForegroundBrush);
            }
        }

        public string CorrectAnwser
        {
            get { return _correctAnwser; }
            set
            {
                _correctAnwser = value;
                NotifyOfPropertyChange(() => CorrectAnwser);
            }
        }

        public DispatcherTimer GameCounter
        {
            get { return _gameCounter; }
            set
            {
                _gameCounter = value;
                NotifyOfPropertyChange(() => GameCounter);
            }
        }

        public DispatcherTimer QuestionCounter {
            get { return _questionCounter; }
            set {
                _questionCounter = value;
                NotifyOfPropertyChange(() => QuestionCounter);
            }
        }

        public TimeSpan QuestionTimeBase
        {
            get { return _gameSettings.TimePerQuestion; }
            set
            {
                _gameSettings.TimePerQuestion = value;
                NotifyOfPropertyChange(() => QuestionTimeBase);
            }
        }

        public TimeSpan GameTimeLeft {
            get {
                return _gameTimeLeft;
            }
            set {
                _gameTimeLeft = value;
                NotifyOfPropertyChange(() => GameTimeLeft);
            }
        }

        public TimeSpan QuestionTimeLeft {
            get { return _questionTimeLeft; }
            set {
                _questionTimeLeft = value;
                NotifyOfPropertyChange(() => QuestionTimeLeft);
            }
        }

        public GameSettingsModel GameSettings
        {
            get { return _gameSettings; }
            set
            {
                _gameSettings = value;
                NotifyOfPropertyChange(() => GameSettings);
            }
        }

        public Brush AnwserIndicatorBrush
        {
            get { return _anwserIndicatorBrush; }
            set
            {
                _anwserIndicatorBrush = value;
                NotifyOfPropertyChange(() => AnwserIndicatorBrush);
            }
        }

        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => ID);
            }
        }

        public string FormClass {
            get { return _formClass; }
            set {
                _formClass = value;
                NotifyOfPropertyChange(() => FormClass);
            }
        }

        public string Chapter {
            get { return _chapter; }
            set {
                _chapter = value;
                NotifyOfPropertyChange(() => Chapter);
            }
        }

        public string Book {
            get { return _book; }
            set {
                _book = value;
                NotifyOfPropertyChange(() => Book);
            }
        }

        public string AnwserText
        {
            get { return _anwserText; }
            set
            {
                _anwserText = value;
                NotifyOfPropertyChange(() => AnwserText);
            }
        }

        public string QuestionText
        {
            get { return _polish; }
            set
            {
                _polish = value;
                NotifyOfPropertyChange(() => QuestionText);
            }
        }
        #endregion

        public GameWindowViewModel(List<EntityModel> questions, GameSettingsModel gameSettings)
        {
            //Assign and shuffle question set
            _questions = RandomPick.SimpleShuffle(questions);
            _questions = RandomPick.SimpleShuffle(_questions);
            _questions = RandomPick.SimpleShuffle(_questions);

            //Assign game settings
            GameSettings = gameSettings;

            //TIMERS SETTINGS
            //Checking events every second
            GameCounter.Interval = new TimeSpan(0, 0, 1);
            QuestionCounter.Interval = new TimeSpan(0, 0, 1);

            if (GameSettings.EnableTimeChallange)
            {
                GameCounter.Tick += NegatieGameTimer;
                QuestionCounter.Tick += NegativeQuestionTimer;
                GameTimeLeft = GameSettings.TimePerGame;
            }
            else
            {
                GameCounter.Tick += PositiveGameTimer;
                QuestionCounter.Tick += PositiveQuestionTimer;
            }

            GameCounter.Start();
            Ask();
        }

        private void FinishGame()
        {
            GameCounter.Stop();
            QuestionCounter.Stop();
            MessageBox.Show("YAY");
            //TODO Do finish stuff. Maybe some charts?
        }

        #region Time challange timer methods
        private void NegatieGameTimer(object sender, EventArgs e)
        {
            GameTimeLeft = GameTimeLeft.Subtract(new TimeSpan(0, 0, 1));
            if (GameTimeLeft <= TimeSpan.Zero)
            {
                FinishGame();
            }
        }

        private void NegativeQuestionTimer(object sender, EventArgs e)
        {
            QuestionTimeLeft = QuestionTimeLeft.Subtract(new TimeSpan(0, 0, 1));
            if (QuestionTimeLeft <= TimeSpan.Zero && GameSettings.EnableTimeChallange)
            {
                CheckButton_Click();
            }
        }
        #endregion

        #region Classic quiz methods
        private void PositiveGameTimer(object sender, EventArgs e)
        {
            GameTimeLeft = GameTimeLeft.Add(new TimeSpan(0, 0, 1));
        }

        private void PositiveQuestionTimer(object sender, EventArgs e)
        {
            QuestionTimeLeft = QuestionTimeLeft.Add(new TimeSpan(0, 0, 1));
        }
        #endregion

        private void Ask()
        {
            var currentQuestion = RandomPick.Draw<EntityModel>(_questions);

            ID = currentQuestion.ID;
            QuestionText = currentQuestion.Polish;
            FormClass = currentQuestion.FormClass;
            Chapter = currentQuestion.Chapter;
            Book = currentQuestion.Book;
            PresentPoints = currentQuestion.Points;
            CorrectAnwser = currentQuestion.English;

            AnwserBelowForegroundBrush = _defaultBrush;
            AnwserIndicatorBrush = _defaultBrush;

            QuestionTimeLeft = QuestionTimeBase;
            QuestionCounter.Start();
            AnwserText = "";
        }

        private void CheckAnwser()
        {
            QuestionCounter.Stop();
            
            if (string.Equals(CorrectAnwser, AnwserText, StringComparison.InvariantCultureIgnoreCase))
            {
                AnwserIndicatorBrush = _correctAnwserBrush;
                PresentPoints += GameSettings.PointsPerGoodAnwser;
            }
            else
            {
                AnwserIndicatorBrush = _badAnwserBrush;
                PresentPoints -= GameSettings.PointsPerBadAnwser;
            }

            AnwserBelowForegroundBrush = _visibleForegroundBrush;

            if (!(PresentPoints >= GameSettings.PointsToPass)) return;
            var copyOfActualQuestion = _questions.First(item => item.ID == ID);
            _questions.Remove(copyOfActualQuestion);

        }

        #region Events
        public void CheckButton_Click()
        {
            if (CheckButtonContent == "Check anwser")
            {
                CheckButtonContent = "Next question";
                CheckAnwser();
            }
            else
            {
                CheckButtonContent = "Check anwser";
                Ask();
            }
        }
        #endregion

    }
}
