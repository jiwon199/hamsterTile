using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour
{
    
    //��
    //��ġ ��ǥ
    //��ġ����
    public bool touched;
    //������ id ��
    public int m_ID;
    Board board;
    SpriteRenderer render;
    string start;
    string end;
   // public SpriteOutline s;
    public static Vector3 Lastpos;  //���������� ��ġ�� Ÿ���� ��ġ��ǥ
    GuideText guideText;
    //�ش� if���� �ѹ��� ����ǰ�..
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
    //�ͽ�Ÿ���� ��ġ �ľ� 
   // void findMixKey()
   // {
   //     board.colorPos[];
   // }
    //�� ĥ�Ҽ� �ִ��� üũ �Լ� 
    bool touchAble(Vector3 pos,GameObject touchedObject )
    {
       
        if (board.getStartOrNot())
        {
            string start = board.getStart();
            int x= int.Parse(start.Substring(0, 1));
            int y= int.Parse(start.Substring(2, 1));
            if (pos.x == x && pos.y == y) {
                guideText.deleteMessage("ù ��ġ�� ���������ʹ���!");
                return true; }
            else {
                guideText.addMessage("ù ��ġ�� ���������ʹ���!");
                return false;
            }
           
        }
        //�ʱ���°� �ƴҰ��
        else {
             
            string key = pos.x.ToString()+ "," + pos.y.ToString();
      
      //�ѹ� ��ġ�� Ÿ���� �ߺ���ġ �Ұ���
        if (board.getChecked(key))
            {           
                return false; 
            }
         else if (board.getJump(key)&&board.touchJumpCount(key)==0) {  //����Ÿ�� ��ġ�̰�, �����Ӽ��� ������ �ִ� �����̸�
                if (miniTouchAble(pos, touchedObject))
                {
                    Lastpos = pos;  //��Ʈ�������� �ش���ġ�� ����
                    jumpTile = false;  //���� ���̻� ����Ÿ�� �Ӽ��� ���� ����...

                }
                return false;
            }
         
        //
         else if(board.getMix(key)&& board.touchJumpCount(key) == 0)
            {
                if(miniTouchAble(pos, touchedObject))
                {
                    Lastpos = pos; //��ġ����               
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
                 
                //���� ���������� ��ġ�� Ÿ���� ��, �Ʒ�, ���� ��ġ ����. 
                if ((Lastpos.x == pos.x - 1) && (Lastpos.y == pos.y)) { return true; }
                else if ((Lastpos.x == pos.x + 1) && (Lastpos.y == pos.y)) { return true; }
                else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y - 1)) { return true; }
                else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y + 1)) { return true; }              
                else {
                    //�ͽ�Ÿ�Ͽ��� �Ȱ����� ������ġ������ ��� �ȶ߰� if��
                    if (board.touchJumpCount(key)==0) guideText.addMessage("��ġ�� �̾����� �Ѵ���!");
                   
                    return false;
                }   
            }
     
        }
    }
    bool miniTouchAble(Vector3 pos, GameObject touchedObject)
    {
        string key = pos.x.ToString() + "," + pos.y.ToString();
        //���� ���������� ��ġ�� Ÿ���� ��, �Ʒ�, ���� ��ġ ����. 
        if ((Lastpos.x == pos.x - 1) && (Lastpos.y == pos.y)) { return true; }
        else if ((Lastpos.x == pos.x + 1) && (Lastpos.y == pos.y)) { return true; }
        else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y - 1)) { return true; }
        else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y + 1)) { return true; }
        else
        {
            //�ͽ�Ÿ�Ͽ��� �Ȱ����� ������ġ������ ��� �ȶ߰� if��
            if (board.touchJumpCount(key) == 0) guideText.addMessage("��ġ�� �̾����� �Ѵ���!");

            return false;
        }
    }
    //��ĥ �Լ�
    void touchTile()
    {
        
        //��ư�� ������ ������
        if (Input.GetMouseButton(0))
        {
            //�������� ��Ȳ�� �ƴϸ�(����� ��ġ ����)
          if (!EventSystem.current.IsPointerOverGameObject()&& board.getIsPlaying())
            {
            //� ������Ʈ�� ��ġ�ߴ��� �˾Ƴ��� 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);  
            if (hitInformation.collider != null )
            {
                GameObject touchedObject = hitInformation.transform.gameObject;
                Vector3 pos = touchedObject.transform.position;
                
                string key = pos.x.ToString() + "," + pos.y.ToString();
                
                if (touchAble(pos,touchedObject)) {    
                    guideText.deleteMessage("��ġ�� �̾����� �Ѵ���!");
                                            
                            //���� ĥ���ֱ�   
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
     
    //��ġ���-�� �ǵ�����
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
