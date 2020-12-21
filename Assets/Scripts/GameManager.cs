using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

 
[System.Serializable]
public class clearData
{
    public bool[] clear=new bool[12];
}

//
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    string filePath;
    public bool[] localClearInfo=new bool[12];

    
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
         for(int i = 0; i < 12; i++)
        {         
            cleardata.clear[i]  = false;
        }
        File.WriteAllText(Application.persistentDataPath + "/clearInfo.json", JsonUtility.ToJson(cleardata));
    }
    public void Load()
    {
        //파일이 없으면 해당위치에 만든다.(첫플레이시)
        if (!File.Exists(filePath)) { makeD();   }

         string str = File.ReadAllText(Application.persistentDataPath + "/clearInfo.json");
         clearData cleardata = JsonUtility.FromJson<clearData>(str);

        //읽어온 클리어정보를 localClearInfo에
        for (int i = 0; i < 12; i++)
        {
            localClearInfo[i] = cleardata.clear[i];
             
        }

    }
    public void Save()
    {
        clearData cleardata = new clearData();

        //게임 플레이하면서 localClearInfo 변수를 조작, 세이브할때 localClearInfo 정보를 cleardata.data에 넣고 파일에 쓴다.
        for (int i = 0; i < 12; i++)
        {
            cleardata.clear[i] = localClearInfo[i];
        }
        File.WriteAllText(Application.persistentDataPath + "/clearInfo.json", JsonUtility.ToJson(cleardata));
    }
    //테스트원할하게하기위한.. 클리어정보 리셋 함수.
     public void resetClear()
    {
        Debug.Log("호출됨");
        for (int i = 1; i <=12; i++)
        {
            localClearInfo[i-1]=false;
            GameObject btn = GameObject.Find(i.ToString());
            if (btn == null) Debug.Log("버튼이 널");
            Image img = btn.GetComponent<Image>();
            btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("btn") as Sprite;
        }
        Save();
    }
    //퍼즐 씬에서 클리어 조건 맞추면 호출되는 함수.
    public  void updateClear(int n)
    {
        Debug.Log("updateClear 호출됨"+n);        
        localClearInfo[n] =true;
      
        Save();
    }
    
}
