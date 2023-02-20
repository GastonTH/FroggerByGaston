using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI Win;
    [SerializeField] private TextMeshProUGUI Lose;
    [SerializeField] private TextMeshProUGUI HomeCountTxt;

    private float timeToFinish;
    private int HomeCount;
    private int score;

    private void Start()
    {
        timeToFinish = 100f;
        score = 0;
        HomeCount = 0;
        Lose.gameObject.SetActive(false);
        Win.gameObject.SetActive(false);
    }

    private void Update()
    {

        if(timeToFinish > 0)
        {
            if (HomeCount == 5)
            {
                Win.gameObject.SetActive(true);
            }
            else
            {
                timeToFinish -= Time.deltaTime;
                Timer.text = "Time: " + (int)timeToFinish;
                Score.text = "Score: " + score;
                HomeCountTxt.text = HomeCount + " :Home count";
            }
        }else{
            Lose.gameObject.SetActive(false);
        }
    }

    public void AddScoreByWalk() => score += 10;
    public void AddScoreByWin() => score += 100;
    public void KillInScore() => score-= 50;
    public void AddHomeCount() => HomeCount++;
}
