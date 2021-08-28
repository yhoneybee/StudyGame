using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> HPs;

    Vector3 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    [SerializeField]
    private int hp;
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
        transform.position = new Vector3(MousePos.x, MousePos.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        --HP;
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
}
