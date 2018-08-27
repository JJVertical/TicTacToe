using System;
using TicTacToe.UI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameSettingsPage : ContentPage
    {
        private readonly GameSettingsViewModel _gameSettingsViewModel;

        public GameSettingsPage()
        {
            InitializeComponent();
            BindingContext = _gameSettingsViewModel = new GameSettingsViewModel();
        }

        private async void PlayGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(new GameSettings(_gameSettingsViewModel.Player1Name, _gameSettingsViewModel.Player2Name, _gameSettingsViewModel.CurrentlySelectedGameSize.Value)));
        }
    }
}
