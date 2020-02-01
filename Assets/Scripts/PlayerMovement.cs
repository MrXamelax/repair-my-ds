using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] Transform playerTf;
    [SerializeField] float movementSpeed = 1;
    [SerializeField] [Tooltip("Turn on for the real icy shit")] bool iced = false;
    private Vector3 move = Vector3.zero;
    private bool wFree = true, sFree = true, dFree = true, aFree = true;

    void Start() {

    }

    void Update() {

        Debug.DrawRay(transform.position, Vector3.forward + Vector3.right);
        Debug.DrawRay(transform.position, Vector3.forward + Vector3.left);
        Debug.DrawRay(transform.position, Vector3.back + Vector3.right);
        Debug.DrawRay(transform.position, Vector3.back + Vector3.left);
        Debug.DrawRay(transform.position, Vector3.forward);
        Debug.DrawRay(transform.position, Vector3.back);
        Debug.DrawRay(transform.position, Vector3.right);
        Debug.DrawRay(transform.position, Vector3.left);

        if (!iced) move = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && wFree) {
            if (Input.GetKey(KeyCode.S)) { } else {
                move.z += 1f;
                sFree = true;
            }
        } else {
            if (Input.GetKey(KeyCode.S) && sFree) {
                if (Input.GetKey(KeyCode.W)) { } else {
                    move.z += -1f;
                    wFree = true;
                }
            }
        }

        if (Input.GetKey(KeyCode.D) && dFree) {
            if (Input.GetKey(KeyCode.A)) { } else {
                move.x += 1f;
                aFree = true;
            }
        } else {
            if (Input.GetKey(KeyCode.A) && aFree) {
                if (Input.GetKey(KeyCode.D)) { } else {
                    move.x += -1f;
                    dFree = true;
                }
            }
        }

        playerTf.position += move.normalized * movementSpeed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger enter!");
        wFree = true; sFree = true; dFree = true; aFree = true;
        if (Physics.Raycast(playerTf.position, Vector3.forward, 1f)) {
            wFree = false;
            Debug.Log("W blocked!");
        }
        if (Physics.Raycast(playerTf.position, Vector3.forward + Vector3.left, 1f)) {
            wFree = false;
            aFree = false;
            Debug.Log("W and A blocked!");
        }
        if (Physics.Raycast(playerTf.position, Vector3.forward + Vector3.right, 1f)) {
            wFree = false;
            dFree = false;
            Debug.Log("W and D blocked!");
        }
        if (Physics.Raycast(playerTf.position, Vector3.back, 1f)) {
            sFree = false;
            Debug.Log("S blocked!");
        }
        if (Physics.Raycast(playerTf.position, Vector3.back + Vector3.left, 1f)) {
            sFree = false;
            aFree = false;
            Debug.Log("S and A blocked!");
        }
        if (Physics.Raycast(playerTf.position, Vector3.back + Vector3.right, 1f)) {
            sFree = false;
            dFree = false;
            Debug.Log("S and D blocked!");
        }
        if (Physics.Raycast(playerTf.position, Vector3.right, 1f)) {
            dFree = false;
            Debug.Log("D blocked!");
        }
        if (Physics.Raycast(playerTf.position, Vector3.left, 1f)) {
            aFree = false;
            Debug.Log("A blocked!");
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Trigger leave!");
        wFree = true; sFree = true; dFree = true; aFree = true;
        if (Physics.Raycast(playerTf.position, Vector3.forward, 1f)) {
            Debug.Log("W blocked!");
            wFree = false;
        } else if (Physics.Raycast(playerTf.position, Vector3.back, 1f)) {
            Debug.Log("S blocked!");
            sFree = false;
        } else if (Physics.Raycast(playerTf.position, Vector3.right, 1f)) {
            Debug.Log("D blocked!");
            dFree = false;
        } else if (Physics.Raycast(playerTf.position, Vector3.left, 1f)) {
            Debug.Log("A blocked!");
            aFree = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("TRIGGERED");
        if (Physics.Raycast(playerTf.position, Vector3.forward, 1f)) {
            if (!Physics.Raycast(playerTf.position, Vector3.right, 1f)) dFree = true;
            if (!Physics.Raycast(playerTf.position, Vector3.left, 1f)) aFree = true;
        }
        if (Physics.Raycast(playerTf.position, Vector3.back, 1f)) {
            if (!Physics.Raycast(playerTf.position, Vector3.right, 1f)) dFree = true;
            if (!Physics.Raycast(playerTf.position, Vector3.left, 1f)) aFree = true;
        }
        if (Physics.Raycast(playerTf.position, Vector3.right, 1f)) {
            if (!Physics.Raycast(playerTf.position, Vector3.forward, 1f)) wFree = true;
            if (!Physics.Raycast(playerTf.position, Vector3.back, 1f)) sFree = true;
        }
        if (Physics.Raycast(playerTf.position, Vector3.left, 1f)) {
            if (!Physics.Raycast(playerTf.position, Vector3.forward, 1f)) wFree = true;
            if (!Physics.Raycast(playerTf.position, Vector3.back, 1f)) sFree = true;
        }
    }

}
