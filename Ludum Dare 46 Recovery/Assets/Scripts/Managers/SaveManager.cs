using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    const string hp = "Health";
    const string maxHP = "MaxHealth";
    const string speed = "Speed";
    const string pos = "Position";
    const string rot = "Rotation";
    const string x = "X";
    const string y = "Y";
    const string z = "Z";
    const string w = "W";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool HasData(string key)
    {
        Debug.Log("Key " + key);
        bool x = PlayerPrefs.HasKey(key);
        return x;
    }

    public void YeetAllData()
    {
        Debug.Log("Just Send It Boysssss");
        PlayerPrefs.DeleteAll();
    }

    public void WriteChanges() => PlayerPrefs.Save();

    public void SaveData(string key, int val) { PlayerPrefs.SetInt(key, val); PlayerPrefs.Save(); }
    public void SaveData(string key, float val) { PlayerPrefs.SetFloat(key, val); PlayerPrefs.Save(); }
    public void SaveData(string key, string val)  { PlayerPrefs.SetString(key, val); PlayerPrefs.Save(); }

    public int LoadInt(string key) { return PlayerPrefs.GetInt(key); }
    public float LoadFloat(string key) { return PlayerPrefs.GetFloat(key); }
    public string LoadString(string key) { return PlayerPrefs.GetString(key); }

    public void SaveVector3(string key, Vector3 vec)
    {
        SaveData(key + x, vec.x);
        SaveData(key + y, vec.y);
        SaveData(key + z, vec.z);
        PlayerPrefs.Save();
    }

    public Vector3 LoadVector3(string key)
    {
        Vector3 vec;
        vec.x = LoadFloat(key + x);
        vec.y = LoadFloat(key + y);
        vec.z = LoadFloat(key + z);
        return vec;
    }

    public void SaveQuaternion(string key, Quaternion vec)
    {
        SaveData(key + x, vec.x);
        SaveData(key + y, vec.y);
        SaveData(key + z, vec.z);
        SaveData(key + w, vec.w);
        PlayerPrefs.Save();
    }

    public Quaternion LoadQuaternion(string key)
    {
        Quaternion vec;
        vec.x = LoadFloat(key + x);
        vec.y = LoadFloat(key + y);
        vec.z = LoadFloat(key + z);
        vec.w = LoadFloat(key + w);
        return vec;
    }
}
