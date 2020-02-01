using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField] float time = 20f;
    [SerializeField] float speed = 0.5f;

    void Update() {
        time -= Time.deltaTime * speed;
    }
}
