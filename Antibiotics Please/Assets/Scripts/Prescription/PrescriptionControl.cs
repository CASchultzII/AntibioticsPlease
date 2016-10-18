using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrescriptionControl : MonoBehaviour {

    public GameObject SignatureButton;

    int onScreenHash = Animator.StringToHash("Base Layer.OnScreen");

    Animator animator;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        animator.SetTrigger("Show");

    }

    public void Show()
    {


    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

    // Update is called once per frame
    void Update () {

        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //if(stateInfo.fullPathHash == onScreenHash && SignatureButton.activeInHierarchy == false)
        //{
        //    SignatureButton.SetActive(true);
        //}
	}
}
