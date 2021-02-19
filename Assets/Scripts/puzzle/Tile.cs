using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour
{
    
    //색
    //위치 좌표
    //터치여부
    public bool touched;
    //고유한 id 값
    public int m_ID;
    Board board;
    SpriteRenderer render;
    string start;
    string end;
   // public SpriteOutline s;
    public static Vector3 Lastpos;  //마지막으로 터치한 타일의 위치좌표
    GuideText guideText;
    //해당 if문은 한번만 실행되게..
    public bool jumpTile = true;

    public bool mixTouch = false;
   
    List<string> mixKeyList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
       
        touched = false;
        // board = gameObject.AddComponent<Board>();
        board = FindObjectOfType<Board>();
        guideText = FindObjectOfType<GuideText>();

        board.SetColoring( );
        
        // Unit m_Unit = gameObject.AddComponent("Marine") as Unit;
         
        //Lastpos.x =2 ; Lastpos.y = 2;
    }

    // Update is called once per frame
    void Update()
    {
        touchTile();

        
           
    }
    //믹스타일의 위치 파악 
   // void findMixKey()
   // {
   //     board.colorPos[];
   // }
    //색 칠할수 있는지 체크 함수 
    bool touchAble(Vector3 pos,GameObject touchedObject )
    {
       
        if (board.getStartOrNot())
        {
            string start = board.getStart();
            int x= int.Parse(start.Substring(0, 1));
            int y= int.Parse(start.Substring(2, 1));
            if (pos.x == x && pos.y == y) {
                guideText.deleteMessage("첫 터치는 시작점부터다찌!");
                return true; }
            else {
                guideText.addMessage("첫 터치는 시작점부터다찌!");
                return false;
            }
           
        }
        //초기상태가 아닐경우
        else {
             
            string key = pos.x.ToString()+ "," + pos.y.ToString();
      
      //한번 터치한 타일을 중복터치 불가능
        if (board.getChecked(key))
            {           
                return false; 
            }
         else if (board.getJump(key)&&board.touchJumpCount(key)==0) {  //점프타일 위치이고, 점프속성을 가지고 있는 상태이면
                if (miniTouchAble(pos, touchedObject))
                {
                    Lastpos = pos;  //라스트포지션을 해당위치로 갱신
                    jumpTile = false;  //이제 더이상 점프타일 속성을 갖지 않음...

                }
                return false;
            }
         
        //
         else if(board.getMix(key)&& board.touchJumpCount(key) == 0)
            {
                if(miniTouchAble(pos, touchedObject))
                {
                    Lastpos = pos; //위치갱신               
                    board.colorTouch(key);
                    board.putTileInTouchList(key);
                    GameObject child = board.m_TilesDictionary[key].transform.GetChild(1).gameObject;
                    render = child.GetComponent<SpriteRenderer>();
                    Color color;
                    ColorUtility.TryParseHtmlString("#A6A6A6", out color);
                    render.color = color;

                   
                }
                return false;

            }
           
            else
            {
                 
                //가장 마지막으로 터치한 타일의 위, 아래, 옆만 터치 가능. 
                if ((Lastpos.x == pos.x - 1) && (Lastpos.y == pos.y)) { return true; }
                else if ((Lastpos.x == pos.x + 1) && (Lastpos.y == pos.y)) { return true; }
                else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y - 1)) { return true; }
                else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y + 1)) { return true; }              
                else {
                    //믹스타일에서 똑같은거 연속터치했을때 경고 안뜨게 if문
                    if (board.touchJumpCount(key)==0) guideText.addMessage("터치가 이어져야 한다찌!");
                   
                    return false;
                }   
            }
     
        }
    }
    bool miniTouchAble(Vector3 pos, GameObject touchedObject)
    {
        string key = pos.x.ToString() + "," + pos.y.ToString();
        //가장 마지막으로 터치한 타일의 위, 아래, 옆만 터치 가능. 
        if ((Lastpos.x == pos.x - 1) && (Lastpos.y == pos.y)) { return true; }
        else if ((Lastpos.x == pos.x + 1) && (Lastpos.y == pos.y)) { return true; }
        else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y - 1)) { return true; }
        else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y + 1)) { return true; }
        else
        {
            //믹스타일에서 똑같은거 연속터치했을때 경고 안뜨게 if문
            if (board.touchJumpCount(key) == 0) guideText.addMessage("터치가 이어져야 한다찌!");

            return false;
        }
    }
    //색칠 함수
    void touchTile()
    {
        
        //버튼을 누르고 있으면
        if (Input.GetMouseButton(0))
        {
            //게임종료 상황이 아니면(종료시 터치 막힘)
          if (!EventSystem.current.IsPointerOverGameObject()&& board.getIsPlaying())
            {
            //어떤 오브젝트를 터치했는지 알아내서 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);  
            if (hitInformation.collider != null )
            {
                GameObject touchedObject = hitInformation.transform.gameObject;
                Vector3 pos = touchedObject.transform.position;
                
                string key = pos.x.ToString() + "," + pos.y.ToString();
                
                if (touchAble(pos,touchedObject)) {    
                    guideText.deleteMessage("터치가 이어져야 한다찌!");
                                            
                            //색깔 칠해주기   
                            render = touchedObject.GetComponent<SpriteRenderer>();
                            Color color;
                            ColorUtility.TryParseHtmlString("#A6A6A6", out color);
                             
                            render.color = color;
                            Lastpos = touchedObject.transform.position;
                            board.SetChecked(key);
                         
                  
               }
             
                
            }
            }
        }
         
    }
     
    //터치취소-색 되돌리기
    public void resetColor(int newx, int newy)
    {
        SpriteRenderer render = this.GetComponent<SpriteRenderer>();
        Color color;
        ColorUtility.TryParseHtmlString("#fffff", out color);      
        render.color = color;
        Lastpos.x = newx;
        Lastpos.y = newy;
    }
    public void resetColor()
    {
        SpriteRenderer render = this.GetComponent<SpriteRenderer>();
        Color color;
        ColorUtility.TryParseHtmlString("#fffff", out color);
        render.color = color;
       
    }
    public void resetHalfColor(string key)
    {
        GameObject child = board.m_TilesDictionary[key].transform.GetChild(1).gameObject;
        render = child.GetComponent<SpriteRenderer>();
        Color color;
        ColorUtility.TryParseHtmlString("#fffff", out color);
        render.color = color;
    }

    public void setLastPosT()
    {
       // s.lastTouch = true;
       this.transform.GetChild(2).gameObject.SetActive(true);
        Lastpos = this.transform.position;
    }
    public void setLastPosF()
    {
        // s.lastTouch = false;
        this.transform.GetChild(2).gameObject.SetActive(false);
    }
}
