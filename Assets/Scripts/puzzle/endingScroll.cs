using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class endingScroll : MonoBehaviour
{
    public ScrollRect sr;
    
    float posY = 0;
    // Start is called before the first frame update
    void Start()
    {
        //1~1874±îÁö
        sr.content.localPosition = new Vector3(0, 1, 0);
     

    }
    
    // Update is called once per frame
    void Update()
    {

        if (!Input.GetMouseButton(0))
        {
            posY = sr.content.localPosition.y;
            if (posY <= 1874) posY++;
            sr.content.localPosition = new Vector3(0, posY, 0);
        }
         
       // Debug.Log(sr.content.localPosition.y);
        
    }
}
