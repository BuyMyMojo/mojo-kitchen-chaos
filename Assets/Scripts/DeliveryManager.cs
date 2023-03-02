using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];

                Debug.Log(waitingRecipeSO.recipeName);

                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i=0; i < waitingRecipeSOList.Count; ++i)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSoList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Has equal ingreedients on plate

                bool plateContentsMatchesRecipe = true;

                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSoList)
                {
                    // Cycle through all ingreedients within recipe

                    bool ingreedientFound = false;

                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // Cycle through all ingreedients within plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // Ingreedient matches!
                            ingreedientFound = true;
                            break;
                        }
                    }

                    if (!ingreedientFound)
                    {
                        // This Recipe ingreedient was not on plate
                        plateContentsMatchesRecipe = false;
                    }

                    if (plateContentsMatchesRecipe)
                    {
                        // Player delivered correct recipe
                        Debug.Log("Player delivered a recipe from the waiting list!");

                        waitingRecipeSOList.RemoveAt(i);

                        return;
                    }
                }
            }
        }

        // No matches found!
        // Player brought the wrong recipe!
        Debug.Log("Player brought the wrong recipe!");
    }

}
