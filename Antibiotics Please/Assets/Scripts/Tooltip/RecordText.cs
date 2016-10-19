using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordText : MonoBehaviour {
    string currentText;
    string nextText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetNextText(string text)
    {
        nextText = text;
    }

    public void UseNextText()
    {
        currentText = nextText;
        this.GetComponent<Text>().text = currentText;
    }
}
