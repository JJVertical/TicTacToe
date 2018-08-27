namespace TicTacToe.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new UI.App());
        }
    }
}
