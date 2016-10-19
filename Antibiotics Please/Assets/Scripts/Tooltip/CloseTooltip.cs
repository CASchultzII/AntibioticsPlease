using UnityEngine;
using System.Collections;

public class CloseTooltip : MonoBehaviour {
    public Animator tooltipAnimator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        tooltipAnimator.SetBool("Show", false);
        tooltipAnimator.SetTrigger("FadeOut");
    }
}
