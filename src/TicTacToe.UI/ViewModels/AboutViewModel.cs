namespace TicTacToe.UI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "TicTacToe Game";
        }

        public string GameDescription => "This is a basic TicTacToe mobile game.";
    }
}
