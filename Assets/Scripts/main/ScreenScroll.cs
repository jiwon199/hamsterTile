using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class ScreenScroll : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private int world;
    private Vector3 startPos;

    public GameObject cityObj;

    
    void Start()
    {
        world = GameManager.instance.localWorldInfo;
    }

    // Update is called once per frame
    void Update()
    {
        if ( cityObj.transform.position.y > 1.58 ) cityObj.transform.position=new Vector3(0,1.58f,0);
        else if ( cityObj.transform.position.y < -1.58 ) cityObj.transform.position=new Vector3(0,-1.58f,0);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (world == 1) {
            //Debug.Log("드래그시작");
            //Debug.Log(cityObj.transform.position.y);

            if( cityObj.transform.position.y <= 1.581f && cityObj.transform.position.y >= -1.581f)
            {
                startPos = eventData.position;
                //Debug.Log("첫위치할당");
                cityObj.transform.position+=new Vector3(0,(eventData.position.y-startPos.y)/5000,0);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (world == 1) {
            //Debug.Log("드래그 중");
            //Debug.Log(cityObj.transform.position.y);

            if( cityObj.transform.position.y<=1.581f && cityObj.transform.position.y>=-1.581f)
            {
                cityObj.transform.position+=new Vector3(0,(eventData.position.y-startPos.y)/5000,0);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*
        if (world == 1) {
            Debug.Log("드래그 끝");
        }
        */
    }
}
