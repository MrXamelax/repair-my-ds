using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int repairsDone = 0;
    [SerializeField] AudioSource[] uniqueClips = null;

    public AudioSource failSound = null;
    [SerializeField] AudioSource success = null;

    public static float Score = 1000f;

    private void Start() {
        instance = this;
        
        uniqueClips[0].Play();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            RepairFinish();
        }
    }

    public void RepairFinish() {
        success.Play();
        repairsDone++;
        if (repairsDone >= uniqueClips.Length) {

            Timer.instance.enabled = false;

            //foreach (var item in uniqueClips) {
            //    item.Stop();
            //    item.Play();
            //}
            Invoke("BackToMenu", 7f);

            return;
        }
        uniqueClips[repairsDone].PlayDelayed(8f - (uniqueClips[0].time % 8f));
        //Debug.Log($"enabled {uniqueClips[repairsDone].clip.name} in {8f - (uniqueClips[0].time % 8f)}s");

        if (repairsDone % 2 == 1) {
            RepairFinish();
        }
    }

    private void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
