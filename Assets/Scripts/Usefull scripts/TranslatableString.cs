using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TranslatableString 
{
    public const int LANG_EN = 0;
    public const int LANG_RU = 1;

    [SerializeField] private string english;
    [SerializeField] private string russian;

    public static implicit operator string(TranslatableString translatableString)
    {
        int languageId = PlayerPrefs.GetInt("language_id");
        switch (languageId) {
            case LANG_EN:
                return translatableString.english;
            case LANG_RU:
                return translatableString.russian;
        }
        Debug.LogError("Wrong languageId in config");
        return translatableString.english;
    }
}