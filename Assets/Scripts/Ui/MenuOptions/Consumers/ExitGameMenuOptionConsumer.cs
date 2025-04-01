using Ui.MenuOptions.Interfaces;
using UnityEngine;

public class ExitGameMenuOptionConsumer : MenuOption
{
    public override void Execute()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
