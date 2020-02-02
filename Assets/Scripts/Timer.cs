using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static Timer instance;

    public float time = 20f;
    public float speed = 0.2f;

    Text timertext;

    private void Start() {
        instance = this;
        timertext = GetComponent<Text>();
    }

    void Update() {
        if (time >= 0f) {
            time -= Time.deltaTime * speed;
            timertext.text = "Time:\n" + time.ToString("N5");
        } else {
            GameManager.Score -= Time.deltaTime * speed;
            timertext.text = "Score:\n" + (GameManager.Score * 1000f).ToString("N0");
        }
        
        if (speed < 1f) {
            speed += Time.deltaTime * 0.03f;
        }
        
    }
}
