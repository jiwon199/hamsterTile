using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class stage
{
    //mixTile좌표,어떤 두 색이 섞였는지 추가 정보가 필요함. 도라버리겟다...
    public int level;   //스테이지 레벨
    public int width;  //가로 몇칸인지..
    public int height; //세로 몇칸인지..
    public int colorNum;  //사용된 색타일의 수
    public string start;  //시작타일 좌표
    public string end;  //끝타일 좌표
    public string[] colorPos;  //색타일 좌표

    //믹스타일은 컬러넘에 포함시키면 안될거같은데? 조정을 어케하지
    /*
    public int mixNum; //믹스타일 수
    public string[] mixPos;
    public string[] mixCol;
    */
    public int jumpNum; //점프타일 갯수
    public string[] jumpPos; //점프타일 좌표

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

        //data 오브젝트를 json형식으로 스트링에 넣는다.
        string str = JsonUtility.ToJson(data);
        Debug.Log("ToJson : " + str);

        //  json형식의 데이터를 다시 오브젝트형으로 변환.
        Data data2 = JsonUtility.FromJson<Data>(str);
        data2.printData();

        // 저장하기
        Data data3 = new Data();
        data3.m_nLevel = 99;
        data3.m_vecPositon = new Vector3(8.1f, 9.2f, 7.2f);
        File.WriteAllText(Application.dataPath + "/stageInfo.json", JsonUtility.ToJson(data3));
        */

        //로드하기
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
