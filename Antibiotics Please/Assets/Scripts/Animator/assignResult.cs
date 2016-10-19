using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class assignResult : StateMachineBehaviour {

    Game game;
    GameObject successText, deathText;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    if(game == null)
            game = GameObject.Find("Canvas").GetComponents<MonoBehaviour>()[2] as Game;
        if (successText == null)
            successText = GameObject.Find("ResultTable").transform.GetChild(1).gameObject;
        if (deathText == null)
            deathText = GameObject.Find("ResultTable").transform.GetChild(2).gameObject;

        successText.GetComponent<Text>().text = "Patients cured: " + game.successCount;
        deathText.GetComponent<Text>().text = "Patient deaths: " + game.deathCount;
    }

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
}
