using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pool
{
    WARNING,
    OBSTACLE,
}

public class ObstacleMgr : MonoBehaviour
{
    public static ObstacleMgr Instance { get; private set; } = null;

    [SerializeField]
    Warning OriginWarning;

    [SerializeField]
    Move OriginObstacle;

    List<Queue<GameObject>> pools = new List<Queue<GameObject>>()
    {
        new Queue<GameObject>(),new Queue<GameObject>(),
    };

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

    GameObject CreateObj(Pool poolType)
    {
        GameObject obj = null;
        switch (poolType)
        {
            case Pool.WARNING:
                obj = Instantiate(OriginWarning, Vector3.zero, Quaternion.identity).gameObject;
                break;
            case Pool.OBSTACLE:
                obj = Instantiate(OriginObstacle, Vector3.zero, Quaternion.identity).gameObject;
                break;
        }
        obj.SetActive(false);
        //obj.transform.SetParent(transform);
        return obj;
    }
    public static GameObject GetObj(Pool poolType)
    {
        if (Instance.pools[((int)poolType)].Count > 0)
        {
            var obj = Instance.pools[((int)poolType)].Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);

            return obj;
        }
        else
        {
            var obj = Instance.CreateObj(poolType);
            obj.SetActive(true);
            obj.transform.SetParent(null);

            return obj;
        }
    }
    public static void ReturnObj(Pool poolType, GameObject obj)
    {
        obj.SetActive(false);
        //obj.transform.SetParent(Instance.transform);
        Instance.pools[((int)poolType)].Enqueue(obj);
    }
    public void WarningObstacle(Direction direction, params float[] pos_y)
    {
        for (int i = 0; i < pos_y.Length; i++)
        {
            //var warning = Instantiate(OriginWarning, Vector3.zero, Quaternion.identity);
            var obj = GetObj(Pool.WARNING);
            var warning = obj.GetComponent<Warning>();
            warning.Setup(direction, Colors[WarningCount++], Random.Range(0.0f, 5.0f));
            warning.transform.SetParent(UIMgr.Instance.Canvas.transform, false);
            warning.RT.position = new Vector2(0, pos_y[i]);
            warning.RT.localScale = Vector3.one;
        }
    }
    public void ShotObstacle(Direction direction, float y)
    {
        GameObject go;
        Move move = null;

        switch (direction)
        {
            case Direction.UP:
                break;
            case Direction.DOWN:
                break;
            case Direction.LEFT:
                go = GetObj(Pool.OBSTACLE);
                move = go.GetComponent<Move>();
                move.transform.position = new Vector3(10, y);
                break;
            case Direction.RIGHT:
                go = GetObj(Pool.OBSTACLE);
                move = go.GetComponent<Move>();
                move.transform.position = new Vector3(-10, y);
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
