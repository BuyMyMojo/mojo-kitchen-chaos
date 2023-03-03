using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{

    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animator.SetBool(IS_FLASHING, false);

    }

    private void Start()
    {
        stoveCounter.OnProgressChange += StoveCounter_OnProgressChange; 
    }

    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventsArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool play = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, play);
    }

}
