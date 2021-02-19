using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
public class Board : MonoBehaviour
{
    //����Ÿ�� ����+ �ڷΰ��� ���� �����ؾߵ�
    string[] colorPos;
    //���� �ڵ带 ������ ���� �迭-����-�Ķ�-�ʷ�-��Ȳ-����
    //�����ȷ�Ʈrgb���� ���� /255f ����� ����� �� ��.     
    private Color [] color   =  { Color.red, Color.blue, new Color(11 / 255f, 201 / 255f, 4 / 255f, 1), new Color(255 / 255f, 205 / 255f, 18 / 255f, 1), new Color(165 / 255f, 102 / 255f, 255 / 255f, 1) };

    //������ �ִ� �������� ��ȣ�� ������ �迭
    int[] itemStage ;    

    int[] itemvil = { 1, 3, 5, 7, 9, 12, 15, 18, 21, 24 }; //10��
    int[] itemcity = { 1, 3, 5, 8, 12, 16, 20, 24, 28, 32, 36, 40 }; //12�� 

    
    //1,3,5,7,9,12,15,18,21,24
    //���� ������ ���̴� ������
    public int m_Width;
    public  int m_Height;
     int colorNum;
    string start;
    string end;
    public int ColorSeq = -1;
    int jumpNum;
    string[] jumpPos;

    //��ųʸ� =  key����(���� ã�� ���� ����) ��value�� �̷���� �ڷᱸ��
    //Tile�� �� ������ Ÿ�� �ϳ��� �ٴ� ��ũ��Ʈ �̸��� Tile.cs�̱� ������. �ٸ� ��ųʸ��� ��������.
    public Dictionary<string, Tile> m_TilesDictionary = new Dictionary<string, Tile>();
    private Dictionary<int, SeqTile> m_SeqTilesDictionary = new Dictionary<int, SeqTile>();
     

    //��ġ ���� ���� ����Ʈ-�ڷΰ��� ������ �ʿ�
    public List<string> touchList = new List<string>();
    //��Ÿ�� ��ġ ���� ���帮��Ʈ
    List<string> ColorTouchList = new List<string>();
    private GameObject tilePrefab;
    private GameObject SeqTilePrefab;
    public Data data;

    //�ͽ�Ÿ�� ��ġ���� ������ ����Ʈ 
    private Dictionary<string, mixTile> m_mixDictionary = new Dictionary<string, mixTile>();

    //��Ÿ�� ��ġ�� ����� �˾�����..
    //�Ϲ� �˾� 
    public GameObject background;
    public GameObject popup;
    public Text result;
    public GameObject nextBtn;
    public GameObject retryBtn;
    public GameObject revokeBtn;

    //�������˾�
    public GameObject itempopup;
    public GameObject itemNextBtn;
    public GameObject itemRetryBtn;
    int lastStage ;

    GuideText guideText;
    GameObject StageManager;
    int nowStage;
    bool secondTry;

    public Text showvil;
    public Text showLev;

     AudioSource errorAudio;
    public AudioClip errorSound;
    AudioSource rightAudio;
    public AudioClip rightSound;

    string failReason; //��������
    public Text failreason;

    public AudioClip sucessBGM; //
    //Ÿ�ϼ���
    public GameObject guideTile;

    bool isPlaying;
    void Start()
    {

        isPlaying = true;
        StageManager = GameObject.Find("stagenum");
        nowStage = stagenum.stageNum;  //���� � ������ ���������� �����;��ϴ��� ã�´�.
        //�׽�Ʈ�÷��̽� �������� ���þ����� �Ѿ���� �ʰ� ������� �����Ѱ��, �� ������ ���ϱ� ���� 1�� ���������� �ڵ� ����
        if (nowStage == 0) nowStage = 1;
 
        //json �����ͷκ��� �������� ������ �����´�.
        TextAsset textData;
        //string str = File.ReadAllText(Application.dataPath + "/stageInfo.json");          
        showLev.text = "level "+nowStage.ToString();
        if (GameManager.instance.localWorldInfo == 0)
        {
            textData = Resources.Load("puzzle/stageInfo") as TextAsset;  //�ܽ������� ��������
            lastStage = 24;
            itemStage = itemvil;
            showvil.text = "�ܽ�������-";
            guideTile.GetComponent<Image>().sprite = Resources.Load<Sprite>("puzzle/vilGuide");

        }

        else
        {
            textData = Resources.Load("puzzle/stageInfo2") as TextAsset;   //�ܽ�Ƽ ��������
            lastStage = 40;
            itemStage = itemcity;
            showvil.text = "�ܽ�Ƽ-";
            guideTile.GetComponent<Image>().sprite = Resources.Load<Sprite>("puzzle/cityGuide");
        }


        data = JsonUtility.FromJson<Data>(textData.ToString());
       // Debug.Log(data.stageInfo[0].level);

        //�������� ������ �°� ���� ����
        m_Width = data.stageInfo[nowStage - 1].width;
        m_Height = data.stageInfo[nowStage - 1].height;
         colorNum = data.stageInfo[nowStage - 1].colorNum;
        start = data.stageInfo[nowStage - 1].start;
        end = data.stageInfo[nowStage - 1].end;
        colorPos= data.stageInfo[nowStage - 1].colorPos;
        jumpNum = data.stageInfo[nowStage - 1].jumpNum;
        jumpPos = data.stageInfo[nowStage - 1].jumpPos;


        //�������� �����ͼ�(�ε��ؼ�) ��������
        tilePrefab = Resources.Load("Prefabs/tile") as GameObject;
        SeqTilePrefab = Resources.Load("Prefabs/SeqTile") as GameObject;
        guideText = FindObjectOfType<GuideText>();
       
        //�� �������� �������� Ÿ�� ���� 
        CreateTile();
        CreateSeqTile();
        guideText.createSeqText(colorNum);
       
        //������ �� ����������� true, �ƴϸ� false    
        if (GameManager.instance == null) return;

        if(GameManager.instance.localWorldInfo==0)
            secondTry = GameManager.instance.localClearInfo[nowStage - 1];
        else
            secondTry = GameManager.instance.localClearInfo2[nowStage - 1];

        this.errorAudio = this.gameObject.AddComponent<AudioSource>();
        this.errorAudio.clip = this.errorSound;
        this.errorAudio.loop = false;
        this.errorAudio.volume = 0.3f;

        this.rightAudio = this.gameObject.AddComponent<AudioSource>();
        this.rightAudio.clip = this.rightSound;
        this.rightAudio.loop = false;

        failReason = "";


    }
    void Update()
    {
       
        //��Ÿ���� ��ġ
       if (m_TilesDictionary[end].touched)
        {
            isPlaying = false;
            //Ŭ���� ���� �Ǵ�-Ŭ������ ���
            if (checkClear())
            {
                //��������� �ٷ� �����ϸ� null������ ������, ���� �÷��̽ÿ��� ������ �������� ���� ���ľ� �ϹǷ�..
                if (GameManager.instance == null) return;            
                result.text = "����!";
            
                // ������ �����̸� �������� ��ư ��� �ٽ��ϱ� ��ư Ȱ��ȭ.
                if (nowStage == lastStage)
                {                                        
                    retryBtn.SetActive(true);
                    itemRetryBtn.SetActive(true);
                    //������ ���� �ܿ��� �ٽ��ϱ� ��ư�� ���к���� �޷������Ƿ�... ����+���������� �����̸� �����Ŭ���� �ٲ��ش�.
                    AudioSource audioSource = retryBtn.GetComponent<AudioSource>();
                    audioSource.clip = sucessBGM;
                }
                else //������ ������ �ƴϸ� ���� ������ ��ư Ȱ��ȭ.
                {
                    nextBtn.SetActive(true);
                    itemNextBtn.SetActive(true);
                }
                //������ ��� ���������� �������˾�Ȱ��ȭ
                if (itemOrNot())
                {
                    itempopup.SetActive(true);
                    setItemPopup(); //������ �̹���,�̸� ����

                }
                else  //�Ϲݽ��������� �Ϲ��˾� Ȱ��ȭ
                {
                    popup.SetActive(true);
                }
                //Ŭ��������-�ش� ���������� Ʈ���
                GameManager.instance.updateClear(nowStage - 1);
            }
            //Ŭ���� ���� ���.
            else {
               
                result.text = "����!";
                retryBtn.SetActive(true);
                popup.SetActive(true);
                failreason.text = failReason;
            }

            //�ٸ� ��ư���� ��ġ�� ����
            background.SetActive(true);
         
        }
       //��ġ��Ÿ���� �ϳ��� ������ �ڷΰ��� ��ư ����
        if (touchList.Count == 0) revokeBtn.GetComponent<Button>().interactable = false;
        else { revokeBtn.GetComponent<Button>().interactable = true; }

    }
    //���������Ȳ���� �ƴ��� Ȯ��
     public bool getIsPlaying()
    {
        return isPlaying;
    }
    //������ �ִ� ������������ �ƴ��� Ȯ��
    private bool itemOrNot()
    {
        
        bool check = false;
        for(int i = 0; i < itemStage.Length; i++)
        {          
            //������ �ִ� ������������, �̹� �� ���������� �ٽ� ���� ��Ȳ�̸� ������ �˾��� ������ �ʴ´�.
            if (itemStage[i] == nowStage&&!secondTry ) check = true; 
          
        }
        return check;
    } 
    //������ �˾��� �̹��� ����
    public void setItemPopup()
    {
        string[] ResourceList;
        string[] ItemList;
        
        if (GameManager.instance.localWorldInfo == 0) //������
        {         
            string [] vilResourceList = { "leaves_forIcon", "1_tile_forIcon","2_parasol","fountain","4_bench","wheel","6_sunflower","7_lamp_foricon","8_sellingStand" ,"hammock"};
            string[] vilItemList = { "[������]", "[�ٴ� Ÿ��]","[�Ķ��]","[�м�]","[��ġ]","[�¹���]","[�عٶ��]","[���丮 ���ε�]","[�ǸŴ�]","[�ظ�]" };
             
            ResourceList = vilResourceList;
            ItemList = vilItemList;
        }
        else   //��Ƽ
        {
            string[] cityResourceList = { "0_Icon_bridge_f", "1_ICON_building", "2_ICON_cafe", "3_ICON_swing", "clock_p", "5_ICON_flowerPot", "6seesaw_Icon", "7boat_Icon", "8_ICON_slide", "9_parasol_city", "ICON_streetLamp_p", "11cleaningHam_Icon" };
            string[] cityitemList = { "[�ٸ�]", "[�ǹ�]", "[ī��]", "[�׳�]", "[�ð�]", "[ȭ��]", "[�ü�]", "[��Ʈ]", "[�̲���Ʋ]", "[�Ķ��]" , "[���ε�]" , "[û�Һ� �ܾ�����]" };
            ResourceList = cityResourceList;
            ItemList = cityitemList;
        }
        for (int i = 0; i < itemStage.Length; i++)
        {
            if (itemStage[i] == nowStage)
            {
                string resourceName;
                if (GameManager.instance.localWorldInfo == 0) resourceName = "Village/"+ResourceList[i];
                else resourceName = "City/" + ResourceList[i];
                itempopup.transform.GetChild(5).GetComponent<Text>().text= ItemList[i]; //������ �̸� ����
                itempopup.transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>(resourceName); //�̹��� �ٲٱ�
            }

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
           
            colorTouch(key);
        }
        //�߰��ϱ� ���� ���� �ֽ��̾��� ���� �׵θ��� ���ְ� 
        if(touchList.Count!=0) m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosF();
        //��ġ����Ʈ�� �߰�
        touchList.Add(key);
        //���� ���ŵ� ��������ġ Ÿ�Ͽ� �׵θ�
        m_TilesDictionary[touchList[touchList.Count-1]].setLastPosT();   


    }
    //�ۿ����� �θ��� �ְ�...
   public void colorTouch(string key)
    {
        ColorSeq++;   //�� ������ ������ ������� ��Ÿ���� ��ġ�ߴٸ� ������ ����
        ColorTouchList.Add(key);  //������ ������� ��Ÿ�� ��ġ ����Ʈ�� �߰�
        //�װ� ������ �´� ��Ÿ���̾�����
        if (checkColorSeq())
        {
            this.rightAudio.Play();
            m_SeqTilesDictionary[ColorSeq].touched = true;
            m_SeqTilesDictionary[ColorSeq].touchTime = Time.deltaTime;
        }
        else
        {
            //����Ʋ������ �޼��� 
            guideText.addMessage("������ Ʋ�ȴ���!");
            this.errorAudio.Play();
        }
    }
    //����Ÿ�ϰ������...��ġ���´� false���� �ѹ��� ��ġ�ѻ����̹Ƿ�, ���� �Լ��� ȣ���ؼ� ��ġ����Ʈ�� �ִ´�.
    public void putTileInTouchList(string key)
    {
          //�� �ѹ��� ���� �����ϱ�
        if(!touchList[touchList.Count - 1].Equals(key)) //������ġ�� Ÿ���� �������� ��ġ�� �� ����, ����Ÿ���� �����Ӽ��� ������ �����Ƿ�
        {
            m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosF();
            touchList.Add(key);
            m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosT();
        }
        
    }
    //�ڷΰ����, ������ ��ġ ��ġ�� �����ϰ� ���� �ǵ�����
   public void renewPos(string key)
    {
        //���� �ٲ��ֱ�. touchList.Count�� 1�̸� ����Ʈ �����Ƿ� ���� ���ش�.
        if (touchList.Count != 1)
        {
            if (getMix(key) &&  touchJumpCount(key) == 1)//�ͽ�Ÿ���� 1����ġ-�ι���ġ�ΰ��
            {
                m_TilesDictionary[key].resetHalfColor(key);
            }
            else
            {
                m_TilesDictionary[key].resetColor(int.Parse(touchList[touchList.Count - 2].Substring(0, 1)), int.Parse(touchList[touchList.Count - 2].Substring(2, 1)));
            }
             
        }       
        else
        {          
            m_TilesDictionary[key].resetColor();
        }

    }
    //�ڷΰ���
    public void revokeTouch()
    {
        
        //���������� ��ġ�� Ÿ���� Ű�� ���ؼ�
        string key = touchList[touchList.Count - 1];
        
        if (!getJump(key))
        {
            //��ġ�� false��  
            m_TilesDictionary[key].touched = false;
            renewPos(key);
        }
        else   //����Ѿְ� ����Ÿ����ġ�� �ִ� �ָ�
        {
            if (m_TilesDictionary[key].touched) // 2����ġ->1����ġ
            {                 
                m_TilesDictionary[key].touched = false;  //��ġ�� false��
                renewPos(key);   

            }
            else  //1����ġ->0����ġ (����Ÿ�� �ǵ�����)
            {            
                m_TilesDictionary[key].transform.GetChild(0).gameObject.SetActive(true);
                m_TilesDictionary[key].touched = false;
                m_TilesDictionary[key].jumpTile = true;                
                renewPos(key);
                 

            }
        }
        //�׵θ��� �����ְ�
        m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosF(); 
        //��ġ����Ʈ���� ����
        touchList.RemoveAt(touchList.Count - 1);
        //���ο� ��������ġŸ�Ͽ� �׵θ�
       if(touchList.Count !=0) m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosT();

        //����Ѿְ� ��Ÿ���̾�����  ColorSeq--
        if (getTouchCol(key))
        {
            m_SeqTilesDictionary[ColorSeq].gameObject.SetActive(true);
            m_SeqTilesDictionary[ColorSeq].touched = false; 
            ColorSeq--;
            ColorTouchList.RemoveAt(ColorTouchList.Count - 1);            
            //��ҷ� ���� ��Ÿ�� ��ġ������ �ùٸ��� �Ǿ����� ��� �޼��� ����
            if (checkColorSeq()) guideText.deleteMessage("������ Ʋ�ȴ���!");
        }
        //����� ���� �ش�Ǵ� ����Ÿ���� ���������� �Ⱦ��������� üũ-���������ָ� �ٽø���� 
             

    }
    //��ġ����Ʈ���� ���ڷ� ���� Ű�� ��ġ����Ʈ�� � �ִ��� Ȯ��-����Ÿ������
    public int touchJumpCount(string key)
    {
        int check = 0;
        for(int i = 0; i < touchList.Count; i++)
        {
            if (key.Equals(touchList[i])) check++;
        }
        
        return check;
    }
     
    //��Ÿ����ġ ������ �´��� Ȯ��
     public bool checkColorSeq()
    {
        bool check = true;         
        for (int i = 0; i <= ColorSeq; i++)
        {
            if (!ColorTouchList[i].Equals(colorPos[i].Substring(0, 3))) check = false;
        }
        //Debug.Log(check + " ����");
        return check;
    }
    public void SetReset(string key)
    {
        m_TilesDictionary[key].touched = false;
    }
    
    //��Ÿ�� ��ġ ���� Ȯ�� �Լ� 
    public bool getTouchCol(string key)
    {
        for(int i=0;i< colorPos.Length; i++)
        {
            if (key.Equals(colorPos[i])){
                return true;
            }
            //�ͽ��� ���
            else if (key.Equals(colorPos[i].Substring(0, 3)))
            {
                return true;
            } 
        }
    
        return false;
    }
    //�ͽ� ���� �Ǵ�. �Ϲ� ��Ÿ���� false ��ȯ
    public bool getMix(string key)
    {
        for (int i = 0; i < colorPos.Length; i++)
        {
                     
            if (colorPos[i].Length!=3&&key.Equals(colorPos[i].Substring(0, 3)))
            {
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
   
    public mixTile getmixCol(string key)
    {
        return m_mixDictionary[key];
    }
    //�ʱ� Ÿ�� �÷��� ����
    //���迭�� �ε���-���� ���س���. 0���� 1�Ķ� ��..
    //num�� ���� ������ �����..
    public void SetColoring( )
    {
         
        //start end ������ �ٲٱ�    
        m_TilesDictionary[start].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("puzzle/starttile", typeof(Sprite));
        m_TilesDictionary[end].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("puzzle/endtile", typeof(Sprite));

        for (int i = 0; i < colorNum; i++)
        {
            if (colorPos[i].Length == 3)//�Ϲ� ��Ÿ��+�ι�° �ͽ�
            {
                m_TilesDictionary[colorPos[i]].GetComponent<Renderer>().material.color = color[i];
            }
            else  //�ͽ�Ÿ���� ���
            {
                 
                string pos=colorPos[i].Substring(0, 3);
                string upOrDown= colorPos[i].Substring(4, 1);
                 
                if (upOrDown.Equals("0"))
                {
                    GameObject child = m_TilesDictionary[pos].transform.GetChild(1).gameObject;
                    child.SetActive(true);
                    child.GetComponent<Renderer>().material.color = color[i];
                }
                else
                {
                    m_TilesDictionary[pos].GetComponent<Renderer>().material.color = color[i];
                }
                 
            }
             
        }
        //���⼭ ����Ÿ�ϵ� ���س���
        for(int i = 0; i < jumpNum; i++)
        {        
            GameObject child = m_TilesDictionary[jumpPos[i]].transform.GetChild(0).gameObject;
            child.SetActive(true);
            child.GetComponent<SpriteRenderer>().sortingOrder = 1;

        }

    }
    //����Ÿ������ ���� üũ
    public bool getJump(string key)
    {
       bool check = false;
       for(int i = 0; i < jumpNum; i++)
        {
            if (key.Equals(jumpPos[i])) check = true;
        }
        
        return check;
        
    }
    //����Ÿ�� ù��° ��ġ-�׽�Ʈ��
    public void setJumpFirstTouch()
    {
        jumpPos[0] = "0";
    }
    //Ŭ���� ���� üũ 
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
        if (!check) failReason = "��ġ���� ���� Ÿ���� �ִ���..";
        //��ġ ������ �¾Ҵ��� üũ
        for(int i=0;i< colorPos.Length; i++)
        {       
            if (!m_SeqTilesDictionary[i].touched) { check = false;            
            }
        }
        if (!check&&failReason.Equals("")) failReason = "��ġ ������ Ʋ�ȴ���..";
        return check;
    }
    
    private void CreateSeqTile()
    {
        for (int i = 0; i <colorNum; i++)
        {
             
            SeqTile seqtile = Instantiate<SeqTile>(SeqTilePrefab.transform.GetComponent<SeqTile>());
            seqtile.transform.SetParent(this.transform);
            seqtile.transform.position = new Vector3((float)m_Width/2.0f-0.5f, m_Height+ ((float)0.25 * (float)i), 0f);
            m_SeqTilesDictionary.Add(i, seqtile);
            m_SeqTilesDictionary[i].GetComponent<Renderer>().material.color = color[i];
          //  m_SeqTilesDictionary[i].GetComponent<Renderer>().sortingOrder = i;
            
        }

    }
     
    
}
