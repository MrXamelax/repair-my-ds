using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTiles : MonoBehaviour {

    [SerializeField] float movementSpeed = 1;
    [SerializeField] Camera cam = null;
    private bool moving = false;
    private Vector3 destination, startPosition;
    private Vector3 move = Vector3.zero;
    private float camHeight, camDist;

    [SerializeField] LayerMask wallLayer = 0;

    [SerializeField] Animator animator = null;

    void Start() {
        startPosition = transform.position;
        camHeight = cam.transform.position.y;
        camDist = Mathf.Abs(Mathf.Abs(cam.transform.position.z) - Mathf.Abs(transform.position.z));
    }

    void Update() {

        if (Input.GetKey(KeyCode.Q)) {
            moving = false;
            destination = startPosition;
            transform.position = startPosition;
            animator.SetBool("isWalking", false);
            StopAllCoroutines();
        }

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

        move = transform.position - cam.transform.position;
        move.y = 0;
        move.z -= camDist;
        cam.transform.position += move * movementSpeed * Time.deltaTime;
        
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
}
