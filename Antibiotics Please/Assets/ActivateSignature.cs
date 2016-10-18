using UnityEngine;
using System.Collections;

public class ActivateSignature : MonoBehaviour {
    public GameObject Signature;
    public GameObject PrescriptionPad;
    public GameObject Text;

    Animator signatureAnimator;
    Animator padAnimator;

    int onNoAnimation = Animator.StringToHash("Base Layer.NoAnimation");


    // Use this for initialization
    void Start () {
        signatureAnimator = Signature.GetComponent<Animator>();
        padAnimator = PrescriptionPad.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Signature.activeInHierarchy == true)
        {
            AnimatorStateInfo stateInfo = signatureAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.fullPathHash == onNoAnimation)
            {
                padAnimator.SetTrigger("Hide");
            }
        }
	}

    public void OnClick()
    {
        Signature.SetActive(true);
        signatureAnimator.SetTrigger("PlayOnce");
        //signatureAnimator.SetBool("Play", true);
        Text.SetActive(false);
    }
}
