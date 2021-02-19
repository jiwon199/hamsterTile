using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csUIPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float fScaleWidth = ((float)Screen.width / (float)Screen.height) / ((float)16 / (float)9);
        Vector3 vecButtonPos = GetComponent<RectTransform>().localPosition;
        vecButtonPos.x = vecButtonPos.x * fScaleWidth;
        GetComponent<RectTransform>().localPosition = new Vector3(vecButtonPos.x, vecButtonPos.y, vecButtonPos.z);
     }

   
}
