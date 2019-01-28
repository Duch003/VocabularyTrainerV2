using System.Windows;
using Caliburn.Micro;
using UI.Models;

namespace UI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private IVocabularyRepository repo;

        public ShellViewModel()
        {
            repo = new FakeRepository();
        }

        public void Play()
        {
            ActivateItem(new PlaySettingsViewModel(repo));
        }

        public void Edit()
        {
            ActivateItem(new DatabaseViewModel(repo));
        }

        public void InputOutput()
        {
           ActivateItem(new InputOutputViewModel());   
        }

        public void About()
        {
            ActivateItem(new AboutViewModel());
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
