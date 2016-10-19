using UnityEngine;
using System.Collections;

public class ActivateSignature : MonoBehaviour {
    public GameObject Signature;
    public GameObject PrescriptionPad;
    public GameObject Text;

    Animator signatureAnimator;

    // Use this for initialization
    void Start () {
        signatureAnimator = Signature.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void OnClick()
    {
        Signature.SetActive(true);
        signatureAnimator.SetTrigger("PlayOnce");
        Text.SetActive(false);
    }
}
