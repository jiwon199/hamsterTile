using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
public class Board : MonoBehaviour
{
    //점프타일 로직+ 뒤로가기 로직 구현해야됨
    string[] colorPos;
    //색깔 코드를 저장해 놓은 배열-빨강-파랑-초록-주황-보라
    //색상팔레트rgb값에 각각 /255f 해줘야 제대로 된 값.     
    private Color [] color   =  { Color.red, Color.blue, new Color(11 / 255f, 201 / 255f, 4 / 255f, 1), new Color(255 / 255f, 205 / 255f, 18 / 255f, 1), new Color(165 / 255f, 102 / 255f, 255 / 255f, 1) };

    //아이템 주는 스테이지 번호를 저장한 배열
    int[] itemStage ;    

    int[] itemvil = { 1, 3, 5, 7, 9, 12, 15, 18, 21, 24 }; //10개
    int[] itemcity = { 1, 3, 5, 8, 12, 16, 20, 24, 28, 32, 36, 40 }; //12개 

    
    //1,3,5,7,9,12,15,18,21,24
    //퍼즐 구성에 쓰이는 변수들
    public int m_Width;
    public  int m_Height;
     int colorNum;
    string start;
    string end;
    public int ColorSeq = -1;
    int jumpNum;
    string[] jumpPos;

    //딕셔너리 =  key값과(값을 찾기 위한 열쇠) 와value로 이루어진 자료구조
    //Tile이 된 이유는 타일 하나에 붙는 스크립트 이름이 Tile.cs이기 때문에. 다른 딕셔너리도 마찬가지.
    public Dictionary<string, Tile> m_TilesDictionary = new Dictionary<string, Tile>();
    private Dictionary<int, SeqTile> m_SeqTilesDictionary = new Dictionary<int, SeqTile>();
     

    //터치 순서 저장 리스트-뒤로가기 구현시 필요
    public List<string> touchList = new List<string>();
    //색타일 터치 순서 저장리스트
    List<string> ColorTouchList = new List<string>();
    private GameObject tilePrefab;
    private GameObject SeqTilePrefab;
    public Data data;

    //믹스타일 위치정보 저장할 리스트 
    private Dictionary<string, mixTile> m_mixDictionary = new Dictionary<string, mixTile>();

    //끝타일 터치시 결과를 팝업으로..
    //일반 팝업 
    public GameObject background;
    public GameObject popup;
    public Text result;
    public GameObject nextBtn;
    public GameObject retryBtn;
    public GameObject revokeBtn;

    //아이템팝업
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

    string failReason; //실패이유
    public Text failreason;

    public AudioClip sucessBGM; //
    //타일설명
    public GameObject guideTile;

    bool isPlaying;
    void Start()
    {

        isPlaying = true;
        StageManager = GameObject.Find("stagenum");
        nowStage = stagenum.stageNum;  //지금 어떤 레벨의 스테이지를 가져와야하는지 찾는다.
        //테스트플레이시 스테이지 선택씬에서 넘어오지 않고 퍼즐씬을 실행한경우, 널 에러를 피하기 위해 1번 스테이지로 자동 설정
        if (nowStage == 0) nowStage = 1;
 
        //json 데이터로부터 스테이지 정보를 가져온다.
        TextAsset textData;
        //string str = File.ReadAllText(Application.dataPath + "/stageInfo.json");          
        showLev.text = "level "+nowStage.ToString();
        if (GameManager.instance.localWorldInfo == 0)
        {
            textData = Resources.Load("puzzle/stageInfo") as TextAsset;  //햄스빌리지 스테이지
            lastStage = 24;
            itemStage = itemvil;
            showvil.text = "햄스빌리지-";
            guideTile.GetComponent<Image>().sprite = Resources.Load<Sprite>("puzzle/vilGuide");

        }

        else
        {
            textData = Resources.Load("puzzle/stageInfo2") as TextAsset;   //햄시티 스테이지
            lastStage = 40;
            itemStage = itemcity;
            showvil.text = "햄시티-";
            guideTile.GetComponent<Image>().sprite = Resources.Load<Sprite>("puzzle/cityGuide");
        }


        data = JsonUtility.FromJson<Data>(textData.ToString());
       // Debug.Log(data.stageInfo[0].level);

        //스테이지 레벨에 맞게 변수 설정
        m_Width = data.stageInfo[nowStage - 1].width;
        m_Height = data.stageInfo[nowStage - 1].height;
         colorNum = data.stageInfo[nowStage - 1].colorNum;
        start = data.stageInfo[nowStage - 1].start;
        end = data.stageInfo[nowStage - 1].end;
        colorPos= data.stageInfo[nowStage - 1].colorPos;
        jumpNum = data.stageInfo[nowStage - 1].jumpNum;
        jumpPos = data.stageInfo[nowStage - 1].jumpPos;


        //프리팹을 가져와서(로드해서) 변수설정
        tilePrefab = Resources.Load("Prefabs/tile") as GameObject;
        SeqTilePrefab = Resources.Load("Prefabs/SeqTile") as GameObject;
        guideText = FindObjectOfType<GuideText>();
       
        //위 변수들을 바탕으로 타일 생성 
        CreateTile();
        CreateSeqTile();
        guideText.createSeqText(colorNum);
       
        //이전에 깬 스테이지라면 true, 아니면 false    
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
       
        //끝타일을 터치
       if (m_TilesDictionary[end].touched)
        {
            isPlaying = false;
            //클리어 여부 판단-클리어한 경우
            if (checkClear())
            {
                //퍼즐씬에서 바로 시작하면 null에러가 나지만, 실제 플레이시에는 무조건 스테이지 씬을 거쳐야 하므로..
                if (GameManager.instance == null) return;            
                result.text = "성공!";
            
                // 마지막 레벨이면 다음레벨 버튼 대신 다시하기 버튼 활성화.
                if (nowStage == lastStage)
                {                                        
                    retryBtn.SetActive(true);
                    itemRetryBtn.SetActive(true);
                    //마지막 레벨 외에는 다시하기 버튼에 실패브금이 달려있으므로... 성공+마지막레벨 조합이면 오디오클립을 바꿔준다.
                    AudioSource audioSource = retryBtn.GetComponent<AudioSource>();
                    audioSource.clip = sucessBGM;
                }
                else //마지막 레벨이 아니면 다음 레벨로 버튼 활성화.
                {
                    nextBtn.SetActive(true);
                    itemNextBtn.SetActive(true);
                }
                //아이템 얻는 스테이지면 아이템팝업활성화
                if (itemOrNot())
                {
                    itempopup.SetActive(true);
                    setItemPopup(); //아이템 이미지,이름 세팅

                }
                else  //일반스테이지면 일반팝업 활성화
                {
                    popup.SetActive(true);
                }
                //클리어정보-해당 스테이지를 트루로
                GameManager.instance.updateClear(nowStage - 1);
            }
            //클리어 못한 경우.
            else {
               
                result.text = "실패!";
                retryBtn.SetActive(true);
                popup.SetActive(true);
                failreason.text = failReason;
            }

            //다른 버튼들의 터치를 막기
            background.SetActive(true);
         
        }
       //터치한타일이 하나도 없을땐 뒤로가기 버튼 막기
        if (touchList.Count == 0) revokeBtn.GetComponent<Button>().interactable = false;
        else { revokeBtn.GetComponent<Button>().interactable = true; }

    }
    //게임종료상황인지 아닌지 확인
     public bool getIsPlaying()
    {
        return isPlaying;
    }
    //아이템 주는 스테이지인지 아닌지 확인
    private bool itemOrNot()
    {
        
        bool check = false;
        for(int i = 0; i < itemStage.Length; i++)
        {          
            //아이템 주는 스테이지여도, 이미 깬 스테이지를 다시 깨는 상황이면 아이템 팝업을 띄위지 않는다.
            if (itemStage[i] == nowStage&&!secondTry ) check = true; 
          
        }
        return check;
    } 
    //아이템 팝업의 이미지 세팅
    public void setItemPopup()
    {
        string[] ResourceList;
        string[] ItemList;
        
        if (GameManager.instance.localWorldInfo == 0) //빌리지
        {         
            string [] vilResourceList = { "leaves_forIcon", "1_tile_forIcon","2_parasol","fountain","4_bench","wheel","6_sunflower","7_lamp_foricon","8_sellingStand" ,"hammock"};
            string[] vilItemList = { "[나뭇잎]", "[바닥 타일]","[파라솔]","[분수]","[벤치]","[쳇바퀴]","[해바라기]","[도토리 가로등]","[판매대]","[해먹]" };
             
            ResourceList = vilResourceList;
            ItemList = vilItemList;
        }
        else   //시티
        {
            string[] cityResourceList = { "0_Icon_bridge_f", "1_ICON_building", "2_ICON_cafe", "3_ICON_swing", "clock_p", "5_ICON_flowerPot", "6seesaw_Icon", "7boat_Icon", "8_ICON_slide", "9_parasol_city", "ICON_streetLamp_p", "11cleaningHam_Icon" };
            string[] cityitemList = { "[다리]", "[건물]", "[카페]", "[그네]", "[시계]", "[화분]", "[시소]", "[보트]", "[미끄럼틀]", "[파라솔]" , "[가로등]" , "[청소부 햄아저씨]" };
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
                itempopup.transform.GetChild(5).GetComponent<Text>().text= ItemList[i]; //아이템 이름 설정
                itempopup.transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>(resourceName); //이미지 바꾸기
            }

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
           
            colorTouch(key);
        }
        //추가하기 전에 가장 최신이었던 애의 테두리를 없애고 
        if(touchList.Count!=0) m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosF();
        //터치리스트에 추가
        touchList.Add(key);
        //새로 갱신된 마지막터치 타일에 테두리
        m_TilesDictionary[touchList[touchList.Count-1]].setLastPosT();   


    }
    //밖에서도 부를스 있게...
   public void colorTouch(string key)
    {
        ColorSeq++;   //이 변수는 순서에 상관없이 색타일을 터치했다면 무조건 증가
        ColorTouchList.Add(key);  //순서에 상관없이 색타일 터치 리스트에 추가
        //그게 순서에 맞는 색타일이었으면
        if (checkColorSeq())
        {
            this.rightAudio.Play();
            m_SeqTilesDictionary[ColorSeq].touched = true;
            m_SeqTilesDictionary[ColorSeq].touchTime = Time.deltaTime;
        }
        else
        {
            //순서틀렸으면 메세지 
            guideText.addMessage("순서가 틀렸다찌!");
            this.errorAudio.Play();
        }
    }
    //점프타일같은경우...터치상태는 false여도 한번은 터치한상태이므로, 따로 함수를 호출해서 터치리스트에 넣는다.
    public void putTileInTouchList(string key)
    {
          //딱 한번만 들어가게 조절하기
        if(!touchList[touchList.Count - 1].Equals(key)) //점프위치의 타일이 연속으로 터치될 수 없고, 시작타일이 점프속성을 가질수 없으므로
        {
            m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosF();
            touchList.Add(key);
            m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosT();
        }
        
    }
    //뒤로가기시, 마지막 터치 위치를 갱신하고 색깔 되돌리기
   public void renewPos(string key)
    {
        //색깔 바꿔주기. touchList.Count가 1이면 리스트 터지므로 따로 빼준다.
        if (touchList.Count != 1)
        {
            if (getMix(key) &&  touchJumpCount(key) == 1)//믹스타일을 1번터치-두번터치인경우
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
    //뒤로가기
    public void revokeTouch()
    {
        
        //마지막으로 터치한 타일의 키를 구해서
        string key = touchList[touchList.Count - 1];
        
        if (!getJump(key))
        {
            //터치를 false로  
            m_TilesDictionary[key].touched = false;
            renewPos(key);
        }
        else   //취소한애가 점프타일위치에 있는 애면
        {
            if (m_TilesDictionary[key].touched) // 2번터치->1번터치
            {                 
                m_TilesDictionary[key].touched = false;  //터치를 false로
                renewPos(key);   

            }
            else  //1번터치->0번터치 (점프타일 되돌리기)
            {            
                m_TilesDictionary[key].transform.GetChild(0).gameObject.SetActive(true);
                m_TilesDictionary[key].touched = false;
                m_TilesDictionary[key].jumpTile = true;                
                renewPos(key);
                 

            }
        }
        //테두리를 없애주고
        m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosF(); 
        //터치리스트에서 삭제
        touchList.RemoveAt(touchList.Count - 1);
        //새로운 마지막터치타일에 테두리
       if(touchList.Count !=0) m_TilesDictionary[touchList[touchList.Count - 1]].setLastPosT();

        //취소한애가 색타일이었으면  ColorSeq--
        if (getTouchCol(key))
        {
            m_SeqTilesDictionary[ColorSeq].gameObject.SetActive(true);
            m_SeqTilesDictionary[ColorSeq].touched = false; 
            ColorSeq--;
            ColorTouchList.RemoveAt(ColorTouchList.Count - 1);            
            //취소로 인해 색타일 터치순서가 올바르게 되었으면 경고 메세지 없앰
            if (checkColorSeq()) guideText.deleteMessage("순서가 틀렸다찌!");
        }
        //취소한 색에 해당되는 쌓인타일이 없어졌는지 안없어졌는지 체크-없어졌던애면 다시만들기 
             

    }
    //터치리스트에서 인자로 받은 키가 터치리스트에 몇개 있는지 확인-점프타일전용
    public int touchJumpCount(string key)
    {
        int check = 0;
        for(int i = 0; i < touchList.Count; i++)
        {
            if (key.Equals(touchList[i])) check++;
        }
        
        return check;
    }
     
    //색타일터치 순서가 맞는지 확인
     public bool checkColorSeq()
    {
        bool check = true;         
        for (int i = 0; i <= ColorSeq; i++)
        {
            if (!ColorTouchList[i].Equals(colorPos[i].Substring(0, 3))) check = false;
        }
        //Debug.Log(check + " 리턴");
        return check;
    }
    public void SetReset(string key)
    {
        m_TilesDictionary[key].touched = false;
    }
    
    //색타일 터치 여부 확인 함수 
    public bool getTouchCol(string key)
    {
        for(int i=0;i< colorPos.Length; i++)
        {
            if (key.Equals(colorPos[i])){
                return true;
            }
            //믹스인 경우
            else if (key.Equals(colorPos[i].Substring(0, 3)))
            {
                return true;
            } 
        }
    
        return false;
    }
    //믹스 여부 판단. 일반 색타일은 false 반환
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
   
    public mixTile getmixCol(string key)
    {
        return m_mixDictionary[key];
    }
    //초기 타일 컬러링 세팅
    //색배열은 인덱스-색을 정해놓자. 0빨강 1파랑 등..
    //num은 색깔 갯수가 몇개인지..
    public void SetColoring( )
    {
         
        //start end 변수로 바꾸기    
        m_TilesDictionary[start].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("puzzle/starttile", typeof(Sprite));
        m_TilesDictionary[end].GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("puzzle/endtile", typeof(Sprite));

        for (int i = 0; i < colorNum; i++)
        {
            if (colorPos[i].Length == 3)//일반 색타일+두번째 믹스
            {
                m_TilesDictionary[colorPos[i]].GetComponent<Renderer>().material.color = color[i];
            }
            else  //믹스타일의 경우
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
        //여기서 점프타일도 정해놓기
        for(int i = 0; i < jumpNum; i++)
        {        
            GameObject child = m_TilesDictionary[jumpPos[i]].transform.GetChild(0).gameObject;
            child.SetActive(true);
            child.GetComponent<SpriteRenderer>().sortingOrder = 1;

        }

    }
    //점프타일인지 여부 체크
    public bool getJump(string key)
    {
       bool check = false;
       for(int i = 0; i < jumpNum; i++)
        {
            if (key.Equals(jumpPos[i])) check = true;
        }
        
        return check;
        
    }
    //점프타일 첫번째 터치-테스트용
    public void setJumpFirstTouch()
    {
        jumpPos[0] = "0";
    }
    //클리어 여부 체크 
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
        if (!check) failReason = "터치하지 않은 타일이 있다찌..";
        //터치 순서가 맞았는지 체크
        for(int i=0;i< colorPos.Length; i++)
        {       
            if (!m_SeqTilesDictionary[i].touched) { check = false;            
            }
        }
        if (!check&&failReason.Equals("")) failReason = "터치 순서가 틀렸다찌..";
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
