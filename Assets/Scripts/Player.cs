using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    List<GameObject> HPs;
    [SerializeField]
    SpriteRenderer SR;
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
    [SerializeField]
    int JumpCount = 3;

    float Dir = 0;

    [SerializeField]
    private int hp = 5;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            hp = Mathf.Clamp(hp, 0, 10);
            Active(HP);
        }
    }


    void Update()
    {
        Dir = InputMgr.Instance.GetAxisRaw(KeyCode.A, KeyCode.D);

        if (Dir != 0)
            SR.flipX = Dir == -1;

        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;

        if ((0 < Dir && transform.position.x <= maxX - MoveRange) || (Dir < 0 && minX + MoveRange <= transform.position.x))
            transform.Translate(new Vector3(Dir, 0, 0) * Time.deltaTime * Speed);

        if (R2D.velocity.y < 0)
        {
            var hit = Physics2D.Raycast(transform.position + 1.5f * Vector3.down, Vector2.down, 0.5f, LayerMask.GetMask("ground"));
            Debug.DrawRay(transform.position + 1.5f * Vector3.down, Vector2.down * 0.5f, Color.red, 0.1f);
            if (hit.transform != null && hit.transform.name == "ground")
            {
                if (Animator.GetBool("DoubleJump"))
                {
                    Animator.SetBool("DoubleJump", false);
                    Animator.SetTrigger("Land");
                }
                Animator.SetBool("Jump", false);
                JumpCount = 3;
            }
        }

        Animator.SetBool("Slide", false);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Animator.SetBool("Slide", true);
            Animator.SetBool("DoubleJump", false);
        }
        else if (JumpCount > 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            R2D.velocity = new Vector2(R2D.velocity.x, 0);

            Animator.ResetTrigger("Land");
            if (Animator.GetBool("Jump"))
            {
                Animator.SetBool("Jump", false);
                Animator.SetBool("DoubleJump", true);
            }
            else
            {
                if (Animator.GetBool("DoubleJump"))
                    Animator.SetTrigger("Again");
                else
                    Animator.SetBool("Jump", true);
            }
            R2D.AddForce(Vector2.up * Jumpforce);
            //--JumpCount;
            GameMgr.Instance.Score += 10;
        }

        GameMgr.Instance.Score += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Period))
        {
            ++HP;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "MousePointer")
        {
            --HP;
            if (HP <= 0)
            {
                Animator.SetTrigger("Die");
                StartCoroutine(Delay(2));
            }
        }
        else if (collision.transform.name.Contains("Obstacle"))
        {
            GameMgr.Instance.Score += 50;
        }
    }

    public void Active(int pivot)
    {
        if (pivot == 0)
        {
            print("die");
        }
        for (int i = 0; i < HPs.Count; i++)
        {
            if (i < pivot)
                HPs[i].SetActive(true);
            else
                HPs[i].SetActive(false);
        }
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);

        if (HP <= 0) SceneMgr.Instance.ChangeScene("GameOver");

        yield return null;
    }
}