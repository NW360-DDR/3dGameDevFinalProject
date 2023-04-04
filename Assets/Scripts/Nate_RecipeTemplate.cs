using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Recipes/New Ingredient")]
public class Nate_RecipeTemplate : ScriptableObject
{
    public string[] parts;
    public int minCook;
    public int maxCook;
}
