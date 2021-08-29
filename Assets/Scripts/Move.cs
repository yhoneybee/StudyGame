using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Direction Direction;

    [SerializeField]
    float Speed = 10;

    void Update()
    {
        if ((Direction == Direction.LEFT && transform.position.x < -30) || Direction == Direction.RIGHT && 30 < transform.position.x)
            ObstacleMgr.ReturnObj(Pool.OBSTACLE, gameObject);

        transform.Translate(Speed * Time.deltaTime * new Vector3(Direction == Direction.LEFT ? -1 : 1, 0, 0));
    }
}
