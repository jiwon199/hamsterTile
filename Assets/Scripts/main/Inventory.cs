using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    private GameObject curWorld, curInventory;
    private int world;

    void Start()
    {
        world = GameManager.instance.localWorldInfo;
        switch(world)
        {
            case 0:
                Debug.Log("Village inventory load");
                curInventory = GameObject.Find("Viewport").transform.Find("Village Inventory").gameObject;
                curInventory.SetActive(true);
                GameObject.Find("Viewport").transform.Find("City Inventory").gameObject.SetActive(false);
                break;
            case 1:
                Debug.Log("City inventory load");
                curInventory = GameObject.Find("Viewport").transform.Find("City Inventory").gameObject;
                curInventory.SetActive(true);
                GameObject.Find("Viewport").transform.Find("Village Inventory").gameObject.SetActive(false);
                break;
        }

        GameObject.Find("Inventory (Scroll View)").GetComponent<ScrollRect>().content = curInventory.GetComponent<RectTransform>();
        
        

        /*자식컴포넌트(아이템 목록) 받아와서 대입
        item_village=new GameObject[curWorld.transform.childCount];
        Debug.Log(curWorld.transform.childCount);
        for(int i=0;i<curWorld.transform.childCount;i++){
            item_village[i]=curWorld.transform.GetChild(i).gameObject;
        }
        */

        if(!GameManager.instance.localCompleteInfo[world])
        {
            update_inventory();
        }

    }


    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.localCompleteInfo[world])
        {
            update_inventory();
        }
    }

    //빌리지 인벤토리 업데이트..
    private void update_inventory(){
        
        switch(world)
        {
            case 0 :
                curWorld=GameObject.Find("Village Inventory").gameObject;
                if(GameManager.instance.localClearInfo[23]) curWorld.transform.GetChild(9).gameObject.SetActive(!GameManager.instance.localPlacedInfo[9]);
                if(GameManager.instance.localClearInfo[20]) curWorld.transform.GetChild(8).gameObject.SetActive(!GameManager.instance.localPlacedInfo[8]);
                if(GameManager.instance.localClearInfo[17]) curWorld.transform.GetChild(7).gameObject.SetActive(!GameManager.instance.localPlacedInfo[7]);
                if(GameManager.instance.localClearInfo[14]) curWorld.transform.GetChild(6).gameObject.SetActive(!GameManager.instance.localPlacedInfo[6]);
                if(GameManager.instance.localClearInfo[11]) curWorld.transform.GetChild(5).gameObject.SetActive(!GameManager.instance.localPlacedInfo[5]);
                if(GameManager.instance.localClearInfo[8]) curWorld.transform.GetChild(4).gameObject.SetActive(!GameManager.instance.localPlacedInfo[4]);
                if(GameManager.instance.localClearInfo[6]) curWorld.transform.GetChild(3).gameObject.SetActive(!GameManager.instance.localPlacedInfo[3]);
                if(GameManager.instance.localClearInfo[4]) curWorld.transform.GetChild(2).gameObject.SetActive(!GameManager.instance.localPlacedInfo[2]);
                if(GameManager.instance.localClearInfo[2]) curWorld.transform.GetChild(1).gameObject.SetActive(!GameManager.instance.localPlacedInfo[1]);
                if(GameManager.instance.localClearInfo[0]) curWorld.transform.GetChild(0).gameObject.SetActive(!GameManager.instance.localPlacedInfo[0]);
                break;
            case 1 :
                curWorld=GameObject.Find("City Inventory").gameObject;
                if(GameManager.instance.localClearInfo2[39]) curWorld.transform.GetChild(11).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[11]);
                if(GameManager.instance.localClearInfo2[35]) curWorld.transform.GetChild(10).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[10]);
                if(GameManager.instance.localClearInfo2[31]) curWorld.transform.GetChild(9).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[9]);
                if(GameManager.instance.localClearInfo2[27]) curWorld.transform.GetChild(8).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[8]);
                if(GameManager.instance.localClearInfo2[23]) curWorld.transform.GetChild(7).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[7]);
                if(GameManager.instance.localClearInfo2[19]) curWorld.transform.GetChild(6).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[6]);
                if(GameManager.instance.localClearInfo2[15]) curWorld.transform.GetChild(5).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[5]);
                if(GameManager.instance.localClearInfo2[11]) curWorld.transform.GetChild(4).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[4]);
                if(GameManager.instance.localClearInfo2[7]) curWorld.transform.GetChild(3).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[3]);
                if(GameManager.instance.localClearInfo2[4]) curWorld.transform.GetChild(2).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[2]);
                if(GameManager.instance.localClearInfo2[2]) curWorld.transform.GetChild(1).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[1]);
                if(GameManager.instance.localClearInfo2[0]) curWorld.transform.GetChild(0).gameObject.SetActive(!GameManager.instance.localPlacedInfo2[0]);
                break;
        }
    }






    //테스트용,,배치 초기화
    public void initData(){
        GameManager.instance.makeD();
        GameManager.instance.Load();

        UnityEngine.SceneManagement.SceneManager.LoadScene("homeScene");
    }
}