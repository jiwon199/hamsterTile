using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class item : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler ,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Vector3 itemPos;
    private int world;
    public int index;  //해당 아이템인덱스,,

    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject bangObj;
    [SerializeField] private GameObject largeBangObj;


    // Start is called before the first frame update
    void Start()
    {
        world=GameManager.instance.localWorldInfo;
    }

    // Update is called once per frame
    void Update()
    {

    }


    //클릭하면 아이템이 조금 불투명해짐
    public void OnPointerDown (PointerEventData eventData)
    {
        Color color = itemImage.color;
        color.a=0.7f;
        itemImage.color=color;
    }
    public void OnPointerUp (PointerEventData eventData)
    {
        Color color = itemImage.color;
        color.a=1f;
        itemImage.color=color;
    }




    public void OnBeginDrag(PointerEventData eventData){
        //인벤토리 내에서 자리만 남겨두고 이미지 투명하게 만듦
        Color color = itemImage.color;
        color.a=0;
        itemImage.color=color;

        //Drag 오브젝트에게 전달
        DragItem.instance.dragItem=this;
        DragItem.instance.DragSetImage(itemImage);
        
        DragItem.instance.transform.position=eventData.position;
    }

    public void OnDrag(PointerEventData eventData){          
        DragItem.instance.transform.position=eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData){

        float X=DragItem.instance.transform.position.x;
        float Y=DragItem.instance.transform.position.y;


        //마을 위치에 내려두었을 때만 아이템 인벤토리에서 사라짐
        //아이템 위치에 bang이펙트 발생
        if (X>=90 && X<=990 && Y>=420 && Y<=1600) 
        {
            itemPos = GameObject.Find("Canvas").transform.GetChild(world).gameObject.transform.GetChild(index).position;
            if(world == 0){
                switch(index){
                    case 0: case 1: case 7: Instantiate(largeBangObj, itemPos ,Quaternion.identity); break;
                    default: Instantiate(bangObj , itemPos ,Quaternion.identity); break;
                }
            }
            else {
                switch(index){
                    case 0: case 1: case 2: case 5: case 10: Instantiate(largeBangObj, itemPos ,Quaternion.identity); break;
                    default: Instantiate(bangObj , itemPos ,Quaternion.identity); break;
                }
            }
            GameManager.instance.updatePlaced(index);
        }


        DragItem.instance.SetColor(0);
        DragItem.instance.dragItem=null;

        //투명하게 만들었던 아이템 이미지 원래대로 돌려놓음
        Color color = itemImage.color;
        color.a=1;
        itemImage.color=color;

    }    
}
