using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreBox : MonoBehaviour
{
    public Text score;
    private int totalPoints;
    // Start is called before the first frame update

    public void SetScore(int points)
    {
        totalPoints += points;
        score.text = totalPoints.ToString();
    }

    public int getPoints()
    {
        return totalPoints;
    }
}
