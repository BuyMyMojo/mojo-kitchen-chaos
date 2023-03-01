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
                // player has object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a plate
                    if (plateKitchenObject.TryAddIngreedient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                } else
                {
                    // player not carrying plate but other object
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Counter holds plate
                        if (plateKitchenObject.TryAddIngreedient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }

                    }
                }
            }
            else
            {
                // player has nothing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
