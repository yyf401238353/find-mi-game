using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Helper
{
    [MenuItem("Assets/Helper/PlayerPrefs_DeleteAll")]
    static void PlayerPrefsDeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("DeleteAll finish!");
    }
}
