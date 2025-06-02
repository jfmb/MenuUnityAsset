using ScriptableObjects;
using UnityEngine;

public class GameInfoFacade: GameService
{
    [SerializeField] private MenuSO settingsMenuSO;
    
    private GameStatus _gameStatus = new();
    private GameSettings _gameSettings = new();
    
    public override void Install()
    {
        DontDestroyOnLoad(gameObject);
        ServiceLocator.RegisterService(this);

        var listOfSettings = settingsMenuSO.AllSubOptions;

        _gameSettings.SetupSettings(listOfSettings);

        IsInstallationDone = true;
    }

    public int GetLastSettingsValueSelectedFromKey(string key)
    {
        if (_gameSettings.Settings.ContainsKey(key))
        {
            return _gameSettings.Settings[key];
        }

        return -1;
    }
    
    public void SaveSettingNewValueWithKey(string key, int newValue)
    {
        _gameSettings.SaveNewSettingsValueWithKey(key, newValue);
    }
    
    public void SetGameIsStartedWith(bool newValue)
    {
        _gameStatus.IsGameStarted = newValue;
    }

    public void AddNewSetting(string key, int value)
    {
        _gameSettings.Settings.Add(key, value);
    }
}
