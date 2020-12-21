using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
public class Board : MonoBehaviour
{
    
    string[] colorPos; 
    private Color [] color   =  { Color.red, Color.blue, new Color(0, 1, 1, 1) }; //���� �ڵ带 ������ ���� �迭

     
    //�� �ۺ� �������� �� �����Ҷ� ����Ƽ ���� board������ ���������
     int m_Width;
      int m_Height;
     int colorNum;
    string start;
    string end;
    public int ColorSeq = -1;

    //��ųʸ� =  key����(���� ã�� ���� ����) ��value�� �̷���� �ڷᱸ��
    //Tile�� �� ������ Ÿ�� �ϳ��� �ٴ� ��ũ��Ʈ �̸��� Tile.cs�̱� ������. �ι�° ��ųʸ��� ��������.
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
        nowStage = stagenum.stageNum;  //���� � ������ ���������� �����;��ϴ��� ã�´�.
        //�׽�Ʈ�÷��̽� �������� ���þ����� �Ѿ���� �ʰ� ������� �����Ѱ��, �� ������ ���ϱ� ���� 1�� ���������� �ڵ� ����
        if (nowStage == 0) nowStage = 1;

        //json �����ͷκ��� �������� ������ �����´�.

        //string str = File.ReadAllText(Application.dataPath + "/stageInfo.json");       
        TextAsset textData= Resources.Load("stageInfo") as TextAsset;
        data = JsonUtility.FromJson<Data>(textData.ToString());
        Debug.Log(data.stageInfo[0].level);

        //�������� ������ �°� ���� ����
        m_Width = data.stageInfo[nowStage - 1].width;
        m_Height = data.stageInfo[nowStage - 1].height;
         colorNum = data.stageInfo[nowStage - 1].colorNum;
        start = data.stageInfo[nowStage - 1].start;
        end = data.stageInfo[nowStage - 1].end;
        colorPos= data.stageInfo[nowStage - 1].colorPos;

        //�������� �����ͼ�(�ε��ؼ�) ��������
        tilePrefab = Resources.Load("Prefabs/tile") as GameObject;
        SeqTilePrefab = Resources.Load("Prefabs/SeqTile") as GameObject;
        guideText = FindObjectOfType<GuideText>();
       
        //�� �������� �������� Ÿ�� ���� 
        CreateTile();
        CreateSeqTile();
     
        guideText.createSeqText(colorNum);
       // Debug.Log(nowStage + " ��������");


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

    //Ư����ġ�� Ÿ�� ����
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
    //x,y ��ġ�� �ִ� Ÿ���� �����´�.(�ٸ� Ŭ�������� �θ� �� �ְ�)
    public Tile GetTile(int x, int y)
    {
        string key = x.ToString() + "," + y.ToString();
        return m_TilesDictionary[key];
    }
    //���� �ٸ��� �ؼ� �ϳ� ��
    public Tile GetTile(string xy)
    {
        return m_TilesDictionary[xy];
    }
    //�� Ÿ�ϵ��� ��ġ�� �ƴ��� ������ board����...Ÿ���� ��ġ������ ��ġ�� Ʈ���
    public void SetChecked(string key)
    {
        m_TilesDictionary[key].touched = true;

        //��ġ�� Ÿ���� ����Ÿ��
        if (getTouchCol(key))
        {
            ColorSeq++;   //�� ������ ������ ������� ��Ÿ���� ��ġ�ߴٸ� ������ ����..

            //�װ� ������ �´� ��Ÿ���̾�����
            if (key.Equals(colorPos[ColorSeq]))  
            {
                m_SeqTilesDictionary[ColorSeq].touched = true;
            }
            else
            { 
                //�ڷΰ��� ����� ������ �ʴ� �̻�, ���ټ� ���� �޼���...
                guideText.addMessage("������ Ʋ�ȴ���!");
            }
        }
        
    }
    //��Ÿ�� ��ġ ���� Ȯ�� �Լ�
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
    //Ÿ�� �ϳ��� ��ġ ���� ���¸� true
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
    //�ʱ� Ÿ�� �÷��� ����
    //���迭�� �ε���-���� ���س���. 0���� 1�Ķ� ��..
    //num�� ���� ������ �����..
    public void SetColoring( )
    {
        //start end ������ �ٲٱ�
    
        m_TilesDictionary[start].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("starttile", typeof(Sprite));
        m_TilesDictionary[end].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("endtile", typeof(Sprite));
        for (int i = 0; i < colorNum; i++)
        {
            m_TilesDictionary[colorPos[i]].GetComponent<Renderer>().material.color = color[i];
        }

    }
    //Ŭ���� ���� üũ++�˾�â ���� ��ɵ� �ڵ��ؾߵ�
    public bool checkClear()
    {
        bool check = true;
        //�ϳ��� ��ġ �ȵ� Ÿ���� ������ false
        for (int y = 0; y < m_Height; y++)
        {
            for (int x = 0; x < m_Width; x++)
            {
                string key = x.ToString() + "," + y.ToString();
                if (!m_TilesDictionary[key].touched) check = false;
            }
        }
        //��ġ ������ �¾Ҵ��� üũ
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
