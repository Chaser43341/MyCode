using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateScore : MonoBehaviour
{
    public GameController gameController;
    private int TotalScore=0;
    private int Ballnumber=5;
    
    
    // Start is called before the first frame update
    public Text scoreText;
    public Text ballsRemaining;
    public Button restartButton;
    void Start()
    {
        
        Messenger.OnScoreUpdate += HandleScoreUpdate;
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        
    }

    // Update is called once per frame
  
    void HandleScoreUpdate(int score)
    {
        if(Ballnumber!=0)
        {
            Debug.Log("Score Updated: " + score);
            TotalScore +=score;
            scoreText.text = "Total Score: "+ TotalScore.ToString();
            Ballnumber--;
            ballsRemaining.text ="Balls remain:"+ Ballnumber.ToString();
        }
        else
        {
            
            scoreText.text = "Final Score: "+ TotalScore.ToString();
        }
    }
    void DestroyAllBullets()
    {
        // 找到所有带有 "Bullet" 字符串的对象
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        // 遍历并销毁它们
        foreach (var bullet in bullets)
        {
            // 判断对象名字是否以 "Bullet(clone)" 结尾
            if (bullet.name.EndsWith("(Clone)"))
            {
                Destroy(bullet);
            }
        }
    }
    void OnRestartButtonClicked()
    {
        scoreText.text = "Total Score: 0";
        ballsRemaining.text ="Balls remain: 5";
        TotalScore=0;
        Ballnumber=5;
        DestroyAllBullets();
        Debug.Log("Restart Button Clicked");

        
    }
    
}
