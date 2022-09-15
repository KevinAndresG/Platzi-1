using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int gameTime;
    public int difficulty = 1;
    [SerializeField] int score = 0;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            if (score % 2000 == 0)
            {
                difficulty++;
            }
        }
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameTime = 30;
        StartCoroutine(CountDownGameTime());
    }
    IEnumerator CountDownGameTime()
    {
        while (gameTime > 0)
        {
            yield return new WaitForSeconds(1);
            gameTime--;
        }
        if (gameTime == 0)
        {
            // Game Over
        }
    }
}
