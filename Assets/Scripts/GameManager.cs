using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static readonly string highScoreFile = @"TurnipKnight/highscore.txt";
    public static AudioSource soundSource;
    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int GetHighScore()
    {
        int highscore = 0;

        return highscore;
    }

    public static void SaveHighScore()
    {

    }
}
