using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleValue : MonoBehaviour {

    public GameObject SignatureButton;
    Toggle toggle;

	// Use this for initialization
	void Start () {
        toggle = GetComponent<Toggle>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ValueChange()
    {
        if(toggle.isOn == true)
        {
            SignatureButton.SetActive(true);
        }
    }
}
