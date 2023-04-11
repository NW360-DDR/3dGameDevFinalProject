using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShortcutManagement; commented out so the game can compile correctly -andrew
using UnityEngine;

public class Nate_RecipeManager : MonoBehaviour
{
    // ATTENTION: THIS SCRIPT SHOULD NOT BE ACCESSED IF YOU WANT TO MAKE NEW RECIPES OR INGREDIENTS
     

    [SerializeField] Nate_RecipeTemplate[] RecipeList;

    // The structure for a Recipe in theory should be three things
    // 1) An array of ingredients,
    // 2) The "Cook Range", and
    // 3) The Resulting food.
    // These things are contained in 

    public Nate_RecipeTemplate RecipeCheck(string[] parts)
    {
        Array.Sort(parts);

        foreach(Nate_RecipeTemplate jimmy in RecipeList)
        {
            if (RecipeCompare(jimmy.parts, parts))
            {
                return jimmy;
            }
        }
        return RecipeList[^1];
    }

    bool RecipeCompare(string[] a, string[] b)
    {
        // If the recipe lists aren't even the same, what are we doing here?
        if (a.Length != b.Length)
            return false;

        //Counterintuitively, we check to see if the list ISN'T the same to prove it is.
        for (var i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
                return false;
        }
        return true;
    }
}
