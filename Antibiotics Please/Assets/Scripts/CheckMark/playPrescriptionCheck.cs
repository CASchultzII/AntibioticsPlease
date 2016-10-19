using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playPrescriptionCheck : MonoBehaviour {
    public Game game;
    public GameObject signatureButton;
    public GameObject buttonText;

    Toggle toggle;

    // Use this for initialization
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCheck()
    {
        if (toggle.isOn)
        {
            game.playCheckMarkSound();
            signatureButton.SetActive(true);
            buttonText.SetActive(true);
        }
        else
        {
            signatureButton.SetActive(false);
            buttonText.SetActive(false);
        }
    }
}
