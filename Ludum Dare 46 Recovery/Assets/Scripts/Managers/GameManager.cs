using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    const string hp = "Health";
    const string maxHP = "MaxHealth";
    const string speed = "Speed";
    const string pos = "Position";
    const string rot = "Rotation";
    const string x = "X";
    const string y = "Y";
    const string z = "Z";
    const string w = "W";
    const string manager = "Manager";
    const string deaths = "Deaths";
    const string difficulty = "Difficulty";

    public static string playerName;

    public string saveKey = "player1";

    public static GameManager instance;

    int playerDeaths = 0;
    Difficulty gameDifficulty;
    GameState gameState;
    internal static float killLimitY = -50f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
        if (playerName == null)
            playerName = FindObjectOfType<PlayerClass>().name;
    }

    private void Start()
    {
        EventManager.instance.OnPlayerDeathEvent += OnPlayerDeath;
        EventManager.instance.OnSaveGameEvent += Save;
        Load();
    }

    private void Save()
    {
        SaveManager.instance.SaveData(manager + deaths, playerDeaths);
        SaveManager.instance.SaveData(manager + difficulty, (int)gameDifficulty);
        SavePlayer(saveKey);
    }

    public void SavePlayer(string key)
    {
        PlayerClass player = FindObjectOfType<PlayerClass>();
        SaveManager.instance.SaveData(key, key);
        SaveManager.instance.SaveData(key + hp, player.GetHealth());
        SaveManager.instance.SaveData(key + maxHP, player.GetMaxHealth());
        SaveManager.instance.SaveData(key + speed, player.GetSpeed());
        SaveManager.instance.SaveVector3(key + pos, player.GetPosition());
        SaveManager.instance.SaveQuaternion(key + rot, player.GetRotation());
    }

    private void Load()
    {
        playerDeaths = SaveManager.instance.LoadInt(manager + deaths);
        gameDifficulty = (Difficulty)SaveManager.instance.LoadInt(manager + difficulty);

        if (SceneManager.GetActiveScene().name == "Menu")
            gameState = GameState.Menu; // will search to see if the active scene is named "Menu" if it is, it will set gamestate to menu
        else
            gameState = GameState.Active;
        LoadPlayer(saveKey);
    }

    private bool LoadPlayer(string key)
    {
        PlayerClass player = FindObjectOfType<PlayerClass>();
        if (SaveManager.instance.HasData(key) == false)
        { 
            Debug.Log("Could Not Load Player");
            player.FirstTimeLoad(player.data);
            return false; 
        }
        int playerHP = SaveManager.instance.LoadInt(key + hp);
        int playerMaxHP = SaveManager.instance.LoadInt(key + maxHP);
        float playerSpeed = SaveManager.instance.LoadFloat(key + speed);
        Vector3 playerPos = SaveManager.instance.LoadVector3(key + pos);
        Quaternion playerRot = SaveManager.instance.LoadQuaternion(key + rot);
        player.InitialSetup(playerHP,playerMaxHP,playerSpeed,playerPos,playerRot);
        return true;
    }

    private void OnPlayerDeath()
    {
        playerDeaths++;
    }

    public void ToggleGameState(bool isActive)
    {
        gameState = isActive ? GameState.Active : GameState.Paused;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Menu:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0.01f;
                break;
            case GameState.Active:
                Time.timeScale = 1f;
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SavePlayer(saveKey);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadPlayer(saveKey);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveManager.instance.YeetAllData();
        }
    }

    public Difficulty GameDifficulty
    {
        get => gameDifficulty;
        set
        {
            if (gameState == GameState.Menu) gameDifficulty = value;
            else { return; }
        }
    }

    public int PlayerDeaths { get => playerDeaths; set => playerDeaths = value; }
}

public enum Difficulty
{
    VeryEasy = 0,
    Easy = 1,
    Normal = 2,
    Hard = 3,
    Nightmare = 4
}

public enum GameState
{
    Menu,
    Paused,
    Active
}