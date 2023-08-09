using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Gère le chargement et la sauvegarde des données du jeu.
/// </summary>
public class FileDataHandler
{
    private string directoryPath;

    private string filePath;

    public FileDataHandler(string directoryPath, string filePath) {
        this.directoryPath = directoryPath;
        this.filePath = filePath;
    }

    /// <summary>
    /// Charger les données du jeu.
    /// </summary>
    public GameData Load() {
        string fullPath = Path.Combine(directoryPath, filePath);
        GameData loadedData = null;
        if (File.Exists(fullPath)) {
            try {
                // Lit le string JSON du fichier
                string dataAsJson = "";
                using (FileStream fs = new FileStream(fullPath, FileMode.Open)) {
                    using (StreamReader reader = new StreamReader(fs)) {
                        dataAsJson = reader.ReadToEnd();
                    }
                }

                // Désérialise le string JSON en objet GameData
                loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            } catch (Exception e) {
                Debug.LogError("Erreur à charger les données de " + fullPath + ": " + e.Message);
            }
        }
        return loadedData;
    }

    /// <summary>
    /// Cette méthode permet de sauvegarder les données du jeu.
    /// </summary>
    public void Save(GameData gameData) {
        string fullPath = Path.Combine(directoryPath, filePath);
        try {
            // Crée le dossier s'il n'existe pas
            Directory.CreateDirectory(directoryPath);

            // Sérialise l'objet GameData en string JSON
            string jsonData = JsonUtility.ToJson(gameData, true);

            // Écrit le string JSON dans le fichier
            using (FileStream fs = new FileStream(fullPath, FileMode.Create)) {
                using (StreamWriter writer = new StreamWriter(fs)) {
                    writer.Write(jsonData);
                }
            }


        } catch (Exception e) {
            Debug.LogError("Erreur à sauvegarder les données dans " + fullPath + ": " + e.Message);
        }
    }

}
