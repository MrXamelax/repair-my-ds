using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTiles : MonoBehaviour {

    [SerializeField] float movementSpeed = 1;
    [SerializeField] Camera cam;
    private bool wFree = true, sFree = true, dFree = true, aFree = true;
    private bool moving = false;
    private Vector3 destination;

    [SerializeField] LayerMask wallLayer;

    [SerializeField] Animator animator;

    void Start() {

    }

    void Update() {

        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.S)) { } else {
                if (!moving) {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (!Physics.Raycast(transform.position, Vector3.forward, 1f, wallLayer)) {
                        destination = transform.position + Vector3.forward;
                        destination = destination.Round();
                        StartCoroutine(MoveForeward());
                    }
                }
            }
        } else {
            if (Input.GetKey(KeyCode.S)) {
                if (Input.GetKey(KeyCode.W)) { } else {
                    if (!moving) {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        if (!Physics.Raycast(transform.position, Vector3.back, 1f, wallLayer)) {
                            destination = transform.position + Vector3.back;
                            destination = destination.Round();
                            StartCoroutine(MoveBack());
                        }
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.D)) {
            if (Input.GetKey(KeyCode.A)) { } else {
                if (!moving) {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    if (!Physics.Raycast(transform.position, Vector3.right, 1f, wallLayer)) {
                        destination = transform.position + Vector3.right;
                        destination = destination.Round();
                        StartCoroutine(MoveRight());
                    }
                }
            }
        } else {
            if (Input.GetKey(KeyCode.A)) {
                if (Input.GetKey(KeyCode.D)) { } else {
                    if (!moving) {
                        transform.rotation = Quaternion.Euler(0, 270, 0);
                        if (!Physics.Raycast(transform.position, Vector3.left, 1f, wallLayer)) {
                            destination = transform.position + Vector3.left;
                            destination = destination.Round();
                            StartCoroutine(MoveLeft());
                        }
                    }
                }
            }
        }

    }

    IEnumerator MoveForeward() {
        animator.SetBool("isWalking", true);
        moving = true;
        while ((transform.position - destination).sqrMagnitude > 0.001f * movementSpeed) {
            transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
            yield return null;
        }
        moving = false;
        animator.SetBool("isWalking", false);
    }

    IEnumerator MoveBack() {
        animator.SetBool("isWalking", true);
        moving = true;
        while ((transform.position - destination).sqrMagnitude > 0.001f * movementSpeed) {
            transform.position += Vector3.back * movementSpeed * Time.deltaTime;
            yield return null;
        }
        moving = false;
        animator.SetBool("isWalking", false);
    }

    IEnumerator MoveRight() {
        animator.SetBool("isWalking", true);
        moving = true;
        while ((transform.position - destination).sqrMagnitude > 0.001f * movementSpeed) {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            yield return null;
        }
        moving = false;
        animator.SetBool("isWalking", false);
    }

    IEnumerator MoveLeft() {
        animator.SetBool("isWalking", true);
        moving = true;
        while ((transform.position - destination).sqrMagnitude > 0.001f * movementSpeed) {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            yield return null;
        }
        moving = false;
        animator.SetBool("isWalking", false);
    }

    private void OnDisable() {
        moving = false;
        animator.SetBool("isWalking", false);
    }

    /*
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger enter!");
        wFree = true; sFree = true; dFree = true; aFree = true;
        if (Physics.Raycast(transform.position, Vector3.forward, 1f, wallLayer)) {
            wFree = false;
            Debug.Log("W blocked!");
        }
        if (Physics.Raycast(transform.position, Vector3.forward + Vector3.left, 1f, wallLayer)) {
            wFree = false;
            aFree = false;
            Debug.Log("W and A blocked!");
        }
        if (Physics.Raycast(transform.position, Vector3.forward + Vector3.right, 1f, wallLayer)) {
            wFree = false;
            dFree = false;
            Debug.Log("W and D blocked!");
        }
        if (Physics.Raycast(transform.position, Vector3.back, 1f, wallLayer)) {
            sFree = false;
            Debug.Log("S blocked!");
        }
        if (Physics.Raycast(transform.position, Vector3.back + Vector3.left, 1f, wallLayer)) {
            sFree = false;
            aFree = false;
            Debug.Log("S and A blocked!");
        }
        if (Physics.Raycast(transform.position, Vector3.back + Vector3.right, 1f, wallLayer)) {
            sFree = false;
            dFree = false;
            Debug.Log("S and D blocked!");
        }
        if (Physics.Raycast(transform.position, Vector3.right, 1f, wallLayer)) {
            dFree = false;
            Debug.Log("D blocked!");
        }
        if (Physics.Raycast(transform.position, Vector3.left, 1f, wallLayer)) {
            aFree = false;
            Debug.Log("A blocked!");
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Trigger leave!");
        wFree = true; sFree = true; dFree = true; aFree = true;
        if (Physics.Raycast(transform.position, Vector3.forward, 1f, wallLayer)) {
            Debug.Log("W blocked!");
            wFree = false;
        }
        if (Physics.Raycast(transform.position, Vector3.back, 1f, wallLayer)) {
            Debug.Log("S blocked!");
            sFree = false;
        }
        if (Physics.Raycast(transform.position, Vector3.right, 1f, wallLayer)) {
            Debug.Log("D blocked!");
            dFree = false;
        }
        if (Physics.Raycast(transform.position, Vector3.left, 1f, wallLayer)) {
            Debug.Log("A blocked!");
            aFree = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("TRIGGERED");
        if (Physics.Raycast(transform.position, Vector3.forward, 1f, wallLayer)) {
            if (!Physics.Raycast(transform.position, Vector3.right, 1f, wallLayer)) dFree = true;
            if (!Physics.Raycast(transform.position, Vector3.left, 1f, wallLayer)) aFree = true;
        }
        if (Physics.Raycast(transform.position, Vector3.back, 1f, wallLayer)) {
            if (!Physics.Raycast(transform.position, Vector3.right, 1f, wallLayer)) dFree = true;
            if (!Physics.Raycast(transform.position, Vector3.left, 1f, wallLayer)) aFree = true;
        }
        if (Physics.Raycast(transform.position, Vector3.right, 1f, wallLayer)) {
            if (!Physics.Raycast(transform.position, Vector3.forward, 1f, wallLayer)) wFree = true;
            if (!Physics.Raycast(transform.position, Vector3.back, 1f, wallLayer)) sFree = true;
        }
        if (Physics.Raycast(transform.position, Vector3.left, 1f, wallLayer)) {
            if (!Physics.Raycast(transform.position, Vector3.forward, 1f, wallLayer)) wFree = true;
            if (!Physics.Raycast(transform.position, Vector3.back, 1f, wallLayer)) sFree = true;
        }
    }
    */

}
