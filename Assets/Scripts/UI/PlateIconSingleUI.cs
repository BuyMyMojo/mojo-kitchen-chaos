using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    // For some reason it was pulling Image from somewhere else randomly near the end of development so I had to specify UnityEngine.UI.Image??
    [SerializeField] private UnityEngine.UI.Image image;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }

}
