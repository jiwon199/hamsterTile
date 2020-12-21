using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
public class Board : MonoBehaviour
{
    
    string[] colorPos; 
    private Color [] color   =  { Color.red, Color.blue, new Color(0, 1, 1, 1) }; //색깔 코드를 저장해 놓은 배열

     
    //이 퍼블릭 변수들은 값 변경할때 유니티 내의 board에서도 편집해줘야
     int m_Width;
      int m_Height;
     int colorNum;
    string start;
    string end;
    public int ColorSeq = -1;

    //딕셔너리 =  key값과(값을 찾기 위한 열쇠) 와value로 이루어진 자료구조
    //Tile이 된 이유는 타일 하나에 붙는 스크립트 이름이 Tile.cs이기 때문에. 두번째 딕셔너리도 마찬가지.
    private Dictionary<string, Tile> m_TilesDictionary = new Dictionary<string, Tile>();
    private Dictionary<int, SeqTile> m_SeqTilesDictionary = new Dictionary<int, SeqTile>();
    
    private GameObject tilePrefab;
    private GameObject SeqTilePrefab;
    public Data data;

    GuideText guideText;
    GameObject StageManager;
    int nowStage;
    void Start()
    {      
        StageManager = GameObject.Find("stagenum");
        nowStage = stagenum.stageNum;  //지금 어떤 레벨의 스테이지를 가져와야하는지 찾는다.
        //테스트플레이시 스테이지 선택씬에서 넘어오지 않고 퍼즐씬을 실행한경우, 널 에러를 피하기 위해 1번 스테이지로 자동 설정
        if (nowStage == 0) nowStage = 1;

        //json 데이터로부터 스테이지 정보를 가져온다.

        //string str = File.ReadAllText(Application.dataPath + "/stageInfo.json");       
        TextAsset textData= Resources.Load("stageInfo") as TextAsset;
        data = JsonUtility.FromJson<Data>(textData.ToString());
        Debug.Log(data.stageInfo[0].level);

        //스테이지 레벨에 맞게 변수 설정
        m_Width = data.stageInfo[nowStage - 1].width;
        m_Height = data.stageInfo[nowStage - 1].height;
         colorNum = data.stageInfo[nowStage - 1].colorNum;
        start = data.stageInfo[nowStage - 1].start;
        end = data.stageInfo[nowStage - 1].end;
        colorPos= data.stageInfo[nowStage - 1].colorPos;

        //프리팹을 가져와서(로드해서) 변수설정
        tilePrefab = Resources.Load("Prefabs/tile") as GameObject;
        SeqTilePrefab = Resources.Load("Prefabs/SeqTile") as GameObject;
        guideText = FindObjectOfType<GuideText>();
       
        //위 변수들을 바탕으로 타일 생성 
        CreateTile();
        CreateSeqTile();
     
        guideText.createSeqText(colorNum);
       // Debug.Log(nowStage + " 스테이지");


    }
    void Update()
    {
       
        //checkTile();
        if (checkClear())
        {         
           // GameManager.instance.localClearInfo[nowStage] = true;
             GameManager.instance.updateClear(nowStage-1);
          
        }
    }

    //특정위치에 타일 생성
    private void CreateTile()
    {

        for (int y = 0; y < m_Height; y++)
        {
            for (int x = 0; x < m_Width; x++)
            {
                string key = x.ToString() + "," + y.ToString();
                Tile tile = Instantiate<Tile>(tilePrefab.transform.GetComponent<Tile>());
                tile.transform.SetParent(this.transform);
                tile.transform.position = new Vector3(x, y, 0f);  
                m_TilesDictionary.Add(key, tile);
            }
        }

    }
    public string getStart()
    {
        return start;
    }
    //x,y 위치에 있는 타일을 가져온다.(다른 클래스에서 부를 수 있게)
    public Tile GetTile(int x, int y)
    {
        string key = x.ToString() + "," + y.ToString();
        return m_TilesDictionary[key];
    }
    //인자 다르게 해서 하나 더
    public Tile GetTile(string xy)
    {
        return m_TilesDictionary[xy];
    }
    //각 타일들이 터치가 됐는지 관리는 board에서...타일이 터치됐으면 터치를 트루로
    public void SetChecked(string key)
    {
        m_TilesDictionary[key].touched = true;

        //터치한 타일이 색깔타일
        if (getTouchCol(key))
        {
            ColorSeq++;   //이 변수는 순서에 상관없이 색타일을 터치했다면 무조건 증가..

            //그게 순서에 맞는 색타일이었으면
            if (key.Equals(colorPos[ColorSeq]))  
            {
                m_SeqTilesDictionary[ColorSeq].touched = true;
            }
            else
            { 
                //뒤로가기 기능을 만들지 않는 이상, 없앨수 없는 메세지...
                guideText.addMessage("순서가 틀렸다찌!");
            }
        }
        
    }
    //색타일 터치 여부 확인 함수
    public bool getTouchCol(string key)
    {
        for(int i=0;i< colorPos.Length; i++)
        {
            if (key.Equals(colorPos[i])){
                return true;
            }
        }
        return false;
    }
    public bool getChecked(string key)
    {
        return m_TilesDictionary[key].touched ;
    }
    //타일 하나도 터치 안한 상태면 true
    public bool getStartOrNot()
    {
        bool check = true;
        for (int y = 0; y < m_Height; y++)
        {
            for (int x = 0; x < m_Width; x++)
            {
                string key = x.ToString() + "," + y.ToString();
                if (m_TilesDictionary[key].touched) check = false;
            }
        }
        return check;
    }
    //초기 타일 컬러링 세팅
    //색배열은 인덱스-색을 정해놓자. 0빨강 1파랑 등..
    //num은 색깔 갯수가 몇개인지..
    public void SetColoring( )
    {
        //start end 변수로 바꾸기
    
        m_TilesDictionary[start].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("starttile", typeof(Sprite));
        m_TilesDictionary[end].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("endtile", typeof(Sprite));
        for (int i = 0; i < colorNum; i++)
        {
            m_TilesDictionary[colorPos[i]].GetComponent<Renderer>().material.color = color[i];
        }

    }
    //클리어 여부 체크++팝업창 띄우는 기능도 코딩해야됨
    public bool checkClear()
    {
        bool check = true;
        //하나라도 터치 안된 타일이 있으면 false
        for (int y = 0; y < m_Height; y++)
        {
            for (int x = 0; x < m_Width; x++)
            {
                string key = x.ToString() + "," + y.ToString();
                if (!m_TilesDictionary[key].touched) check = false;
            }
        }
        //터치 순서가 맞았는지 체크
        for(int i=0;i< colorPos.Length; i++)
        {       
            if (!m_SeqTilesDictionary[i].touched) { check = false;            
            }
        }
        return check;
    }
    
    private void CreateSeqTile()
    {
        for (int i = 0; i <colorNum; i++)
        {
             
            SeqTile seqtile = Instantiate<SeqTile>(SeqTilePrefab.transform.GetComponent<SeqTile>());
            seqtile.transform.SetParent(this.transform);
            seqtile.transform.position = new Vector3(1, m_Height+ ((float)0.25 * (float)i), 0f);
            m_SeqTilesDictionary.Add(i, seqtile);
            m_SeqTilesDictionary[i].GetComponent<Renderer>().material.color = color[i];
          //  m_SeqTilesDictionary[i].GetComponent<Renderer>().sortingOrder = i;
            
        }

    }
    
}
