using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//내부 클래스=json에 들어가는 정보 클래스
[System.Serializable]
public class clearData
{
    public bool[] clear=new bool[13];
    public bool[][] placed = new bool[2][]{ new bool[6], new bool[2] };
    public bool[] complete = new bool[2];
    public int world;   //햄스빌리지==0, 햄시티==1;
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    string filePath;
    public bool[] localClearInfo=new bool[13];
    public bool[][] localPlacedInfo = new bool[2][]{ new bool[6], new bool[2] };
    public bool[] localCompleteInfo = new bool[2];
    public int localWorldInfo;

    int lastStage = 13;
    
    // bool[] clear=new bool[1];
    void Awake()
    {
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
        
    }
    //Application.persistentDataPath + "/clearInfo.json" 위치에 12개짜리, 모두 false인 거 만들기.
    public void makeD()
    {     
       
        clearData cleardata= new clearData();
        for(int i = 0; i < lastStage; i++)
        {         
            cleardata.clear[i]  = false;
        }
        for(int i=0;i<cleardata.placed.Length;i++)
        {
            for(int j=0;j<cleardata.placed[i].Length;j++)
            {
                cleardata.placed[i][j] = false;
            }
        }
        cleardata.complete[0]=false; cleardata.complete[1]=false;
        cleardata.world = 0;

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
        //읽어온 아이템정보를 localPlacedInfo에 복사
        for(int i=0;i<localPlacedInfo.Length;i++)
        {
            for(int j=0;j<localPlacedInfo[i].Length;j++)
            {
                localPlacedInfo[i][j]=cleardata.placed[i][j];
            }
        }
        //읽어온 월드complete정보와 마지막으로 머물렀던 월드 정보 복사
        localCompleteInfo[0]=cleardata.complete[0]; localCompleteInfo[1]=cleardata.complete[1];
        localWorldInfo = cleardata.world;




    }
    public void Save()
    {
        clearData cleardata = new clearData();

        //게임 플레이하면서 localClearInfo 변수를 조작, 세이브할때 localClearInfo 정보를 cleardata.data에 넣고 파일에 쓴다.
        for (int i = 0; i < lastStage; i++)
        {
            cleardata.clear[i] = localClearInfo[i];
        }
        for(int i=0;i<cleardata.placed.Length;i++)
        {
            for(int j=0;j<cleardata.placed[i].Length;j++)
            {
                cleardata.placed[i][j] = localPlacedInfo[i][j];
            }
        }
        cleardata.complete[0] = localCompleteInfo[0]; cleardata.complete[1] = localCompleteInfo[1];
        cleardata.world = localWorldInfo;


        File.WriteAllText(Application.persistentDataPath + "/clearInfo.json", JsonUtility.ToJson(cleardata));
    }
    
    //퍼즐 씬에서 클리어 조건 맞추면 호출되는 함수.
    public void updateClear(int n)
    {
       // Debug.Log("updateClear 호출됨"+n);        
        localClearInfo[n] =true;
      
        Save();
    }

    //홈화면에서 아이템을 배치할 경우 호출되는 함수
    public void updatePlaced(int n)
    {
        localPlacedInfo[localWorldInfo][n] = true;

        Save();
    }
    
}
