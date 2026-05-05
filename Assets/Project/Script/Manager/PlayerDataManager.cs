using UnityEngine;
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    public PlayerData data;

    private string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath + "/playerdata.json";

            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved");
    }

    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<PlayerData>(json);

            Debug.Log("Game Loaded");
        }
        else
        {
            data = new PlayerData();
            Save();
        }
        if (string.IsNullOrEmpty(data.playerID))
        {
            data.playerID = GenerateUniqueID();
            Debug.Log("Generated Player ID: " + data.playerID);
            Save();
        }
        if (string.IsNullOrEmpty(data.username))
        {
            data.username = GenerateRandomUsername();
            Debug.Log("Generated Username: " + data.username);
            Save();
        }
    }
    string GenerateUniqueID()
    {
        return System.Guid.NewGuid().ToString();
    }
    string GenerateRandomUsername()
    {
        string[] adjectives = { "Silent", "Dark", "Shadow", "Swift", "Deadly", "Ghost", "Hidden", "Night" };
        string[] nouns = { "Hunter", "Assassin", "Ninja", "Sniper", "Blade", "Reaper", "Stalker", "Phantom" };

        string adj = adjectives[Random.Range(0, adjectives.Length)];
        string noun = nouns[Random.Range(0, nouns.Length)];
        int number = Random.Range(10, 999);

        return adj + noun + number;
    }
}