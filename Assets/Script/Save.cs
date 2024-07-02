using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameData
{
    public List<int> skillIDList = new List<int>();
    public int gold;
}
public class Save : MonoSingleton<Save>
{
    public GameData gameData = new GameData();
    string path = Application.streamingAssetsPath + "/" + "Config.txt";  
    public void Init()
    {
        if (!File.Exists(path))
        {
            gameData.gold = 2;
            SaveData();

        }
        else
        {
            StartCoroutine(FileUtils.ReadData(path, SetData));

        }
    }
    public void SetData(string json)  
    {
        gameData = JsonConvert.DeserializeObject<GameData>(json);
        KnapsackData.Instance.money = gameData.gold;
    }
    public void SaveData()
    {
        if (gameData.skillIDList.Count==0)
        {
            Debug.LogError("The game data is empty");

        }
        else
        {
            string json = JsonConvert.SerializeObject(gameData);
            File.WriteAllText(path, json);
        }
    }
    public void OnDestroy()
    {
        SaveData();
    }
}
