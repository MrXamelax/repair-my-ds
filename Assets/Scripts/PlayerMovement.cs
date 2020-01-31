using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] Transform playerTf;
    [SerializeField] float movementSpeed = 1;
    private Vector3 move = Vector3.zero;
    [SerializeField] [Tooltip("Turn on for the real icy shit")] bool iced = false;

    void Start() {
        
    }

    void Update() {

        if (!iced) move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.S)) { } else move.y += 1f;
        } else {
            if (Input.GetKey(KeyCode.S)) {
                if (Input.GetKey(KeyCode.W)) { } else move.y += -1f;
            }
        }

        if (Input.GetKey(KeyCode.D)) {
            if (Input.GetKey(KeyCode.A)) { } else move.x += 1f;
        } else {
            if (Input.GetKey(KeyCode.A)) {
                if (Input.GetKey(KeyCode.D)) { } else move.x += -1f;
            }
        }

        playerTf.position += move.normalized * Time.deltaTime;

    }

}
