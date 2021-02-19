using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class jumpIcon : MonoBehaviour
{
    Board board;
    GuideText guideText;
    
    [SerializeField] private GameObject bangObj;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        guideText = FindObjectOfType<GuideText>();
 
    }

    // Update is called once per frame
    void Update()
    {
        touchJumpTile();
    }
    bool touchAble(Vector3 pos)
    {
        string key = pos.x.ToString() + "," + pos.y.ToString();
        if (board.getChecked(key))
        {
            return false;
        }
        //������ ��ġ����Ʈ�� ������ ������ ��ġ Ÿ�� ��ġ�� �˾Ƴ�
        string s= board.touchList[board.touchList.Count - 1];     
        int x = int.Parse(s.Substring(0, 1));
        int y = int.Parse(s.Substring(2, 1));
        //��ġ ���� ��ġ�� �ִ��� �Ǵ�.
        if ((x == pos.x - 1) && (y == pos.y)) { return true; }
        else if ((x == pos.x + 1) && (y == pos.y)) { return true; }
        else if ((x == pos.x) && (y == pos.y - 1)) { return true; }
        else if ((x == pos.x) && (y == pos.y + 1)) { return true; }
        else {  return false; }
    }
    void touchJumpTile()
    {
        //��ư�� ������ ������
        if (Input.GetMouseButton(0))
        {
            //�������� ��Ȳ�� �ƴϸ�(����� ��ġ ����)
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //� ������Ʈ�� ��ġ�ߴ��� �˾Ƴ��� 
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
                if (hitInformation.collider != null)
                {
                    GameObject touchedObject = hitInformation.transform.gameObject;
                    Vector3 pos = touchedObject.transform.position;

                    string key = pos.x.ToString() + "," + pos.y.ToString();
                    if (board.getJump(key)&&touchAble(pos))
                    {
                       // bangObj.transform.localScale = new Vector3(0.6f, 0.6f, 0);
                        Instantiate(bangObj, pos, Quaternion.identity);
                        // bangObj.transform.localScale = new Vector3(1f, 1f, 0);
                        guideText.deleteMessage("��ġ�� �̾����� �Ѵ���!");
                        touchedObject.transform.GetChild(0).gameObject.SetActive(false);
                        board.putTileInTouchList(key);  //ù��°��ġ�� �Ϸ�� ���̹Ƿ� ��ġ����Ʈ�� �־��ش�. 
                    }
                    

                }
            }
        }
    }
}
