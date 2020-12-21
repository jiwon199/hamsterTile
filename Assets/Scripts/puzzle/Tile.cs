using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour
{
    //IPointerDownHandler,IPointerUpHandler,IDragHandler �����̵鸸 �Ǵµ�?
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

        //ó������ start Ÿ���� ��ġ
        Lastpos.x =2 ; Lastpos.y = 2;
    }

    // Update is called once per frame
    void Update()
    {
        touchTile();
         
    }
    //�� ĥ�Ҽ� �ִ��� üũ �Լ� 
    bool touchAble(Vector3 pos )
    {
       
        //�ʱ������ ��� ������ start Ÿ�ϸ� ��ġ ����(�̰�� 2,2)  //�̺κ� �����ؾߵ�!
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
        else
            {
                Debug.Log(Lastpos.x + " " + Lastpos.y);

                //���� ���������� ��ġ�� Ÿ���� ��, �Ʒ�, ���� ��ġ ����. 
                if ((Lastpos.x == pos.x - 1) && (Lastpos.y == pos.y)) { return true; }
                else if ((Lastpos.x == pos.x + 1) && (Lastpos.y == pos.y)) { return true; }
                else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y - 1)) { return true; }
                else if ((Lastpos.x == pos.x) && (Lastpos.y == pos.y + 1)) { return true; }
                else { guideText.addMessage("���� ���Ѷ���!"); return false; }
            }
     
        }
    }
    //��ĥ �Լ�
    void touchTile()
    {
        
        //��ư�� ������ ������
        if (Input.GetMouseButton(0))
        {          
            //� ������Ʈ�� ��ġ�ߴ��� �˾Ƴ��� 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);  
            if (hitInformation.collider != null )
            {
                GameObject touchedObject = hitInformation.transform.gameObject;
                Vector3 pos = touchedObject.transform.position;
                
                string key = pos.x.ToString() + "," + pos.y.ToString();
                if (touchAble(pos) ) {   //�ϴ� �̰� �־ ������Ʈ�� Lastpos ����� �߰� �ϴ°� ��ǥ�� ����.
                    guideText.deleteMessage("���� ���Ѷ���!");
                //���� ĥ���ֱ�   
                render = touchedObject.GetComponent<SpriteRenderer>();   
                Color color;
                ColorUtility.TryParseHtmlString("#C9C9C9", out color);
                //render.color = new Color(0, 1, 1, 1); //������ ���ڰ� ����
                render.color = color;

                Lastpos = touchedObject.transform.position;
                   
                board.SetChecked(key);
                    //������ Ÿ�� ��ġ������ ���̻� ��ġ ���ϰ� ���°� �����ؾ� ��            
                    if (key.Equals(end)) Debug.Log(board.checkClear());
               }
            }
        }

        
        
        

    }



}
