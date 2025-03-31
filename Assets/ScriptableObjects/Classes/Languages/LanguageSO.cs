using UnityEngine;

namespace ScriptableObjects.Languages
{
    [CreateAssetMenu(fileName = "languageSO", menuName = "ScriptableObjects/Languages/NewLanguage", order = 0)]
    public class LanguageSO : ScriptableObject
    {
        [SerializeField] private string _twoLetterValidIsoLanguage;
        [SerializeField] private string _longNameLanguage;

        public string twoLetterValidIsoLanguage => _twoLetterValidIsoLanguage;

        public string LongNameLanguage => _longNameLanguage;
    }
}