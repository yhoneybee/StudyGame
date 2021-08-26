using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D R2D;
    [SerializeField]
    float MoveRange = 1.5f;
    [SerializeField]
    float Speed = 10;
    [SerializeField]
    float Jumpforce = 5;

    float Dir = 0;

    void Update()
    {
        Dir = InputMgr.Instance.GetAxisRaw(KeyCode.A, KeyCode.D);

        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;

        if ((0 < Dir && transform.position.x <= maxX - MoveRange) || (Dir < 0 && minX + MoveRange <= transform.position.x))
            transform.Translate(new Vector3(Dir, 0, 0) * Time.deltaTime * Speed);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            R2D.AddForce(Vector2.up * Jumpforce);
    }
}
