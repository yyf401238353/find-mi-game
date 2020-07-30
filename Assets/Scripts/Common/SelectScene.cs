using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScene : MonoBehaviour
{
    public GameObject SelectXhhObj;
    public GameObject SelectYyfObj;

    public GameObject SelectXhhDisplay;
    public GameObject SelectYyfDisplay;

    public InputField NameInputObj;
    public Text UserNameText;

    public enum WhichScene
    {
        YYF,
        XHH
    }

    private WhichScene nowScene = WhichScene.XHH;

    // Start is called before the first frame update
    void Start()
    {
        if (ScoreControl.UserName.Length == 0)
        {
            this.UserNameText.gameObject.SetActive(false);
            this.setSelectPartActive(false);
        }
        else
        {
            UserNameText.text = "Welcome : " + ScoreControl.UserName;
            this.NameInputObj.gameObject.SetActive(false);
        }
    }

    private void setSelectPartActive(bool isActive)
    {
        this.SelectYyfDisplay.SetActive(isActive);
        this.SelectXhhDisplay.SetActive(isActive);
        this.SelectXhhObj.SetActive(isActive);
        this.SelectYyfObj.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {


        if (ScoreControl.UserName.Length > 0)
        {
            this.updateSelect();
            this.updateSelectObj();
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ScoreControl.UserName.Length == 0 && this.NameInputObj.text.Length != 0)
            {
                ScoreControl.UserName = this.NameInputObj.text;
                this.NameInputObj.gameObject.SetActive(false);
                this.UserNameText.gameObject.SetActive(true);
                this.setSelectPartActive(true);
                UserNameText.text = "Welcome : " + ScoreControl.UserName;

                // 上传分数示例代码
                ScoreUpload.UnityIns.UploadScore(1, ScoreControl.Type.XHH, delegate ()
                {
                    Debug.Log("upload over");

                });
            }
            else
            {
                if (this.nowScene == WhichScene.XHH)
                {
                    SceneManager.LoadScene("level_xhh");
                }
                else
                {
                    SceneManager.LoadScene("level_yyf");
                }
            }
        }
    }

    private void updateSelect()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (this.nowScene == WhichScene.XHH)
            {
                this.nowScene = WhichScene.YYF;
            }
            else
            {
                this.nowScene = WhichScene.XHH;
            }
        }
    }
    private void updateSelectObj()
    {
        if (this.nowScene == WhichScene.XHH)
        {
            this.SelectYyfObj.SetActive(false);
            this.SelectXhhObj.SetActive(true);
        }
        else
        {
            this.SelectYyfObj.SetActive(true);
            this.SelectXhhObj.SetActive(false);
        }
    }
}
