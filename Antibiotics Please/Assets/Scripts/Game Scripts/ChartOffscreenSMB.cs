using UnityEngine;

public class ChartOffscreenSMB : StateMachineBehaviour {

    private Game game;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (game == null)
            game = GameObject.Find("Canvas").GetComponents<MonoBehaviour>()[2] as Game;

        game.reset(game.overChart);
        game.render(game.overChart);
        game.overChartAnimator.SetTrigger("Reset");
    }
}
