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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        //  instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        filePath = Application.persistentDataPath + "/clearInfo.json";
        //�������� Ŭ���� ��ŭ�ߴ���..���� �ε�
        Load();
              
    }
    void Update()
    {
        
    }
    //Application.persistentDataPath + "/clearInfo.json" ��ġ�� 12��¥��, ��� false�� �� �����.
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
        //������ ������ �ش���ġ�� �����.(ù�÷��̽�)
        if (!File.Exists(filePath)) { makeD();   }

         string str = File.ReadAllText(Application.persistentDataPath + "/clearInfo.json");
         clearData cleardata = JsonUtility.FromJson<clearData>(str);

        //�о�� Ŭ���������� localClearInfo��
        for (int i = 0; i < 12; i++)
        {
            localClearInfo[i] = cleardata.clear[i];
             
        }

    }
    public void Save()
    {
        clearData cleardata = new clearData();

        //���� �÷����ϸ鼭 localClearInfo ������ ����, ���̺��Ҷ� localClearInfo ������ cleardata.data�� �ְ� ���Ͽ� ����.
        for (int i = 0; i < 12; i++)
        {
            cleardata.clear[i] = localClearInfo[i];
        }
        File.WriteAllText(Application.persistentDataPath + "/clearInfo.json", JsonUtility.ToJson(cleardata));
    }
    //�׽�Ʈ�����ϰ��ϱ�����.. Ŭ�������� ���� �Լ�.
     public void resetClear()
    {
        Debug.Log("ȣ���");
        for (int i = 1; i <=12; i++)
        {
            localClearInfo[i-1]=false;
            GameObject btn = GameObject.Find(i.ToString());
            if (btn == null) Debug.Log("��ư�� ��");
            Image img = btn.GetComponent<Image>();
            btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("btn") as Sprite;
        }
        Save();
    }
    //���� ������ Ŭ���� ���� ���߸� ȣ��Ǵ� �Լ�.
    public  void updateClear(int n)
    {
        Debug.Log("updateClear ȣ���"+n);        
        localClearInfo[n] =true;
      
        Save();
    }
    
}
