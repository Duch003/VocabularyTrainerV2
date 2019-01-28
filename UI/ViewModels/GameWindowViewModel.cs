using Caliburn.Micro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using UI.Models;

namespace UI.ViewModels
{
    public class GameWindowViewModel : Screen
    {
        private List<EntityModel> _questions;
        private GameSettingsModel _gameSettings;
        private EntityModel _currentQuestion;
        private TimeSpan _gameTime;
        private TimeSpan _questionTime;
        private TimeSpan _subtrahend;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _questionTimerKeepsGoing = false;
        private bool _questionTimerReachZero = false;
        private bool _userBreaksCounting = false;
        private readonly Brush _correctAnwserBrush = Brushes.LawnGreen;
        private readonly Brush _badAnwserBrush = Brushes.Red;
        private readonly Brush _defaultBrush = Brushes.White;
        private Brush _anwserIndicatorBrush;
        private string _anwserText;
        private string _correctAnwserText;
        private bool _pause;

        public string CorrectAnwser
        {
            get { return _correctAnwserText; }
            set
            {
                _correctAnwserText = value;
                NotifyOfPropertyChange(() => CorrectAnwser);
            }
        }

        public TimeSpan GameTime
        {
            get
            {
                return _gameSettings.TimePerGame;
            }
            set
            {
                _gameSettings.TimePerGame = value;
                NotifyOfPropertyChange(() => GameTime);
            }
        }

        public TimeSpan QuestionTime
        {
            get { return _gameSettings.TimePerQuestion; }
            set
            {
                _gameSettings.TimePerQuestion = value;
                NotifyOfPropertyChange(() => QuestionTime);
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
            get { return _currentQuestion.ID; }
            set
            {
                _currentQuestion.ID = value;
                NotifyOfPropertyChange(() => ID);
            }
        }

        public string FormClass {
            get { return _currentQuestion.FormClass; }
            set {
                _currentQuestion.FormClass = value;
                NotifyOfPropertyChange(() => FormClass);
            }
        }

        public string Chapter {
            get { return _currentQuestion.Chapter; }
            set {
                _currentQuestion.Chapter = value;
                NotifyOfPropertyChange(() => Chapter);
            }
        }

        public string Book {
            get { return _currentQuestion.Book; }
            set {
                _currentQuestion.Book = value;
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

        public EntityModel CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                NotifyOfPropertyChange(() => CurrentQuestion);
            }
        }

        public GameWindowViewModel(List<EntityModel> questions, GameSettingsModel gameSettings)
        {
            _questions = RandomPick.Shuffle(questions);
            _questions = RandomPick.Shuffle(_questions);
            _questions = RandomPick.Shuffle(_questions);

            _cancellationTokenSource = new CancellationTokenSource();

            _pause = false;

            _gameSettings = gameSettings;

            if (gameSettings.EnableTimeChallange)
            {
                _subtrahend = new TimeSpan(0,0,1);
            }
            else
            {
                _subtrahend = new TimeSpan(0,0,-1);
                _gameSettings.TimePerGame = new TimeSpan(0,0,1); //To avoid early cancellation token invocation
                _gameSettings.TimePerQuestion = new TimeSpan(0,0,0);
            }

            Task.Run(() => RunGameTime());
            
        }

        private void StartGame()
        {
            CancellationToken token = new CancellationToken();
            TimeSpan original = QuestionTime;

            while (_questions.Count != 0)
            {
                AnwserIndicatorBrush = _defaultBrush;
                CurrentQuestion = Draw(_questions);
                RunQuestionTime(token);
                while (true)
                {
                    if (_cancellationTokenSource.IsCancellationRequested)
                    {
                        CheckAnwser();
                        while (_pause)
                        { }
                    }
                }
                
            }
            FinishGame();
        }

        private void FinishGame()
        {

        }

        private void CheckAnwser()
        {
            if (AnwserText.Equals((CurrentQuestion.English, StringComparison.CurrentCultureIgnoreCase)))
            {
                AnwserIndicatorBrush = _correctAnwserBrush;
                CorrectAnwser = "Well done!";
                CurrentQuestion.Points += GameSettings.PointsPerGoodAnwser;
                if (CurrentQuestion.Points >= GameSettings.PointsToPass)
                    _questions.Remove(CurrentQuestion);
            }
            else
            {
                AnwserIndicatorBrush = _badAnwserBrush;
                CorrectAnwser = CurrentQuestion.English;
                CurrentQuestion.Points -= GameSettings.PointsPerBadAnwser;
            }
        }

        private void RunGameTime()
        {
            while(GameTime != TimeSpan.Zero)
            {
                GameTime = GameTime.Subtract(new TimeSpan(0, 0, 1));
                Thread.Sleep(1000);
            }
            _cancellationTokenSource.Cancel();
            FinishGame();
        }

        private void RunQuestionTime(CancellationToken ct)
        {
            while (QuestionTime != TimeSpan.Zero)
            {
                QuestionTime = QuestionTime.Subtract(_subtrahend);
                    
                if (ct.IsCancellationRequested)
                {
                    break;
                }

                Thread.Sleep(1000);
            }

        }

        private static EntityModel Draw(List<EntityModel> q)
        {
            var pool = q.Count() > 5 ? Enumerable.Range(0, 5) : Enumerable.Range(0, q.Count());
            pool = RandomPick.Shuffle(pool.ToList());
            var id = pool.First();
            return q.ElementAt(id);
        }

        public void Check()
        {
            if (_pause == false)
                _pause = true;
            else
                _pause = false;
        }

    }
}
