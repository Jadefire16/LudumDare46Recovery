using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public event Action OnPlayerDeathEvent;
    public event Action OnSaveGameEvent;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
    }

    public void InvokePlayerDeath() => OnPlayerDeathEvent?.Invoke();
    public void InvokeSaveGame() => OnSaveGameEvent?.Invoke();
}
