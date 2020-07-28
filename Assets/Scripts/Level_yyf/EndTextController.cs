using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTextController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject WinText;
    GameObject GameOverText;
    GameObject Panel;
    GameObject Tips;
    void Start()
    {
        GameOverText = GameObject.Find("GameOver");
        WinText = GameObject.Find("GameWin");
        Panel = GameObject.Find("Panel");
        Tips = GameObject.Find("Tips");
        GameOverText.SetActive(false);
        WinText.SetActive(false);
        Panel.SetActive(false);
        Tips.SetActive(false);
    }

    public void YouWin(int score)
    {
        Panel.SetActive(true);
        WinText.SetActive(true);
        Tips.SetActive(true);
        Text winText = WinText.GetComponent<Text>();
        if (winText != null)
        {
            winText.text = "You Win!\nYour Final Energy: " + score.ToString();
        }
    }
    public void YouLose()
    {
        GameOverText.SetActive(true);
        Panel.SetActive(true);
        Tips.SetActive(true);
    }

}
