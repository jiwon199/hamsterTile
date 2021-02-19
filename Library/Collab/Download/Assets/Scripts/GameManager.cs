using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
//내부 클래스=json에 들어가는 정보 클래스
[System.Serializable]
public class clearData
{
    public bool[] clear=new bool[24];  //햄스빌리지
    public bool[] clear2 = new bool[40]; //햄시티
    public bool watchStory;  //햄스빌리지 스토리 영상을 봤는지
    public bool watchStory2; //햄시티 스토리 영상을 봤는지
    public bool[] placed = new bool[10];
    public bool[] placed2 = new bool[12];
    public bool[] complete = new bool[2];
    public int world;   //햄스빌리지==0, 햄시티==1;

    public bool vilTuto;
    public bool cityTuto;

}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    string filePath;
    public bool[] localClearInfo=new bool[24];
    public bool[] localClearInfo2 = new bool[40];

    public bool[] localPlacedInfo = new bool[10];
    public bool[] localPlacedInfo2 = new bool[12];
    public bool[] localCompleteInfo = new bool[2];
    public int localWorldInfo;
    public bool localWatchStory;
    public bool localWatchStory2;

    public bool localVilTuto;
    public bool localCityTuto;

    int lastStage = 24;
    int lastStage2 = 40;

    // bool[] clear=new bool[1];
    void Awake()
    {
         //Screen.SetResolution(1080,(1080/9)*16, true);
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        //  instance = this;
        DontDestroyOnLoad(this);

         
    }
    void Start()
    {
        filePath = Application.persistentDataPath + "/clearInfo.json";
        //스테이지 클리어 얼만큼했는지..정보 로드       
        Load();

      
    }
    void Update()
    {
        //퍼즐씬은 리로드해도 배경음악 이어지도록. 다른씬으로 가면 없애기
        if (SceneManager.GetActiveScene().name != "puzzleScene")
        {
            GameObject keepBgm = GameObject.Find("KeepBGM");
            if (keepBgm != null) { Destroy(keepBgm); }
        }
    }
    //Application.persistentDataPath + "/clearInfo.json" 위치에 12개짜리, 모두 false인 거 만들기.
    public void makeD()
    {     
       
        clearData cleardata= new clearData();
        for(int i = 0; i < lastStage; i++)
        {         
            cleardata.clear[i]  = false;
        }
        for (int i = 0; i < lastStage2; i++)
        {
            cleardata.clear2[i] = false;
        }
        for (int i=0;i<cleardata.placed.Length;i++)
        {
            cleardata.placed[i] = false;
        }
        for (int i=0;i<cleardata.placed2.Length;i++)
        {
            cleardata.placed2[i] = false;
        }
        cleardata.complete[0]=false; cleardata.complete[1]=false;
        cleardata.world = 0;
        cleardata.watchStory=false;
        cleardata.watchStory2=false;
        cleardata.cityTuto = false;
        cleardata.vilTuto = false;
        File.WriteAllText(Application.persistentDataPath + "/clearInfo.json", JsonUtility.ToJson(cleardata));
    }
    public void Load()
    {
         
        //파일이 없으면 해당위치에 만든다.(첫플레이시)
        if (!File.Exists(filePath)) { makeD(); }

         string str = File.ReadAllText(Application.persistentDataPath + "/clearInfo.json");
         clearData cleardata = JsonUtility.FromJson<clearData>(str);

        //읽어온 클리어정보를 localClearInfo에
        for (int i = 0; i < lastStage; i++)
        {
            localClearInfo[i] = cleardata.clear[i];
        }
        for (int i = 0; i < lastStage2; i++)
        {
            localClearInfo2[i] = cleardata.clear2[i];
        }

        //읽어온 아이템정보를 localPlacedInfo에 복사
        for (int i=0;i<cleardata.placed.Length;i++)
        {
            localPlacedInfo[i] = cleardata.placed[i];
        }
        for (int i=0;i<cleardata.placed2.Length;i++)
        {
            localPlacedInfo2[i] = cleardata.placed2[i];
        }
        //읽어온 월드complete정보와 마지막으로 머물렀던 월드 정보 복사
        localCompleteInfo[0]=cleardata.complete[0]; localCompleteInfo[1]=cleardata.complete[1];
        localWorldInfo = cleardata.world;
        localWatchStory= cleardata.watchStory;
        localWatchStory2= cleardata.watchStory2;
        localCityTuto = cleardata.cityTuto;
        localVilTuto = cleardata.vilTuto;



    }
    public void Save()
    {
        clearData cleardata = new clearData();

        //게임 플레이하면서 localClearInfo 변수를 조작, 세이브할때 localClearInfo 정보를 cleardata.data에 넣고 파일에 쓴다.
        for (int i = 0; i < lastStage; i++)
        {
            cleardata.clear[i] = localClearInfo[i];
        }
        for (int i = 0; i < lastStage2; i++)
        {
            cleardata.clear2[i] = localClearInfo2[i];
        }
        //localPlacedInfo를 cleardata로
        for (int i=0;i<cleardata.placed.Length;i++)
        {
            cleardata.placed[i] = localPlacedInfo[i];
        }
        for (int i=0;i<cleardata.placed2.Length;i++)
        {
            cleardata.placed2[i] = localPlacedInfo2[i];
        }
        cleardata.complete[0] = localCompleteInfo[0]; cleardata.complete[1] = localCompleteInfo[1];
        cleardata.world = localWorldInfo;
        cleardata.watchStory = localWatchStory;
        cleardata.watchStory2 = localWatchStory2;
        cleardata.cityTuto = localCityTuto;
        cleardata.vilTuto = localVilTuto;        
        File.WriteAllText(Application.persistentDataPath + "/clearInfo.json", JsonUtility.ToJson(cleardata));
    }
    
    //퍼즐 씬에서 클리어 조건 맞추면 호출되는 함수.
    public void updateClear(int n)
    {
        // Debug.Log("updateClear 호출됨"+n);        
        if (localWorldInfo == 0) {   localClearInfo[n] = true; }
        else {  localClearInfo2[n] = true; }
        Save();
    }

    //홈화면에서 아이템을 배치할 경우 호출되는 함수
    public void updatePlaced(int n)
    {
        if (localWorldInfo==0){localPlacedInfo[n] = true;}
        else {localPlacedInfo2[n] = true;}

        Save();
    }
    // 홈화면 각 월드 완료시에 호출되는 함수
    public void updateComplete(int n)
    {
        localCompleteInfo[n]=true;
        Save();
    }
    // 튜토리얼 스토리 시청 끝내면 호출되는 함수-스토리를 봤으면 이전 데이터가 있는 것으로 간주
    public void updateStoryShow()
    {
        localWatchStory = true;
        Save();
    }
    // 햄스빌리지-햄시티 스토리 시청 끝내면 호출되는 함수
    public void updateStoryShow2()
    {
        localWatchStory2 = true;
        Save();
    }
    //빌리지 퍼즐튜토 끝내면 호출되는 함수
    public void updateTuto_vil()
    {
        localVilTuto = true;
        Save();
    }
    //시티 퍼즐튜토 끝내면 호출되는 함수
    public void updateTuto_cityl()
    {
        localCityTuto = true;
        Save();
    }
    //햄스빌리지-햄시티 이동 함수
    public void updateWorld()
    {
        if(localWorldInfo == 0) localWorldInfo = 1;
        else localWorldInfo = 0;
        Save();
    }

}
