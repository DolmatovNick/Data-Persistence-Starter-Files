using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class UIDataManager : MonoBehaviour
{

    [SerializeField] Text PlayerNameText;

    public static UIDataManager Instance;

    public string PlayerName { get; private set; }
    public int PlayerBestScore{ get; set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartScene(int index)
    {
        PlayerName = PlayerNameText.text;
        SceneManager.LoadScene(index);
    }

    public class PlayerData
    {
        public string Name;
        public int Score;
    }

    public void StoreData()
    {
        if (!IsTheBestScore()) 
        {
            return;
        }
        
        string json = JsonUtility.ToJson(
            new PlayerData
            {
                Name = PlayerName,
                Score = PlayerBestScore
            }
        );
        File.WriteAllText(Application.persistentDataPath + "/playerdata.json", json);
    }

    bool IsTheBestScore()
    {
        PlayerData data = GetStoredPlayerData();
        if (data == null) {
            return true;
        }

        return data.Score < PlayerBestScore;
    }

    public void LoadData()
    {
        PlayerData data = GetStoredPlayerData();
        if (data != null)
        {
            PlayerName = data.Name;
            PlayerBestScore = data.Score;
        }
    }

    public PlayerData GetStoredPlayerData()
    {
        string path = Application.persistentDataPath + "/playerdata.json";

        if (!File.Exists(path)) {
            return null;
        }
        
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerData>(json);
    }




}
