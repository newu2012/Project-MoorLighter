using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParseEngRecipe
{
    public static List<(string, int)> Parse(string engRecipe)
    {
        var result = new List<(string, int)>();
        var parts = engRecipe.Split(';');
        foreach (var part in parts)
        {
            var tuple = (part.Split(' ')[0], int.Parse(part.Split(' ')[1]));
            result.Add(tuple);
        }
        return result;
    }
}
