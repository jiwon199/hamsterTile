using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour
{
    //IPointerDownHandler,IPointerUpHandler,IDragHandler 유아이들만 되는듯?
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
    public static Vector3 Lastpos;
    GuideText guideText;

    // Start is called before the first frame update
    void Start()
    {
       
        touched = false;
        // board = gameObject.AddComponent<Board>();
        board = FindObjectOfType<Board>();
        guideText = FindObjectOfType<GuideText>();

        board.SetColoring( );
        // Unit m_Unit = gameObject.AddComponent("Marine") as Unit;

        //처음에는 start 타일의 위치
        Lastpos.x =2 ; Lastpos.y = 2;
    }

    // Update is called once per frame
    void Update()
    {
        touchTile();
         
    }
    //색 칠할수 있는지 체크 함수 
    bool touchAble(Vector3 pos )
    {
       
        //초기상태일 경우 지정된 start 타일만 터치 가능(이경우 2,2)  //이부분 수정해야됨!
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
        else
            {
                Debug.Log(Lastpos.x + " " + Lastpos.y);

                //가장 마지막으로 터치한 타일의 위, 아래, 옆만 터치 가능. 
                if ((Lastpos.x == pos.x - 1) && (Lastpos.y == pos.y)) { return true; }
                else if ((Lastpos.x == pos.x + 1) && (Lastpos.y == pos.y)) { return true; }
                else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y - 1)) { return true; }
                else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y + 1)) { return true; }
                else { guideText.addMessage("룰을 지켜라찌!"); return false; }
            }
     
        }
    }
    //색칠 함수
    void touchTile()
    {
        
        //버튼을 누르고 있으면
        if (Input.GetMouseButton(0))
        {          
            //어떤 오브젝트를 터치했는지 알아내서 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);  
            if (hitInformation.collider != null )
            {
                GameObject touchedObject = hitInformation.transform.gameObject;
                Vector3 pos = touchedObject.transform.position;
                
                string key = pos.x.ToString() + "," + pos.y.ToString();
                if (touchAble(pos) ) {   //일단 이게 있어도 업데이트에 Lastpos 제대로 뜨게 하는걸 목표로 삼자.
                    guideText.deleteMessage("룰을 지켜라찌!");
                //색깔 칠해주기   
                render = touchedObject.GetComponent<SpriteRenderer>();   
                Color color;
                ColorUtility.TryParseHtmlString("#C9C9C9", out color);
                //render.color = new Color(0, 1, 1, 1); //마지막 숫자가 투명도
                render.color = color;

                Lastpos = touchedObject.transform.position;
                   
                board.SetChecked(key);
                    //마지막 타일 터치했으면 더이상 터치 못하게 막는거 구현해야 함            
                    if (key.Equals(end)) Debug.Log(board.checkClear());
               }
            }
        }

        
        
        

    }



}
