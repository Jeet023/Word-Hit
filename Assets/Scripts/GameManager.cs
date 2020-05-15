using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] Buttons;
    public string[] Words = new string[] { "ncu", "games", "ai", "physics", "render", "audio", "video" };
    public Text WordToHitText;
    public static string WordToHit;
    public static int Score;
    public static int Lives = 3;
    private int highScore;


    // Start is called before the first frame update
    void Start()
    {
        UpdateWords();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Disable()
    {
        for(var button in Buttons)
        {
            button.GetComponent<Image>().color = Color.white;
            button.gameObject.SetActive(false);
        }

        WordToHitText.text = "";

        // UpdateWords method is called after 2 seconds
        Invoke("UpdateWords", 2f);
    }

    void UpdateWords()
    {
        for(i=0;i<Buttons.Length;++i)
        {
            var index = Random.Range(0, Words.Length+1);
            Buttons[i].GetComponentInChildren<Text>().text = Words[index];
            Buttons[i].gameObject.SetActive(true);
        }

        var wordToHitIndex = Random.Range(0, Buttons.Length);
        WordToHit = Buttons[wordToHitIndex].GetComponentInChildren<Text>().text;
        WordToHitText.text = WordToHit;

        // Disable method is called after 3 seconds
        Invoke("Disable", 3f);
    }
}
