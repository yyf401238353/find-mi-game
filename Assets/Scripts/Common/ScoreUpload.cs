using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreUpload : MonoBehaviour
{
    public static ScoreUpload UnityIns;

    // Start is called before the first frame update
    void Start()
    {
        ScoreUpload.UnityIns = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UploadScore(int score, ScoreControl.Type type, System.Action uploadOver)
    {
        StartCoroutine(this.uploadScoreDispose(score, type, ScoreControl.UserName, uploadOver));
    }

    private IEnumerator uploadScoreDispose(int score, ScoreControl.Type type, string userName, System.Action uploadOver)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", score);
        form.AddField("type", (int)type);
        form.AddField("user_name", userName);

        UnityWebRequest uwr = UnityWebRequest.Post(ScoreControl.GetUploadScoreUrl, form);
        yield return uwr.SendWebRequest();
        // 不论结果，直接调用结束事件
        uploadOver.Invoke();
    }
}
