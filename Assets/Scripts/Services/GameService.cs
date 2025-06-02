using UnityEngine;

public abstract class GameService: MonoBehaviour
{ 
    protected bool IsInstallationDone;

    public abstract void Install();

    public bool IsDone()
    {
        return IsInstallationDone;
    }
}
