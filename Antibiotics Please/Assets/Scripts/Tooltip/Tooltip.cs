using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour {

    public string message;
    public Animator controller;
    public GameObject tooltipText;


    void Start()
    {
        //controller = this.GetComponent<Animator>();
    }

    public void OnClick()
    {
        tooltipText.GetComponent<RecordText>().SetNextText(message);

        if (controller.GetBool("Show"))
        {
            controller.SetTrigger("FadeOut");
        }
        else
        {
            controller.SetBool("Show", true);
            controller.SetTrigger("FadeIn");
        }
    }
}
