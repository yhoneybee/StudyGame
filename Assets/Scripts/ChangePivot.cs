using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePivot : MonoBehaviour
{
    [SerializeField]
    RectTransform RT;

    private void Update()
    {
        if ((RT.position.y + RT.rect.height / 2) * 10 > UIMgr.Instance.TopBottom.x)
        {
            RT.anchorMax = RT.anchorMin = UIMgr.Instance.Pivots[((int)Pivot.BOTTOM_CENTER)];
            RT.anchoredPosition = new Vector2(RT.anchoredPosition.x, -RT.anchoredPosition.y);
        }
        else if ((RT.position.y - RT.rect.height / 2) * 10 < UIMgr.Instance.TopBottom.y)
        {
            RT.anchorMax = RT.anchorMin = UIMgr.Instance.Pivots[((int)Pivot.TOP_CENTER)];
            RT.anchoredPosition = new Vector2(RT.anchoredPosition.x, -RT.anchoredPosition.y);
        }
    }
}
