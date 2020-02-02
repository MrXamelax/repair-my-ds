using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour {
    public GameObject minigameScriptObject;

    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);

        minigameScriptObject.GetComponent<IInitializable>().Init(this, player.GetComponent<PlayerActions>().EnterPoint.paramsForGames);
    }

    public void FinishedGame() {
        player.SetActive(true);
        player.GetComponent<PlayerActions>().EnterPoint.Solved();
        player.GetComponent<PlayerActions>().EnterPoint = null;

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.RightControl)) {
            FinishedGame();
        }
    }
}
