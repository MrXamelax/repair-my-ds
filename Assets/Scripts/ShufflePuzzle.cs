using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShufflePuzzle : MonoBehaviour, IInitializable {
    class ShufflePositions {
        public Vector2Int currentPos;
        public Vector2Int desiredPos;

        public ShufflePositions(Vector2Int currentPos, Vector2Int desiredPos) {
            this.currentPos = currentPos;
            this.desiredPos = desiredPos;
        }
    }

    [SerializeField] GameObject spawnPrefab;

    [SerializeField] Sprite[] images4x4;

    MinigameManager manager;
    int fieldSize = 4;
    float imgSize;


    Dictionary<GameObject, ShufflePositions> puzzlePositions;

    Vector2Int freeSpot;

    public void Init(MinigameManager manager, int[] param) {
        this.manager = manager;
        if (param.Length >= 1) fieldSize = param[0];

        puzzlePositions = new Dictionary<GameObject, ShufflePositions>();
        imgSize = 600f / fieldSize;

        freeSpot = new Vector2Int(fieldSize - 1, 0);

        List<Vector2Int> imageSpottelies = new List<Vector2Int>();
        for (int x = 0; x < fieldSize; x++) {
            for (int y = 0; y < fieldSize; y++) {
                if (x == fieldSize - 1 && y == 0) {
                    continue;
                }
                imageSpottelies.Add(new Vector2Int(x, y));
            }
        }

        for (int x = 0; x < fieldSize; x++) {
            for (int y = 0; y < fieldSize; y++) {
                if (x == fieldSize - 1 && y == 0) {
                    continue;
                }

                GameObject spawn = Instantiate(spawnPrefab, transform);

                spawn.GetComponent<RectTransform>().sizeDelta = Vector2.one * imgSize;
                spawn.transform.localPosition = GetCanvasPositionFromCoords(new Vector2Int(x, y));

                Vector2Int spawnImageSpottely = imageSpottelies[(int)(Random.value * (imageSpottelies.Count - 1))];
                imageSpottelies.Remove(spawnImageSpottely);
                puzzlePositions.Add(spawn, new ShufflePositions(new Vector2Int(x, y), spawnImageSpottely));
                spawn.GetComponent<Button>().image.sprite = images4x4[spawnImageSpottely.y * 4 + spawnImageSpottely.x];

                //Debug.Log($"distributed {spawnImageSpottely} with img{spawnImageSpottely.y * 4 + spawnImageSpottely.x}");

                spawn.GetComponent<Button>().onClick.AddListener(() => {
                    ButtonCallback(spawn);
                });
            }
        }

    }

    private Vector3 GetCanvasPositionFromCoords(Vector2Int coords) {
        return new Vector3(coords.x * imgSize - 300f, coords.y * imgSize - 300f, 0f);
    }

    public void ButtonCallback(GameObject button) {
        if ((puzzlePositions[button].currentPos - freeSpot).sqrMagnitude == 1) {
            Vector2Int tmp = freeSpot;
            freeSpot = puzzlePositions[button].currentPos;
            puzzlePositions[button].currentPos = tmp;
            button.transform.localPosition = GetCanvasPositionFromCoords(tmp);
        }
        foreach (ShufflePositions curr in puzzlePositions.Values) {
            if (!(curr.currentPos == curr.desiredPos)) {
                return;
            }
        }
        manager.FinishedGame();
    }
}
