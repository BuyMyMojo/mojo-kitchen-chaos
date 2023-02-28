using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no KitchenObject here
            if (player.HasKitchenObject())
            {
                // player has object
                player.GetKitchenObject().SetKitchenObjectParent(this);
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            // there is a KitchenObject here
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}
