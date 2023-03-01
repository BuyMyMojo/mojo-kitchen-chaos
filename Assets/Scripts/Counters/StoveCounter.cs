using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    private float fryingTimer;

    private void Update()
    {
        if (HasKitchenObject())
        {
            fryingTimer += Time.deltaTime;
            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            if (fryingTimer > fryingRecipeSO.fryingTimerMax)
            {
                // Fried
                fryingTimer = 0f;
                Debug.Log("Fried!");
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

            }
            Debug.Log(fryingTimer);
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no KitchenObject here
            if (player.HasKitchenObject())
            {
                // player has object
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // player is carrying an object that can be Fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else
            {
                // player has nothing, do nothing
            }
        }
        else
        {
            // KitchenObject is here
            if (player.HasKitchenObject())
            {
                // player has object, do nothing
            }
            else
            {
                // player has nothing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;

    }

}
