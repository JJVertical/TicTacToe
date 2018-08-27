namespace TicTacToe.UI.ViewModels
{
    public class GameSpaceViewModel
    {
        private readonly SpaceValue _spaceValue;

        public GameSpaceViewModel(SpaceValue spaceValue)
        {
            _spaceValue = spaceValue;
        }

        public bool IsAvailable => _spaceValue == SpaceValue.Available;

        public string Value => _spaceValue == SpaceValue.Available ? string.Empty : _spaceValue.ToString();
    }
}
