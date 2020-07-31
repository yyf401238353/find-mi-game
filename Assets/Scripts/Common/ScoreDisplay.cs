using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreDisplay : MonoBehaviour
{
    [Header("xhh 关卡前五名名字对象")]
    public Text[] XhhFirstFiveNames;
    [Header("xhh 关卡前五名名字分数")]
    public Text[] XhhFirstFiveScore;

    [Header("Yyf 关卡前五名名字对象")]
    public Text[] YyfFirstFiveNames;
    [Header("Yyf 关卡前五名名字分数")]
    public Text[] YyfFirstFiveScore;

    public Text XhhYourLevel;
    public Text XhhYourName;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.getAllScore());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator getAllScore()
    {
        UnityWebRequest uwr = UnityWebRequest.Get(ScoreControl.GetAllScoreUrl);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            ScoreControl.ScoreInfo[] infos = JsonUtility.FromJson<ScoreControl.ApiReturnInfo<ScoreControl.ScoreInfo[]>>(uwr.downloadHandler.text).data;
            this.updateScore(infos);
        }
    }

    private void updateScore(ScoreControl.ScoreInfo[] infos)
    {
        List<ScoreControl.ScoreInfo> xhhInfos = this.getSortedScoreInfo(ScoreControl.Type.XHH, infos);

        for (int index = 0; index < xhhInfos.Count; index++)
        {
            if (index < 5)
            {
                this.XhhFirstFiveNames[index].text = xhhInfos[index].user_name;
                this.XhhFirstFiveScore[index].text = xhhInfos[index].score.ToString();
            }

        }

        List<ScoreControl.ScoreInfo> yyfInfos = this.getSortedScoreInfo(ScoreControl.Type.YYF, infos);

        for (int index = 0; index < yyfInfos.Count; index++)
        {
            if (index < 5)
            {
                this.YyfFirstFiveNames[index].text = yyfInfos[index].user_name;
                this.YyfFirstFiveScore[index].text = yyfInfos[index].score.ToString();
            }

        }
    }

    private List<ScoreControl.ScoreInfo> getSortedScoreInfo(ScoreControl.Type type, ScoreControl.ScoreInfo[] infos)
    {
        List<ScoreControl.ScoreInfo> result = new List<ScoreControl.ScoreInfo>();

        for (int index = 0; index < infos.Length; index++)
        {
            if (infos[index].type == type)
            {
                result.Add(infos[index]);
            }
        }

        result.Sort((x, y) => -(x.score - y.score));

        return result;
    }
}
