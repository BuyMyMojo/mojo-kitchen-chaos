using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    private const string NUMBER_POPUP_ANIMATION = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        int countdownNumber = Mathf.CeilToInt(GameStateManager.Instace.GetCountdownToStartTimer());
        if (countdownNumber != previousCountdownNumber)
        {
            previousCountdownNumber = countdownNumber;

            animator.SetTrigger(NUMBER_POPUP_ANIMATION);

            countdownText.text = countdownNumber.ToString();

            SoundManager.Instance.PlayCountdown();

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
