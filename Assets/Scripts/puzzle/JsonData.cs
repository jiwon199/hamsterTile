using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class stage
{
    public int level;   //�������� ����
    public int width;  //���� ��ĭ����..
    public int height; //���� ��ĭ����..
    public int colorNum;  //���� ��Ÿ���� ��
    public string start;  //����Ÿ�� ��ǥ
    public string end;  //��Ÿ�� ��ǥ
    public string[] colorPos;  //��Ÿ�� ��ǥ

}
 
[System.Serializable]
public class Data
{  
      public stage [] stageInfo;
}
public class JsonData : MonoBehaviour
{
     
    // Start is called before the first frame update
    void Start()
    {
        /*
        Data data = new Data();
        data.m_nLevel = 12;
        data.m_vecPositon = new Vector3(3.4f, 5.6f, 7.8f);

        //data ������Ʈ�� json�������� ��Ʈ���� �ִ´�.
        string str = JsonUtility.ToJson(data);
        Debug.Log("ToJson : " + str);

        //  json������ �����͸� �ٽ� ������Ʈ������ ��ȯ.
        Data data2 = JsonUtility.FromJson<Data>(str);
        data2.printData();

        // �����ϱ�
        Data data3 = new Data();
        data3.m_nLevel = 99;
        data3.m_vecPositon = new Vector3(8.1f, 9.2f, 7.2f);
        File.WriteAllText(Application.dataPath + "/stageInfo.json", JsonUtility.ToJson(data3));
        */

        //�ε��ϱ�
        // file load 

        //  string str = File.ReadAllText(Application.dataPath + "/stageInfo.json");
        // Data data = JsonUtility.FromJson<Data>(str);

        //  Debug.Log(data.stageInfo[0].clear);


         


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
