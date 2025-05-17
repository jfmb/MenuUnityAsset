using System.Collections.Generic;
using NUnit.Framework;
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

        var listOfSettings = new List<SubOptionSO>();
        foreach (var setting in settingsMenuSO.AllSubOptions)
        {
            listOfSettings.Add(setting);
        }
        _gameSettings.SetupSettings(listOfSettings);
    }

    public void SetSettingWithValue(string key, int newValue)
    {
        var gameSettings = _gameSettings.Settings;
        if (gameSettings.ContainsKey(key))
        {
            gameSettings[key] = newValue;
            return;
        }
        
        gameSettings.Add(key, newValue);
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
