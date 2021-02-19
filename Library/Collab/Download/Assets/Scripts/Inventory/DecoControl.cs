using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DecoControl : MonoBehaviour
{
    private int NUMDECO;
    private int world;
    //private GameObject curWorld;

    void Start()
    {
        world=GameManager.instance.localWorldInfo;
        //현재 world 정보들 불러오기
        switch(world)
        {
            case 0:
                Debug.Log("Village world load");
                GameObject.Find("Canvas").transform.Find("Village").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("City").gameObject.SetActive(false);
                break;
            case 1:
                Debug.Log("City world load");
                GameObject.Find("Canvas").transform.Find("City").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("Village").gameObject.SetActive(false);
                break;
        }

        Decorate();
        
        Debug.Log("햄스빌리지 클리어 정보 : "+GameManager.instance.localCompleteInfo[0]);


        //햄스빌리지 완료한 경우 worldChange 버튼 활성화
        GameObject.Find("Canvas")
        .transform.Find("changeWorld (Button)")
        .gameObject.SetActive(GameManager.instance.localCompleteInfo[0]);
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

    public void changeWorld(){
        if( GameManager.instance.localWorldInfo == 0 ) { GameManager.instance.localWorldInfo = 1; }
        else { GameManager.instance.localWorldInfo = 0; }
        GameManager.instance.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("homeScene");
    }
    
}