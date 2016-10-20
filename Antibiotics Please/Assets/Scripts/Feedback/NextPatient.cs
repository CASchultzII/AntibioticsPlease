using UnityEngine;
using System.Collections;

public class NextPatient : StateMachineBehaviour {

    Game game;

    int onOnScreenAnimation = Animator.StringToHash("Base Layer.OnScreen");
    int onHideAnimation = Animator.StringToHash("Base Layer.Hide");
    int onSlideOutAnimation = Animator.StringToHash("Base Layer.SlideOut");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (game == null)
            game = GameObject.Find("Canvas").GetComponents<MonoBehaviour>()[2] as Game;
        // For feedback animator
        if (stateInfo.fullPathHash == onOnScreenAnimation)
        {
            game.UpdateDeathCount();
            game.UpdateSuccessCount();
            game.playResultSound();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (game == null)
            game = GameObject.Find("Canvas").GetComponents<MonoBehaviour>()[2] as Game;

        // For feedback animator
        if (stateInfo.fullPathHash == onHideAnimation)
        {
            game.clipBoardAnimator.SetTrigger("SlideOut");
        }
        // for clipboard animator
        else if (stateInfo.fullPathHash == onSlideOutAnimation)
        {

            if (PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A) + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B) + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C) >= 1.5f)
            {
                game.GameOver();
            }
            else
            {
                game.generatePatient();
                animator.SetTrigger("SlideIn");
            }
        }
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
