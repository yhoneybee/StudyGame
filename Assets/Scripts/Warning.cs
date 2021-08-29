using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Warning : MonoBehaviour
{
    [SerializeField]
    Direction Direction;

    public RectTransform RT;

    [SerializeField]
    TextMeshProUGUI Text;

    Coroutine CTextEffect = null;

    private Color normal_color;
    public Color NormalColor
    {
        get { return normal_color; }
        set
        {
            normal_color = value;
            normal_color.a = 0.509804f;
            Image.color = normal_color;
        }
    }

    public float During { get; set; }

    [SerializeField]
    Image Image;

    private void Update()
    {
        if (During < 0)
        {
            ObstacleMgr.Instance.ShotObstacle(Direction, RT.position.y);
            ObstacleMgr.ReturnObj(Pool.WARNING, gameObject);
        }
        During -= Time.deltaTime;
    }

    public void Setup(Direction direction, Color normal_color, float during)
    {
        Direction = direction;
        NormalColor = normal_color;
        During = during;

        if (CTextEffect != null) StopCoroutine(CTextEffect);
        CTextEffect = StartCoroutine(ETextEffect());
        //StartCoroutine(EColorEffect());
    }

    IEnumerator EColorEffect()
    {
        while (During >= 0)
        {
            Image.color = NormalColor;
            yield return new WaitForSeconds(During / 6);
            Image.color = Color.black;
            yield return new WaitForSeconds(During / 6);
            Image.color = NormalColor;
        }
        yield return null;
    }
    IEnumerator ETextEffect()
    {
        string temp = "- - - - - !  WARNING  ! - - - - -";
        while (During >= 0)
        {
            char[] str = Text.text.ToCharArray();

            if (Direction == Direction.LEFT)
            {
                for (int i = temp.Length - 1; i >= 0 && During >= 0; i--)
                {
                    if (Text.text[i] == '-')
                    {
                        str[i] = '<';

                        Text.text = new string(str);

                        yield return new WaitForSeconds(0.023f * During);
                    }
                }
            }
            else if (Direction == Direction.RIGHT)
            {
                for (int i = 0; i < temp.Length && During >= 0 && During >= 0; i++)
                {
                    if (Text.text[i] == '-')
                    {
                        str[i] = '>';

                        Text.text = new string(str);

                        yield return new WaitForSeconds(0.023f * During);
                    }
                }
            }

            Text.text = temp;
        }

        yield return null;
    }
}
