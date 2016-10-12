using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour {

    public string message;
    public Animator controller;
    public Text text;
    public bool state = false;


    public void ShowMessage()
    {
        if (!state)
        {
            //text.text = message;
            controller.SetBool("Show", true);
            controller.SetTrigger("FadeIn");
            state = true;
        }
        else
        {
            controller.SetBool("Show", false);
            controller.SetTrigger("FadeOut");
            state = false;
        }
    }



}
