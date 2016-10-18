using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour {

    public string message;
    public Animator controller;
    public Text text;
    public bool state = false;

    void Start()
    {
        //controller = this.GetComponent<Animator>();
    } 

    public void OnClick()
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
