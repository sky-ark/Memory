using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chrono : MonoBehaviour
{

    public TMP_Text chronoDisplay;
    private int minutes = 0;
    private int seconds = 0;
    private int milliseconds = 0;

    public static string currentChronoValue = "00:00:000";

    void Update()
    {
        // Incrémente les millisecondes
        milliseconds += (int) (Time.deltaTime * 1000);

        // Si les millisecondes atteignent 1000, incrémentez les secondes et réinitialisez les millisecondes
        if (milliseconds >= 1000)
        {
            seconds++;
            milliseconds = 0;
        }

        // Si les secondes atteignent 60, incrémentez les minutes et réinitialisez les secondes
        if (seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }

        // Met à jour la variable currentChronoValue
        currentChronoValue = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");
        chronoDisplay.text = currentChronoValue;
    }

    public void StopChrono()
    {
        // Arrête le chronomètre
        enabled = false;

        // Stocke la valeur actuelle du chronomètre
        currentChronoValue = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");
        // PlayerPrefs.SetString("TimeAtEnd", currentChronoValue);
    }

   // void OnGUI()
    //{
      //  GUIStyle guiStyle = new GUIStyle();
        //guiStyle.fontSize = 50; 

       // GUI.Label(new Rect(10, 10, 300, 50), "Chronomètre : " + currentChronoValue, guiStyle);
    //}
}