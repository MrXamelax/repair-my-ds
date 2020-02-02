using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static Timer instance;

    public float time = 20f;
    public float speed = 0.5f;

    Text timertext;

    private void Start() {
        instance = this;
        timertext = GetComponent<Text>();
    }

    void Update() {
        time -= Time.deltaTime * speed;
        timertext.text = time.ToString("N5");
        if (speed < 1f) {
            speed += Time.deltaTime * 0.02f;
        }
        
    }
}
