using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public void ChangeScene(string scene) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
