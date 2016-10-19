using UnityEngine;
using System.Collections;

public class CloseTutorial : MonoBehaviour {

    public Animator tutorialAnimator;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        tutorialAnimator.SetTrigger("Hide");
    }
}
