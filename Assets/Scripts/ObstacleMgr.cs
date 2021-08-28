using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMgr : MonoBehaviour
{
    public static ObstacleMgr Instance { get; private set; } = null;

    [SerializeField]
    Warning OriginWarning;

    [SerializeField]
    Move OriginObstacle;

    List<Color> Colors = new List<Color>()
    {
        Color.red, new Color(1,0.5f,0,1), Color.yellow, Color.green, Color.cyan, Color.blue, new Color(0.5f,0,1,1), Color.black
    };

    Coroutine CCountReset = null;

    private int warning_count = 0;
    public int WarningCount
    {
        get { return warning_count; }
        set
        {
            if (CCountReset != null)
                StopCoroutine(CCountReset);

            warning_count = value;
            warning_count %= Colors.Count;

            CCountReset = StartCoroutine(ECountReset());
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
    }

    public void WarningObstacle(Direction direction, params float[] pos_y)
    {
        for (int i = 0; i < pos_y.Length; i++)
        {
            var warning = Instantiate(OriginWarning, Vector3.zero, Quaternion.identity);
            warning.Setup(direction, Colors[WarningCount++], Random.Range(0.0f, 5.0f));
            warning.transform.SetParent(UIMgr.Instance.Canvas.transform, false);
            warning.RT.position = new Vector2(0, pos_y[i]);
        }

    }
    public void ShotObstacle(Direction direction, float y)
    {
        Move move = null;

        switch (direction)
        {
            case Direction.UP:
                break;
            case Direction.DOWN:
                break;
            case Direction.LEFT:
                move = Instantiate(OriginObstacle, new Vector3(10, y), Quaternion.identity);
                break;
            case Direction.RIGHT:
                move = Instantiate(OriginObstacle, new Vector3(-10, y), Quaternion.identity);
                break;
        }

        move.Direction = direction;
    }

    IEnumerator ECountReset()
    {
        yield return new WaitForSeconds(2);
        WarningCount = 0;

        yield return null;
    }
}
