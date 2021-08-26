using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D R2D;
    [SerializeField]
    Animator Animator;
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

        if (R2D.velocity.y < 0)
        {
            var hit = Physics2D.Raycast(transform.position + 1.5f * Vector3.down, Vector2.down, 0.5f, LayerMask.GetMask("ground"));
            Debug.DrawRay(transform.position + 1.5f * Vector3.down, Vector2.down * 0.5f, Color.red, 0.1f);
            if (hit.transform != null && hit.transform.name == "ground")
                Animator.SetBool("Jump", false);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            Animator.SetBool("Jump", true);
            R2D.AddForce(Vector2.up * Jumpforce);
        }
    }
}
