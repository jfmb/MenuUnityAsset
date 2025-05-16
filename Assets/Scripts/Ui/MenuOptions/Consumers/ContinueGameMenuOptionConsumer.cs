using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Ui.MenuOptions.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

public class ContinueGameMenuOptionConsumer : MenuOption
{
    [SerializeField] private EventId gameContinuesEventId;

    public override void Execute()
    {
        //TODO: unpause game
        ContinueGame();
    }

    private void ContinueGame()
    {
        Assert.IsNotNull(gameContinuesEventId, "SettingsInGameClosedEventId can't be null");
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(gameContinuesEventId, EventArgs.Empty);
    }
}
