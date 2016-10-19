using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playChartCheck : MonoBehaviour {
    public Game game;

    Toggle toggle;

	// Use this for initialization
	void Start () {
        toggle = GetComponent<Toggle>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCheck()
    {
        if(toggle.isOn)
        {
            game.playCheckMarkSound();
        }
    }
}
