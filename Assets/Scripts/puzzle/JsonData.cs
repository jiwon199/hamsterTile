using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class stage
{
    //mixTile좌표,어떤 두 색이 섞였는지 추가 정보가 필요함.  
    public int level;   //스테이지 레벨
    public int width;  //가로 몇칸인지..
    public int height; //세로 몇칸인지..
    public int colorNum;  //사용된 색타일의 수
    public string start;  //시작타일 좌표
    public string end;  //끝타일 좌표
    public string[] colorPos;  //색타일 좌표

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
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
