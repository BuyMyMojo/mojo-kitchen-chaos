using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    
    public static GameStateManager Instace{  get; private set; }

    public event EventHandler OnStateChanged;

    private enum State
    {
        WaitingToStart,
        StartingCountdown,
        GamePlaying,
        GameOver,
    }

    private State state;

    // --- Timers ---
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayTimer;
    private float gamePlayTimerMax = 10f;

    private void Awake()
    {
        state = State.WaitingToStart;
        Instace = this;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.StartingCountdown;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.StartingCountdown:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;

                    gamePlayTimer = gamePlayTimerMax;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayTimer -= Time.deltaTime;
                if (gamePlayTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

        Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.StartingCountdown;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimerNomalized()
    {
        return gamePlayTimer / gamePlayTimerMax;
    }

}
