using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PianoKeyBlackComponent : MonoBehaviour
{
    static string[] blackVocies = { "a0m",
                                    "c1m", "d1m", "f1m", "g1m", "a1m",
                                    "c2m", "d2m", "f2m", "g2m", "a2m",
                                    "c3m", "d3m", "f3m", "g3m", "a3m",
                                    "c4m", "d4m", "f4m", "g4m", "a4m",
                                    "c5m", "d5m", "f5m", "g5m", "a5m",
                                    "c6m", "d6m", "f6m", "g6m", "a6m",
                                    "c7m", "d7m", "f7m", "g7m", "a7m"};
    private Image dowmBg;
    private AudioSource audioSoucre;
    private AudioClip clip;
    private float lastPlayTime = 0;
    private int _index = 0;
    // Use this for initialization
    void Start()
    {
        dowmBg = transform.Find("Down").GetComponent<Image>();
        audioSoucre = GetComponent<AudioSource>();
        int index1 = name.IndexOf("(");
        int index2 = name.IndexOf(")");
        _index = 0;
        if (index2 - index1 > 1)
        {
            string number = name.Substring(index1 + 1, index2 - index1 - 1);
            int.TryParse(number, out _index);
        }
        clip = Resources.Load("PianoVoice/" + blackVocies[_index]) as AudioClip;
        EventTriggerListener.Get(gameObject).onDown = OnButtonDown;
        EventTriggerListener.Get(gameObject).onUp = OnButtonUp;
        EventTriggerListener.Get(gameObject).onEnter = OnButtonEnter;
        EventTriggerListener.Get(gameObject).onExit = OnButtonExit;
    }

    private void OnButtonEnter(GameObject go)
    {
        //在这里监听按钮的点击事件
        if (go == gameObject)
        {
            //Debug.Log("EnterDoSomeThings/" + name);
            dowmBg.gameObject.SetActive(true);
            PlayVoice();
        }
    }

    private void OnButtonExit(GameObject go)
    {
        //在这里监听按钮的点击事件
        if (go == gameObject)
        {
            //Debug.Log("ExitDoSomeThings/" + name);
            dowmBg.gameObject.SetActive(false);
        }
    }

    private void OnButtonDown(GameObject go)
    {
        //在这里监听按钮的点击事件
        if (go == gameObject)
        {
            //Debug.Log("DoSomeThings/" + name);
            dowmBg.gameObject.SetActive(true);
            PlayVoice();
        }
    }

    private void OnButtonUp(GameObject go)
    {
        //在这里监听按钮的点击事件
        if (go == gameObject)
        {
            //Debug.Log("...DoSomeThings/" + name);
            dowmBg.gameObject.SetActive(false);
        }
    }

    private void PlayVoice()
    {
        if (Time.realtimeSinceStartup - lastPlayTime > 0.1f)
        {
            lastPlayTime = Time.realtimeSinceStartup;
            audioSoucre.PlayOneShot(clip);
            //PianoPanel.instance.AddVoiceCode(blackVocies[_index] + "(" + nameVocies[_index] + ")");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
