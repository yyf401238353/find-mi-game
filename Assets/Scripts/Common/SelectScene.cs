using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    public GameObject SelectXhhObj;
    public GameObject SelectYyfObj;

    public enum WhichScene
    {
        YYF,
        XHH
    }

    private WhichScene nowScene = WhichScene.XHH;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.updateSelect();
        this.updateSelectObj();

        if (Input.GetKeyDown(KeyCode.Return))
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
