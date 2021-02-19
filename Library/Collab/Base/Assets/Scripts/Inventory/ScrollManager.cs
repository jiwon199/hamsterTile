using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    //SIZE = (전체 아이템 갯수) - 2
    private const int SIZE=4;
    private float[] pos = new float[SIZE];
    private float distance, targetPos;
    private int curIndex, num_item; //num_item : 인벤토리에 존재하는 아이템 갯수


    [SerializeField] Scrollbar scrollbar;
    [SerializeField] GameObject rBtn;
    [SerializeField] GameObject lBtn;


    void Start()
    {
        curIndex=0;
        getNumItem();
        setDistance();
        
    }


    // Update is called once per frame
    void Update()
    {

        //인벤토리에 들어와있는 아이템 갯수
        getNumItem();
        setDistance();

        if(num_item-3>0){
            if(num_item-3<curIndex){
                targetPos=pos[--curIndex];
            } 
            else {
                targetPos=pos[curIndex];
            }
        }

        //targetPos값 바뀌면 부드럽게 이동시켜 주는 코드
        scrollbar.value=Mathf.Lerp(scrollbar.value,targetPos,0.1f);


        //인벤토리 방향 버튼 활성화/비활성화 여부
        if(num_item<4){
            lBtn.SetActive(false); rBtn.SetActive(false);
        }
        else{
            if(curIndex==0){
                lBtn.SetActive(false); rBtn.SetActive(true);
            }
            else if (curIndex==num_item-3){
                lBtn.SetActive(true); rBtn.SetActive(false);
            }
            else {
                lBtn.SetActive(true); rBtn.SetActive(true);
            }
        }
    }

    //pos 계산 함수
    private void setDistance(){
        if (num_item>3){
            distance=1f/(num_item-3);
            for (int i=0;i<num_item-2;i++){
                pos[i]=distance*i;
            }
        }
        else distance=0;
    }

    //인벤토리에 들어있는 아이템 갯수 확인
    private void getNumItem(){
        Inventory_v Inventory = GameObject.Find("Inventory Village").GetComponent<Inventory_v>();
        num_item=0;
        for(int i=0;i<SIZE+2;i++){
            if(Inventory.item_village[i].activeSelf){
                num_item++;
            }
        }
    }


    
    //버튼 on click을 위한 함수
    public void RightClick(){
        if (curIndex<num_item-3){
            targetPos=pos[++curIndex];
        }
    }

    public void LeftClick(){
        if (curIndex>0){
            targetPos=pos[--curIndex];
        }
    }
}
