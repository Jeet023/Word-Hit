using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] Buttons;
    public string[] Words = new string[] { "Ncu", "Games", "AI", "Physics", "Render", "Audio", "Video" };
    public Text WordToHitText, LifeText, ScoreText, HighScoreText;
    public static string WordToHit;
    public static int Score = 0;
    public static int Lives = 3;
    private int highScore = 0;
    public bool[] PressedButton;
    public bool[] FirstPress;
    public GameObject Restarter;

    bool gameover = false;
    bool LifeLost = false;
    bool blank = true;


    public void Retry()
    {
        gameover = false;
        if (highScore < Score)
            highScore = Score;
        Score = 0;
        Lives = 3;
        HighScoreText.text = highScore.ToString();
        UpdateWords();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ScoreUpdate(int ButtonNumber)
    {
        if (FirstPress[ButtonNumber])
        {
            FirstPress[ButtonNumber] = false;
            Score += 2;
            print(Score);
        }
        ScoreText.text = Score.ToString();


    }

    void UpdateWords()
    {
        if (gameover)
            Restart();

        Restarter.gameObject.SetActive(false);

        LifeLost = false;


        for (int i = 0; i < Buttons.Length; ++i)
        {
            var index = Random.Range(0, Words.Length);
            Buttons[i].GetComponentInChildren<Text>().text = Words[index];
            Buttons[i].gameObject.SetActive(true);
            PressedButton[i] = false;
            FirstPress[i] = true;
        }
        LifeText.text = Lives.ToString();
        ScoreText.text = Score.ToString();
        HighScoreText.text = highScore.ToString();

        var wordToHitIndex = Random.Range(0, Buttons.Length);
        WordToHit = Buttons[wordToHitIndex].GetComponentInChildren<Text>().text;
        WordToHitText.text = WordToHit;

        // Disable method is called after 3 seconds
        Invoke("Disable", 3f);
    }

    void LivesLost(int ButtonNumber)
    {
        if (!LifeLost)
        {
            if (FirstPress[ButtonNumber])
            {
                FirstPress[ButtonNumber] = false;
                Lives -= 1;
                print("Lives = " + Lives);
                LifeLost = true;
            }
        }
        LifeText.text = Lives.ToString();
    }

    public void Click(int ButtonNumber)
    {
        if (!LifeLost)
        {
            PressedButton[ButtonNumber] = true;
            for (int i = 0; i < Buttons.Length; ++i)
            {
                if (Buttons[i].GetComponentInChildren<Text>().text == WordToHitText.text && PressedButton[i])
                {
                    Buttons[i].GetComponent<Image>().color = Color.green;
                    ScoreUpdate(i);
                    blank = false;

                }
                else if (!(Buttons[i].GetComponentInChildren<Text>().text == WordToHitText.text) && PressedButton[i])
                {
                    Buttons[i].GetComponent<Image>().color = Color.red;
                    LivesLost(i);
                    blank = false;
                }



                /*    var index = Random.Range(0, Words.Length);
                    Buttons[i].GetComponentInChildren<Text>().text = Words[index];
                    Buttons[i].gameObject.SetActive(true);*/

            }
        }

        if (Lives == 0)
        {
            gameover = true;
            print("dead");
        }
        }
    // Start is called before the first frame update
    void Start()
    {
       UpdateWords();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Restart()
    {
        Restarter.gameObject.SetActive(true);

        while(gameover)
        {  
            Restart();
        };
    }


    void Disable()
    {
        foreach (var button in Buttons)
        {
            button.GetComponent<Image>().color = Color.white;
            button.gameObject.SetActive(false);
        }


        WordToHitText.text = "";

        if (blank)
            Lives--;

        LifeText.text = Lives.ToString();


        if (Lives == 0)
        {
            gameover = true;
            print("dead");
            Restart();
            // UpdateWords method is called after 2 seconds
        }
            Invoke("UpdateWords", 2f);
        
    }
}
