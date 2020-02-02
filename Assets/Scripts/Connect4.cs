using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connect4 : MonoBehaviour, IInitializable {

    MinigameManager manager;

    Dictionary<int, List<Image>> columnLookups;

    byte[,] grid;

    bool isPlayerTurn = true;

    public void Init(MinigameManager manager, int[] param) {
        this.manager = manager;

        grid = new byte[12, 5];

        columnLookups = new Dictionary<int, List<Image>>();

        Button[] buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++) {
            int unityBugsFTW = i;
            buttons[i].onClick.AddListener(() => {
                ButtonCallback(unityBugsFTW);
            });
            columnLookups.Add(i, new List<Image>());
        }

        Image[] images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++) {
            columnLookups[i % columnLookups.Keys.Count].Insert(0, images[i]);
        }
    }

    public void ButtonCallback(int ind) {
        if (grid[ind, 4] == 0) {
            if (isPlayerTurn) {
                grid[ind, 4] = 1;
            } else {
                grid[ind, 4] = 2;
            }
            isPlayerTurn = !isPlayerTurn;

            DoGameLogic(ind);
        }
    }

    private void DoGameLogic(int col) {
        int height;
        for (height = 3; height >= 0; height--) {
            if (grid[col, height] == 0) {
                grid[col, height] = grid[col, height + 1];
                grid[col, height + 1] = 0;
            } else {
                break;
            }
        }
        height++;
        columnLookups[col][height].color = grid[col, height] == 1 ? Color.blue : Color.red;

        int offSet = 0;
        int counter = 0;
        while (col + 1 * offSet < 12 && grid[col + 1 * offSet, height] == grid[col, height]) {
            offSet++;
        }
        offSet--;

        while (col + 1 * offSet >= 0 && grid[col + 1 * offSet, height] == grid[col, height]) {
            offSet--;
            counter++;
        }

        if (counter >= 4) {
            if (grid[col,height] == 1) {
                manager.FinishedGame();
            } else {
                Timer.instance.Speed = 1f;
                manager.FinishedGame();
            }
        }



        offSet = 0;
        counter = 0;
        while (height + 1 * offSet < 5 && grid[col, height + 1 * offSet] == grid[col, height]) {
            offSet++;
        }
        offSet--;

        while (height + 1 * offSet >= 0 && grid[col, height + 1 * offSet] == grid[col, height]) {
            offSet--;
            counter++;
        }

        if (counter >= 4) {
            if (grid[col, height] == 1) {
                manager.FinishedGame();
            } else {
                Timer.instance.Speed = 1f;
                manager.FinishedGame();
            }
        }



        offSet = 0;
        counter = 0;
        while (height + 1 * offSet < 5 && col + 1 * offSet < 12 && grid[col + 1 * offSet, height + 1 * offSet] == grid[col, height]) {
            offSet++;
        }
        offSet--;

        while (height + 1 * offSet >= 0 && col + 1 * offSet >= 0 && grid[col + 1 * offSet, height + 1 * offSet] == grid[col, height]) {
            offSet--;
            counter++;
        }

        if (counter >= 4) {
            if (grid[col, height] == 1) {
                manager.FinishedGame();
            } else {
                Timer.instance.Speed = 1f;
                manager.FinishedGame();
            }
        }



        offSet = 0;
        counter = 0;
        while (height + 1 * offSet < 5 && col - 1 * offSet >= 0 && grid[col - 1 * offSet, height + 1 * offSet] == grid[col, height]) {
            offSet++;
        }
        offSet--;

        while (height + 1 * offSet >= 0 && col + 1 * offSet < 12 && grid[col - 1 * offSet, height + 1 * offSet] == grid[col, height]) {
            offSet--;
            counter++;
        }

        if (counter >= 4) {
            if (grid[col, height] == 1) {
                manager.FinishedGame();
            } else {
                Timer.instance.Speed = 1f;
                manager.FinishedGame();
            }
        }


        float rng = 0.5f;
        while (!isPlayerTurn) {
            ButtonCallback(Mathf.Clamp(col + (int)(((int)(Random.value * 11) - 6) * rng), 0, 11));
            rng += 0.1f;
        }
    }
}
