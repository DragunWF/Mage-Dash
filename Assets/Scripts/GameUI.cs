using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI difficultyText;

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        difficultyText = GameObject.Find("DifficultyText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        scoreText.text = "Score: 0";
        difficultyText.text = "Difficulty: 1";
    }

    private void Update()
    {

    }
}
