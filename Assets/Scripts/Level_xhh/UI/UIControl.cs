using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public static UIControl UnityIns;

    public Text DeadReason;
    public Text ScoreText;
    public GameObject RestartButton;
    public GameObject BackToMainButton;
    public float ScoreUpdateT;

    private int NowScore = 0;

    private int TargetScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        UIControl.UnityIns = this;

        this.RestartButton.SetActive(false);
        this.BackToMainButton.SetActive(false);
        this.DeadReason.gameObject.SetActive(false);

        InvokeRepeating("UpdateScore", 0, this.ScoreUpdateT);
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void UpdateScore()
    {
        if (this.NowScore != this.TargetScore)
        {
            this.NowScore++;
            this.ScoreText.text = this.NowScore.ToString();
        }
    }


    public void AddScore(int num)
    {
        this.TargetScore += num;
        Debug.Log(this.TargetScore);
    }


    public void SetDeadReason(string reason)
    {
        this.DeadReason.text = reason.Length > 0 ? reason : "ow, you dead !!";
    }

    public void HeroDead()
    {
        this.RestartButton.SetActive(true);
        this.BackToMainButton.SetActive(true);
        this.DeadReason.gameObject.SetActive(true);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level_xhh");
    }
}
