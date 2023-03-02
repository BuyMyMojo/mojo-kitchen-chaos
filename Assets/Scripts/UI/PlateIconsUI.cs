using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngreedientAdded += PlateKitchenObject_OnIngreedientAdded;
    }

    private void PlateKitchenObject_OnIngreedientAdded(object sender, PlateKitchenObject.OnIngreedientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
            
            iconTransform.gameObject.SetActive(true);
        }
    }
}
