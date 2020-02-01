using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Minigame {
    Ordernumbers, ShufflePuzzle, Minesweeper
}

[RequireComponent(typeof(Collider))]
public class MinigameEnterPoint : MonoBehaviour {
    public Minigame minigame;

    public int[] paramsForGames;

    public float timeScaleReward = 0.1f;

    private void OnTriggerEnter(Collider other) {
        PlayerActions pa = other.GetComponent<PlayerActions>();
        if (pa) {
            pa.EnterPoint = this;
        }
    }

    private void OnTriggerExit(Collider other) {
        PlayerActions pa = other.GetComponent<PlayerActions>();
        if (pa && pa.EnterPoint == this) {
            pa.EnterPoint = null;
        }
    }

    public void EnterMinigame() {
        SceneManager.LoadScene(minigame.ToString(), LoadSceneMode.Additive);
    }

    public void Solved() {
        GetComponent<Renderer>().material.color = Color.green;
        GetComponent<Collider>().enabled = false;
        Timer.instance.speed *= timeScaleReward;
    }
}
