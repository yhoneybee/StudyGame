using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; } = null;

    public Player player;

    [SerializeField]
    TextMeshProUGUI ScoreText;

    private float score;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            ScoreText.text = $"Score : {((int)score)}";
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Score = 0;
    }
}
