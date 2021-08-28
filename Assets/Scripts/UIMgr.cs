using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

public enum Pivot
{
    TOP_LEFT, TOP_CENTER, TOP_RIGHT,
    MIDDLE_LEFT, MIDDLE_CENTER, MIDDLE_RIGHT,
    BOTTOM_LEFT, BOTTOM_CENTER, BOTTOM_RIGHT
}

public class UIMgr : MonoBehaviour
{
    public static UIMgr Instance { get; private set; } = null;

    public Canvas Canvas;
    public List<Vector2> Pivots = new List<Vector2>()
    {
        new Vector2(0, 1), new Vector2(0.5f, 1), new Vector2(1, 1),
        new Vector2(0, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(1, 0.5f),
        new Vector2(0, 0), new Vector2(0.5f, 0), new Vector2(1, 0),
    };

    public Vector2 TopBottom;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        RectTransform CanvasRT = Canvas.GetComponent<RectTransform>();
        TopBottom.x = CanvasRT.position.y + CanvasRT.rect.height / 2;
        TopBottom.y = CanvasRT.position.y - CanvasRT.rect.height / 2;
    }
}
