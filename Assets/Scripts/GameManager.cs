using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int repairsDone = 0;
    [SerializeField] AudioSource[] uniqueClips;

    private void Start() {
        instance = this;
        
        uniqueClips[0].Play();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            RepairFinish();
        }
        Debug.Log(4f - (uniqueClips[0].time % 4f));
    }

    public void RepairFinish() {
        repairsDone++;
        uniqueClips[repairsDone].PlayDelayed(4f - (uniqueClips[0].time % 4f));
    }


}
