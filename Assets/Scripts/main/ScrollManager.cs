using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    //SIZE = (전체 아이템 갯수) - 2
    private int SIZE;
    private float[] pos;
    private float distance, targetPos;
    private int curIndex, num_item, world; //num_item : 인벤토리에 존재하는 아이템 갯수


    [SerializeField] Scrollbar scrollbar;
    [SerializeField] GameObject rBtn;
    [SerializeField] GameObject lBtn;


    void Start()
    {
        world = GameManager.instance.localWorldInfo;
        scrollbar.value=0;
        curIndex=0;
        //SIZE=GameManager.instance.localPlacedInfo.Length - 2;

        //배열 크기 : 아이템수-2
        if( world == 0 ) { pos = new float[8]; }
        else { pos = new float[10]; }
        getNumItem();
        setDistance();
        
    }

    void Update()
    {
        //인벤토리에 들어와있는 아이템 갯수로 위치 계산
        getNumItem();
        setDistance();

        //화살표가 헛돌지 않게 만듦
        if(num_item > 3 && curIndex > num_item-3) curIndex = num_item-3;
        targetPos = pos[curIndex];
        
        //targetPos값 바뀌면 부드럽게 이동시켜 주는 코드
        scrollbar.value=Mathf.Lerp(scrollbar.value,targetPos,0.1f);

        //인벤토리 방향 버튼 활성화/비활성화 여부
        if(num_item<4)
        {
            lBtn.SetActive(false); rBtn.SetActive(false);
        }
        else
        {
            if(curIndex==0)
            {
                lBtn.SetActive(false); rBtn.SetActive(true);
            }
            else if (curIndex >= num_item-3)
            {
                lBtn.SetActive(true); rBtn.SetActive(false);
            }
            else
            {
                lBtn.SetActive(true); rBtn.SetActive(true);
            }
        }
    }

    //pos 계산 함수
    private void setDistance(){
        if (num_item > 3){
            distance = 1f / (num_item-3);
            for (int i=0;i<num_item-2;i++){
                pos[i]=distance*i;
            }
        }
    }

    //인벤토리에 들어있는 아이템 갯수 확인
    private void getNumItem(){
        num_item=0;
        switch (world)
        {
            case 0:
                for(int i=0; i<GameManager.instance.localPlacedInfo.Length; i++){
                    if(GameObject.Find("Village Inventory").transform.GetChild(i).gameObject.activeSelf) { num_item++; }
                }
                break;
            case 1:
                for(int i=0; i<GameManager.instance.localPlacedInfo2.Length; i++){
                    if(GameObject.Find("City Inventory").transform.GetChild(i).gameObject.activeSelf) { num_item++; }
                }
                break;
        }
    }
    
    //버튼 on click을 위한 함수
    public void RightClick(){
        if (curIndex<num_item-3){
            targetPos = pos[++curIndex];
        }
    }
    public void LeftClick(){
        if (curIndex>0){
            targetPos = pos[--curIndex];
        }
    }
}