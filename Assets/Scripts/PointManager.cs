using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{

    public int Score;
    public TMP_Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateScore(int points)
    {
        Score += points;
        ScoreText.text = "Score: " + Score;
    }
}
