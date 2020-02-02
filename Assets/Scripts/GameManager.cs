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
    }

    public void RepairFinish() {
        repairsDone++;
        if (repairsDone >= uniqueClips.Length) {
            foreach (var item in uniqueClips) {
                item.Stop();
                item.Play();
            }
            return;
        }
        uniqueClips[repairsDone].PlayDelayed(8f - (uniqueClips[0].time % 8f));
        Debug.Log($"enabled {uniqueClips[repairsDone].clip.name} in {8f - (uniqueClips[0].time % 8f)}s");
    }


}
