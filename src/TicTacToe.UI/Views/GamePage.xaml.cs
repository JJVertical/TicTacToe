using TicTacToe.UI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public GamePage(GameSettings gameSettings)
        {
            InitializeComponent();
            BindingContext = new GameViewModel(gameSettings);
        }
    }
}
