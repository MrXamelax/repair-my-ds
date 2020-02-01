using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrderNumbers : MonoBehaviour, IInitializable {
    MinigameManager manager;

    [SerializeField] GameObject spawnPrefab;

    [SerializeField] int fieldSize = 4;
    [SerializeField] int biggestNumber;

    List<int> numbers;

    public void Init(MinigameManager manager, int[] param) {
        this.manager = manager;
        if (param.Length >= 1) fieldSize = param[0];
        if (param.Length >= 2) biggestNumber = param[1];

        int maxSize = (int)GetComponent<RectTransform>().rect.height - 100;

        GridLayoutGroup glg = GetComponent<GridLayoutGroup>();

        glg.cellSize = Vector2.one * (maxSize / (fieldSize + 1));
        glg.spacing = Vector2.one * 0.5f * (maxSize / (fieldSize + 1));
        glg.constraintCount = fieldSize;


        int fieldSizeSq = fieldSize * fieldSize;
        if (biggestNumber < fieldSizeSq) {
            biggestNumber = fieldSizeSq;
        }

        int amountOverRequired = biggestNumber - fieldSizeSq;

        numbers = new List<int>();

        numbers.Add(1);

        while (numbers.Count < fieldSizeSq) {
            int jump = (int)(Random.value * amountOverRequired * (numbers.Count / (float)fieldSizeSq));
            numbers.Add(numbers.Last() + 1 + jump);
            amountOverRequired -= jump;
        }

        List<int> temp = new List<int>();
        temp.AddRange(numbers.AsEnumerable());

        for (int i = 0; i < fieldSizeSq; i++) {
            GameObject spawn = Instantiate(spawnPrefab, transform);
            int buttonNumber = temp.ElementAt((int)(Random.value * (temp.Count - 1)));
            spawn.name = $"But-{buttonNumber}";
            spawn.GetComponentInChildren<Text>().text = buttonNumber.ToString();
            spawn.GetComponent<Button>().onClick.AddListener(() => {
                ButtonCallback(spawn, buttonNumber);
            });
            temp.Remove(buttonNumber);
        }
    }

    public void ButtonCallback(GameObject button, int number) {
        if (number == numbers.First()) {
            button.SetActive(false);
            numbers.RemoveAt(0);
            if (numbers.Count == 0) {
                manager.FinishedGame();
            }
        } else {
            //TODO punishment
        }
    }
}
