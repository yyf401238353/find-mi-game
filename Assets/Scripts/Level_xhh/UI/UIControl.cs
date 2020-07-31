using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    private static string DeadReasonString;

    public Text DeadReason;
    public GameObject RestartButton;
    public GameObject BackToMainButton;

    // Start is called before the first frame update
    void Start()
    {
        this.RestartButton.SetActive(false);
        this.BackToMainButton.SetActive(false);
        this.DeadReason.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void setDeadReason(string reason)
    {
        UIControl.DeadReasonString = reason;
    }

    public void HeroDead()
    {
        this.RestartButton.SetActive(true);
        this.BackToMainButton.SetActive(true);
        this.DeadReason.gameObject.SetActive(true);

        this.DeadReason.text = UIControl.DeadReasonString.Length > 0 ? UIControl.DeadReasonString : "ow, you dead !!";
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
