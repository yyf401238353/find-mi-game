using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public static class ScoreControl
{
    public enum Type
    {
        XHH = 1,
        YYF = 2
    }

    [System.Serializable]
    public struct ApiReturnInfo<T>
    {
        public int code;
        public T data;
    }

    [System.Serializable]
    public struct ScoreInfo
    {
        public int id;
        public string user_name;
        public Type type;
        public int score;
    }


    private readonly static string APP_ID = "FindMiGame";

    private readonly static string HOST = "http://10.224.201.40:3100";

    private readonly static string UserNameKey = "USER_NAME";

    public static string GetAllScoreUrl
    {
        get
        {
            return string.Format("{0}/api/score/{1}/all", HOST, APP_ID);
        }
    }

    public static string UserName
    {
        get
        {
            return PlayerPrefs.GetString(ScoreControl.UserNameKey);
        }

        set
        {
            PlayerPrefs.SetString(ScoreControl.UserNameKey, value);
        }
    }
}
