using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataCtrl : MonoBehaviour
{
    public static DataCtrl instance = null;
    public GameData data;                      
    public bool devMode;
    
    string dataFilePath;                        // the path where the data file is stored
    BinaryFormatter bf;                         // save/load given in binary file

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);             // this allows no more DataCtrl objects to be created, but if one is instantiated at the beginning that will follow us through all the scenes, then everyone else is destroyed
        }

        bf = new BinaryFormatter();

        dataFilePath = Application.persistentDataPath + "/game.dat";
        Debug.Log(dataFilePath);
    }

    public void RefreshData()
    {
        if (File.Exists(dataFilePath))
        {
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData)bf.Deserialize(fs);
            fs.Close();

            Debug.Log("Data Refreshed");
        }
    }

    public void SaveData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Data Refreshed");
    }

    public void SaveData(GameData data)
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Data Refreshed");
    }

    public bool isUnlocked(int levelNumber)
    {
        return data.levelData[levelNumber].isUnlocked;
    }

    public int getStars(int levelNumber)
    {
        return data.levelData[levelNumber].starsAwarded;
    }
    //public int getHighScore(int LevelhighScore)
    //{
    //    return data.levelData[LevelhighScore].LevelhighScore;
    //}

    void OnEnable()
    {
        CheckDB();
    }

    void CheckDB()
    {
        if (!File.Exists(dataFilePath))
        {
            #if UNITY_ANDROID
            CopyDB();
#endif
        }
        else
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                string destFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
                File.Delete(destFile);
                File.Copy(dataFilePath, destFile);

            }

            if (devMode)
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    File.Delete(dataFilePath);
                    CopyDB();
                }
            }
            RefreshData();
        }
    }

    void CopyDB()
    {
        string scrFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
        WWW downloader = new WWW(scrFile);

        while (!downloader.isDone)
        {
            // nothing to nbe done while downloader gets our db file

        }

        // then save to Application.persistentDataPath
        File.WriteAllBytes(dataFilePath, downloader.bytes);
        RefreshData();
    }

}
