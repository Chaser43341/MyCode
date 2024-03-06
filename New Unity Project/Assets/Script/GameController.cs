
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    
    private int totalScore;

    private bool isFirstCollision = true;

    void Start()
    {
        
    }
     public int GetTotalScore()
    {
        return totalScore;
    }
    
    void OnCollisionEnter(Collision other)

{
         if (other.collider.tag=="Bullet"&& isFirstCollision)
            {
                
                int planeScore = GetPlaneScore(other.gameObject);
                totalScore = planeScore;
                Debug.Log(totalScore.ToString());
                Messenger.SendScoreUpdate(totalScore);
                isFirstCollision = false;
            }
}
    

    int GetPlaneScore(GameObject plane)
    {
        
        int planeScore=0;
        
        // 获取 Plane 的数字
        string digitString = plane.name.Substring("board".Length);
        
        if (int.TryParse(digitString, out int digit))
        {
            // 在这里可以根据 Plane 的数字定义分数
            planeScore = digit;
        }

        return planeScore;
    }


}
