using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour
{
    public TextMeshProUGUI coinText, scoreText, maxScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = 0f;
            float maxScore = 0f;

            coinText.text = coins.ToString();
            scoreText.text = "Score: " + score.ToString("f1");
            maxScoreText.text = "MaxScore: " + coins.ToString("f1");
        }
    }
}
