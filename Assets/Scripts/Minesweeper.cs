using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour, IInitializable {
    MinigameManager manager;

    private int width = 10;
    private int height = 10;

    private int bombs = 10;

    [SerializeField] GameObject spawnObject = null;
    [SerializeField] Gradient gradient = null;

    private bool[,] grid;

    private bool generated = false;

    public void Init(MinigameManager manager, int[] param) {
        this.manager = manager;

        if (param.Length >= 1) width = param[0];
        if (param.Length >= 2) height = param[1];
        if (param.Length >= 3) bombs = param[2];



        GetComponent<GridLayoutGroup>().constraintCount = width;
        GetComponent<GridLayoutGroup>().cellSize = Vector2.one * Screen.height * 0.75f / height;

        grid = new bool[width, height];
        generated = false;


        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                GameObject spawn = Instantiate(spawnObject, transform);
                spawn.name = $"B|{x}/{y}";
                spawn.GetComponentInChildren<Text>().text = "?";
                int sX = x, sY = y;
                spawn.GetComponent<Button>().onClick.AddListener(() => {
                    ButtonCallback(spawn, sX, sY);
                });

            }
        }
    }

    public void ButtonCallback(GameObject button, int x, int y) {
        if (!generated) {
            GenerateGrid(x, y);
        }

        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftShift)) {
            button.GetComponentInChildren<Text>().text = "F";
        } else {
            if (grid[x, y]) {
                button.GetComponentInChildren<Text>().text = "X";
                Timer.instance.Speed += 0.1f;
            } else {
                int surroundingBombs = GetSorroundingBombs(x, y);
                button.GetComponentInChildren<Text>().text = surroundingBombs == 0 ? "" : surroundingBombs.ToString();
                button.GetComponentInChildren<Text>().color = gradient.Evaluate(surroundingBombs / 8f);
                if (surroundingBombs == 0) {
                    RevealSorroundingFields(x, y);
                }

                CheckWin();
            }
        }
    }

    private void GenerateGrid(int safeX, int safeY) {
        generated = true;

        int remainingBombs = bombs;
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (x == safeX && y == safeY) {
                    continue;
                }

                if (Random.value < (float)remainingBombs / ((height - y - 1) * width + width - x)) {
                    grid[x, y] = true;
                    remainingBombs--;
                }
            }
        }
    }

    private int GetSorroundingBombs(int x, int y) {
        int r = 0;
        for (int i = x - 1; i <= x + 1; i++) {
            for (int j = y - 1; j <= y + 1; j++) {
                if (i == x && j == y) {
                    continue;
                }
                if (IsBombInThisFieldIfItExists(i, j)) {
                    r++;
                }
            }
        }
        return r;
    }

    private bool IsBombInThisFieldIfItExists(int x, int y) {
        if (x < 0 || x >= width || y < 0 || y >= height || !grid[x, y]) {
            return false;
        }
        return true;
    }

    private void RevealSorroundingFields(int x, int y) {
        for (int i = x - 1; i <= x + 1; i++) {
            for (int j = y - 1; j <= y + 1; j++) {
                if (i == x && j == y) {
                    continue;
                } else {
                    GameObject nB = GameObject.Find($"B|{i}/{j}");
                    if (nB != null) {
                        if (nB.GetComponentInChildren<Text>() && nB.GetComponentInChildren<Text>().text.Equals("?")) {
                            ButtonCallback(nB, i, j);
                        }
                    }
                }
                
            }
        }
    }

    private void CheckWin() {
        int counter = 0;
        foreach (Transform child in transform) {
            if (child.Equals(transform)) {
                continue;
            }

            if (child.GetComponentInChildren<Text>().text.Equals("?") || child.GetComponentInChildren<Text>().text.Equals("F") || child.GetComponentInChildren<Text>().text.Equals("X")) {
                counter++;
            }
        }

        if (counter <= bombs) {
            manager.FinishedGame();
        }
    }
}
