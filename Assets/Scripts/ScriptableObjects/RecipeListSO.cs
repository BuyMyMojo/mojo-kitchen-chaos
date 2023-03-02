using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Commented out for saftey. There should only be one master list of valid recipes.
// [CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
}
