using ScriptableObjects.Classes.Ids;
using UnityEngine;

namespace Ui.MenuOptions.Interfaces
{
    public abstract class MenuOption: MonoBehaviour
    {
        [SerializeField] private MenuOptionId optionId;

        public MenuOptionId OptionId => optionId;

        public abstract void Execute();
    }
}