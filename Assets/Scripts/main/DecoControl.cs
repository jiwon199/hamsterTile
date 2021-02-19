using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DecoControl : MonoBehaviour
{
    private int NUMDECO;
    private int world;
    private Sprite[] worldChangeBtn;
    //private GameObject curWorld;

    void Awake()
    {
        worldChangeBtn=Resources.LoadAll<Sprite>("Button_moveTo");
    }
    void Start()
    {
        
        //스토리씬을 보고 들어온 경우(첫플레이) 시에 안내 패널 띄우는 함수
        if(!GameManager.instance.localWatchStory) {
            transform.Find("firstAdvicePanel").gameObject.SetActive(true);
        }

        
        world=GameManager.instance.localWorldInfo;
        //현재 world 정보들 불러오기
        switch(world)
        {
            case 0:
                Debug.Log("Village world load");
                GameObject.Find("Canvas").transform.Find("Village").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("City").gameObject.SetActive(false);
                transform.Find("Hamsvillage Title").gameObject.SetActive(true);
                GameObject.Find("Hamcity Title").SetActive(false);
                break;
            case 1:
                Debug.Log("City world load");
                GameObject.Find("Canvas").transform.Find("City").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("Village").gameObject.SetActive(false);
                transform.Find("Hamcity Title").gameObject.SetActive(true);
                GameObject.Find("Hamsvillage Title").SetActive(false);
                break;
        }

        Decorate();
        
        Debug.Log("햄스빌리지 클리어 정보 : "+GameManager.instance.localCompleteInfo[0]);


        //햄시티 스토리 시청 완료한 경우 worldChange 버튼 활성화
        GameObject.Find("Canvas")
        .transform.Find("changeWorld (Button)")
        .gameObject.SetActive(GameManager.instance.localWatchStory2);
        //worldChange 버튼 이미지 변경
        if(GameManager.instance.localCompleteInfo[0])
        {
            GameObject.Find("Canvas").transform.Find("changeWorld (Button)")
            .gameObject.GetComponent<Image>().sprite = worldChangeBtn[1-world];
        }
    }

    // Update is called once per frame
    void Update()
    {
        Decorate();

        //월드 클리어시 reward 배치
        if(world == 0 && GameManager.instance.localCompleteInfo[0])
        {
            GameObject.Find("Village").transform.Find("Village Reward").gameObject.SetActive(true);
        }
        else if(world == 1 && GameManager.instance.localCompleteInfo[1])
        {
            GameObject.Find("City").transform.Find("City Reward").gameObject.SetActive(true);
        }
    }

    
    void Decorate(){
        
        if(world == 0 && !GameManager.instance.localCompleteInfo[0])
        {
            for(int i = 0 ; i < GameManager.instance.localPlacedInfo.Length; i++)
            {
                GameObject.Find("Village").transform.GetChild(i).gameObject.SetActive(GameManager.instance.localPlacedInfo[i]);
            }
        }
        else if(world == 1 && !GameManager.instance.localCompleteInfo[1])
        {
            for(int i = 0 ; i < GameManager.instance.localPlacedInfo2.Length; i++)
            {
                GameObject.Find("City").transform.GetChild(i).gameObject.SetActive(GameManager.instance.localPlacedInfo2[i]);
            }
        }
    }    
    public void changeWorld()
    {
        GameManager.instance.updateWorld();
    }
}