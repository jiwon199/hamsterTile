using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DecoControl : MonoBehaviour
{
    private int NUMDECO;
    private GameObject curWorld;

    // Start is called before the first frame update
    void Start()
    {
        //현재 world 정보들 불러오기
        NUMDECO=GameManager.instance.localPlacedInfo[GameManager.instance.localWorldInfo].Length;
        switch(GameManager.instance.localWorldInfo)
        {
            case 0:
                Debug.Log("Village world load");
                curWorld=GameObject.Find("Canvas").transform.Find("Village").gameObject;
                GameObject.Find("Canvas").transform.Find("City").gameObject.SetActive(false);
                break;
            case 1:
                Debug.Log("City world load");
                curWorld=GameObject.Find("Canvas").transform.Find("City").gameObject;
                GameObject.Find("Canvas").transform.Find("Village").gameObject.SetActive(false);
                break;
        }
        curWorld.SetActive(true);
        
        Decorate();
        Debug.Log(GameManager.instance.localCompleteInfo[0]);
        //햄스빌리지 완료한 경우 worldChange 버튼 활성화
        GameObject.Find("Canvas")
        .transform.Find("changeWorld (Button)")
        .gameObject.SetActive(GameManager.instance.localCompleteInfo[0]);

    }

    // Update is called once per frame
    void Update()
    {
        Decorate();
        
        if(GameManager.instance.localCompleteInfo[GameManager.instance.localWorldInfo]){
            //햄스빌리지 클리어
            curWorld.transform.Find("Reward").gameObject.SetActive(true);
            //햄시티 클리어
        }

    }

    
    void Decorate(){
        for(int i = 0 ; i < NUMDECO ; i++){
            curWorld.transform.GetChild(i).gameObject.SetActive(GameManager.instance.localPlacedInfo[GameManager.instance.localWorldInfo][i]);
        }
    }

    public void changeWorld(){
        if( GameManager.instance.localWorldInfo == 0 ) { GameManager.instance.localWorldInfo = 1; }
        else { GameManager.instance.localWorldInfo = 0; }
        GameManager.instance.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("homeScene");
    }
    
}