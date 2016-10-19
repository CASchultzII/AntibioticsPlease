using UnityEngine;
using System.Collections;

public class CloseFeedback : MonoBehaviour {

    public Animator feedbackAnim;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void OnClick()
    {
        feedbackAnim.SetTrigger("Hide");
    }
}
