using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{


    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no KitchenObject here
            if (player.HasKitchenObject())
            {
                // player has object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else
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

}
