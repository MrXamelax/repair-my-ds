using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static Timer instance;

    public float time = 20f;
    Text timertext;

    private float speed;
    public float Speed { get => speed; set {
            speed = Mathf.Clamp01(value); 
        }
    }

    private void Start() {
        instance = this;
        timertext = GetComponent<Text>();
    }

    void Update() {
        if (time >= 0f) {
            time -= Time.deltaTime * Speed;
            timertext.text = "Time:\n" + time.ToString("N5");
        } else {
            GameManager.Score -= Time.deltaTime * Speed;
            timertext.text = "Score:\n" + (GameManager.Score * 1000f).ToString("N0");
        }
        
        if (Speed < 1f) {
            Speed += Time.deltaTime * 0.03f;
        }
        
    }
}
