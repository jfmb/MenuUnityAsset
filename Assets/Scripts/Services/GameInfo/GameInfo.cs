public class GameInfo
{
    private GameStatus _gameStatus = new();
    private GameSettings _gameSettings = new();
    public void SetGameIsStartedWith(bool newValue)
    {
        _gameStatus.IsGameStarted = newValue;
    }    
}
