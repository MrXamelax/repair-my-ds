using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField] Animator robotAnim = null;

    public void StartGame() {
        GetComponent<Animator>().SetTrigger("start");
    }

    public void SetRobotMoving() {
        robotAnim.SetBool("isWalking", true);
    }

    public void LoadGame() {
        SceneManager.LoadScene("MainGame");
    }
}
