using UnityEngine;
using System.Collections;

public class ShowTutorial : MonoBehaviour {

    public Animator tutorialAnimator;
    public GameObject startButton, guideButton;
     

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        tutorialAnimator.SetTrigger("Show");
        startButton.SetActive(false);
        guideButton.SetActive(false);
    }
}
