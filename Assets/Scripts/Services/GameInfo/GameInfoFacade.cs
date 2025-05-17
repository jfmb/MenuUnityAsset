public class GameInfoFacade: GameService
{
    private GameStatus _gameStatus = new();
    private GameSettings _gameSettings = new();
    
    public override void Install()
    {
        DontDestroyOnLoad(gameObject);
        ServiceLocator.RegisterService(this);
    }
    
    public void SetGameIsStartedWith(bool newValue)
    {
        _gameStatus.IsGameStarted = newValue;
    }
}
