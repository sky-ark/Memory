using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWon : MonoBehaviour
{
    public TMP_Text _lastscoreDisplay;
    //public TMP_Text _timeDisplay;
    //public TMP_Text _highScoresDisplay;
    public GameObject scorePrefab;

    public GameObject scoreContainer;
    // Start is called before the first frame update
    void Start()
    {
        Tuple<int, string> tuple = Utils.LoadLastScore();
        _lastscoreDisplay.text = tuple.Item1 + " essais " + " avec un temps de " + tuple.Item2;
        Debug.Log("This score : " + tuple.Item1 + " in " + tuple.Item2);
        List<KeyValuePair<int, string>> list = Utils.LoadAllScores();
        for (var i = 0; i < list.Count; i++) {
            (int key, string value) = list[i];
            Debug.Log("High score : " + key + " in " + value);
            GameObject instantiate = Instantiate(scorePrefab, scoreContainer.transform);
            instantiate.GetComponent<TMP_Text>().text = i + 1 + ". " + key + " - " + value;
        }
        // int score = PlayerPrefs.GetInt("score");
        // _scoreDisplay.text = score.ToString() + " essais ";
        // string TimeAtEnd = PlayerPrefs.GetString("TimeAtEnd");
        // _timeDisplay.text = " Avec un temps de " + TimeAtEnd;

    }


    
}