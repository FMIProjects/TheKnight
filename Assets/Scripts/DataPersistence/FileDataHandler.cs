using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
  private string dataPath = "";

  private string dataFileName = "data.json";

    public FileDataHandler(string dataPath, string dataFileName)
    {
        this.dataPath = dataPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullpath = Path.Combine(dataPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullpath))
        {
            try
            {
                // Reading the data from a file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullpath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Deserializing the data
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load data: " + e.Message);
            }
        }
        return loadedData;
    }

    public void Save(GameData data) 
    {
        string fullpath = Path.Combine(dataPath, dataFileName);

        try
        {
            // Creating a directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));

            // Serializing the data
            string dataToStore = JsonUtility.ToJson(data,true);

            // Writing the data to a file
            using (FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save data: " + e.Message);
        }
    }
}
