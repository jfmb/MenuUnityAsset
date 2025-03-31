using UnityEngine;

namespace ScriptableObjects.Languages
{
    [CreateAssetMenu(fileName = "LanguagesAvailable", menuName = "ScriptableObjects/Languages/LanguagesAvailableSO", order = 0)]
    public class LanguagesAvailableSO : ScriptableObject
    {
        [SerializeField] private LanguageSO[]  twoLettersIsoLanguages;
        [SerializeField] private LanguageSO defaultLanguage;
        public LanguageSO[] TwoLettersIsoLanguages => twoLettersIsoLanguages;

        public LanguageSO DefaultLanguage => defaultLanguage;
    }
}