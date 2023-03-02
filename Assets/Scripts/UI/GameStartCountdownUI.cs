using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        GameStateManager.Instace.OnStateChanged += GameStateManager_OnStateChanged;

        Hide();
    }

    private void GameStateManager_OnStateChanged(object sender, System.EventArgs e)
    {
        
        if (GameStateManager.Instace.IsCountdownToStartActive())
        {
            Show();
        } else
        {
            Hide();
        }

    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameStateManager.Instace.GetCountdownToStartTimer()).ToString();
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
