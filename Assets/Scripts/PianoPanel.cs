using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PianoPanel : MonoSingleton<PianoPanel>
{
    private Text _content;
	// Use this for initialization
	void Start () {
        _content = transform.Find("Content").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddVoiceCode(string code)
    {
        if (_content)
            _content.text = _content.text + " " + code;
    }
}
