using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResetPrescription : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // Reset Pad
    Toggle antibioticA;
    Toggle antibioticB;
    Toggle antibioticC;
    GameObject signature;
    GameObject signatureButton;
    Animator signatureAnimator;

    // Logic
    Game game;
    bool start = true;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!start) {
            string selected;

            // Reset Pad
            if (antibioticA == null)
                antibioticA = GameObject.Find("ABox").GetComponent<Toggle>();
            if (antibioticB == null)
                antibioticB = GameObject.Find("BBox").GetComponent<Toggle>();
            if (antibioticC == null)
                antibioticC = GameObject.Find("CBox").GetComponent<Toggle>();
            if (signature == null)
                signature = GameObject.Find("Signature");
            if (signatureButton == null)
                signatureButton = GameObject.Find("SignatureButton");

            signatureAnimator = signature.GetComponent<Animator>();

            if (antibioticA.isOn) selected = Constants.PREFS_ANTIBIOTIC_A;
            else if (antibioticB.isOn) selected = Constants.PREFS_ANTIBIOTIC_B;
            else selected = Constants.PREFS_ANTIBIOTIC_C;
            
            antibioticA.isOn = false;
            antibioticB.isOn = false;
            antibioticC.isOn = false;

            signatureAnimator.SetTrigger("Reset");
            signature.SetActive(false);
            signatureButton.SetActive(false);

            // Game Logic
            if (game == null)
                game = GameObject.Find("Canvas").GetComponents<MonoBehaviour>()[2] as Game;

            game.treat(selected);
            game.render(game.underChart);
            game.overChartAnimator.SetTrigger("SlideOff");
        } else {
            start = false;
        }
    }
}
