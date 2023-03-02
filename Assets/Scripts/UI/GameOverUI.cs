using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;


    private void Start()
    {
        GameStateManager.Instace.OnStateChanged += GameStateManager_OnStateChanged;

        Hide();
    }

    private void GameStateManager_OnStateChanged(object sender, System.EventArgs e)
    {

        if (GameStateManager.Instace.IsGameOver())
        {
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();

            Show();
        }
        else
        {
            Hide();
        }

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
