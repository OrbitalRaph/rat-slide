using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string directoryPath;

    private string filePath;

    public FileDataHandler(string directoryPath, string filePath) {
        this.directoryPath = directoryPath;
        this.filePath = filePath;
    }

    public GameData Load() {
        string fullPath = Path.Combine(directoryPath, filePath);
        GameData loadedData = null;
        if (File.Exists(fullPath)) {
            try {
                // Read the JSON string from the file
                string dataAsJson = "";
                using (FileStream fs = new FileStream(fullPath, FileMode.Open)) {
                    using (StreamReader reader = new StreamReader(fs)) {
                        dataAsJson = reader.ReadToEnd();
                    }
                }

                // Deserialize the JSON string to a GameData object
                loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            } catch (Exception e) {
                Debug.LogError("Error loading data from " + fullPath + ": " + e.Message);
            }
        }
        return loadedData;
    }

    public void Save(GameData gameData) {
        string fullPath = Path.Combine(directoryPath, filePath);
        try {
            // Create the directory if it doesn't exist
            Directory.CreateDirectory(directoryPath);

            // Serialize the GameData object to JSON
            string jsonData = JsonUtility.ToJson(gameData, true);

            // Write the JSON string to the file
            using (FileStream fs = new FileStream(fullPath, FileMode.Create)) {
                using (StreamWriter writer = new StreamWriter(fs)) {
                    writer.Write(jsonData);
                }
            }


        } catch (Exception e) {
            Debug.LogError("Error saving data to " + fullPath + ": " + e.Message);
        }
    }

}
