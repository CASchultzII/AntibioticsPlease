using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrescriptionControl : MonoBehaviour {

    public GameObject SignatureButton;

    Animator animator;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
    }

    public void Show()
    {
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

    // Update is called once per frame
    void Update () {
	}
}
