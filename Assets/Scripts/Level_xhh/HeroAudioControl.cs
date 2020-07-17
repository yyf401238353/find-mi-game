using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioInfo
{
    [Header("音频对应状态")]
    public Hero.Status type;
    [Header("音频")]
    public AudioClip clip;
}
[RequireComponent(typeof(AudioSource))]
public class HeroAudioControl : MonoBehaviour
{
    public List<AudioInfo> AudioInfos;

    private AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.myAudioSource = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setStatus(Hero.Status status)
    {

        int infoIndex = this.getMatchInfo(status);

        if (infoIndex >= 0)
        {
            this.myAudioSource.Stop();
            this.myAudioSource.clip = this.AudioInfos[infoIndex].clip;
            this.myAudioSource.Play();
        }
    }


    private int getMatchInfo(Hero.Status type)
    {

        try
        {

            return this.AudioInfos.FindIndex((item) =>
            {
                return item.type == type;
            });
        }
        catch
        {
            return -1;
        }


    }

}
