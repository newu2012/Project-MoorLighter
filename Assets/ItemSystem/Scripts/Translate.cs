using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Translate
{
    public static string TranslateWord(string word)
    {
        if (word.Equals("Weapon"))
            return "Оружие";
        if (word.Equals("Armor"))
            return "Броня";
        return null;
    }
}
