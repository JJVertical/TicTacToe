using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}
