using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    
    public static Tuple<int, string> LoadLastScore() {
        int lastScore = PlayerPrefs.GetInt("score", 0);
        string lastTime = PlayerPrefs.GetString("time", "");
        return new Tuple<int, string>(lastScore, lastTime);
    }
    
    public static List<KeyValuePair<int, string>> LoadAllScores() {
        List<KeyValuePair<int, string>> scores = new List<KeyValuePair<int, string>>();

        for (int i = 1; i <= 10; i++)
        {
            if (PlayerPrefs.HasKey("Score" + i) && PlayerPrefs.HasKey("Time" + i))
            {
                int score = PlayerPrefs.GetInt("Score" + i);
                string time = PlayerPrefs.GetString("Time" + i);
                scores.Add(new KeyValuePair<int, string>(score, time));
            }
        }

        return scores;
    }

    
    public static void SaveScore(int score, string time) {
        // Stocker le score et le temps de la dernière partie
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetString("time", time);

        // Récupérer les scores existants à partir des préférences du joueur
        List<KeyValuePair<int, string>> highScores = new List<KeyValuePair<int, string>>();
        for (int i = 1; i <= 10; i++)
        {
            if (PlayerPrefs.HasKey("Score" + i) && PlayerPrefs.HasKey("Time" + i))
            {
                int existingScore = PlayerPrefs.GetInt("Score" + i);
                string existingTime = PlayerPrefs.GetString("Time" + i);
                highScores.Add(new KeyValuePair<int, string>(existingScore, existingTime));
            }
        }

        // Ajouter le score et le temps de la dernière partie à la liste
        highScores.Add(new KeyValuePair<int, string>(score, time));

        // Trier les scores par ordre décroissant en fonction du score
        highScores.Sort((x, y) => y.Key.CompareTo(x.Key));
        highScores.Reverse();


        // Enregistrer les 10 meilleurs scores dans les préférences du joueur
        for (int i = 1; i <= 10; i++)
        {
            if (i <= highScores.Count)
            {
                PlayerPrefs.SetInt("Score" + i, highScores[i - 1].Key);
                PlayerPrefs.SetString("Time" + i, highScores[i - 1].Value);
            }
            else
            {
                // Effacer les scores inutilisés
                PlayerPrefs.DeleteKey("Score" + i);
                PlayerPrefs.DeleteKey("Time" + i);
            }
        }
    }
    
    
    
}