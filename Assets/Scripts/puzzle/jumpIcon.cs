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
        //보드의 터치리스트에 접근해 마지막 터치 타일 위치를 알아냄
        string s= board.touchList[board.touchList.Count - 1];     
        int x = int.Parse(s.Substring(0, 1));
        int y = int.Parse(s.Substring(2, 1));
        //터치 가능 위치에 있는지 판단.
        if ((x == pos.x - 1) && (y == pos.y)) { return true; }
        else if ((x == pos.x + 1) && (y == pos.y)) { return true; }
        else if ((x == pos.x) && (y == pos.y - 1)) { return true; }
        else if ((x == pos.x) && (y == pos.y + 1)) { return true; }
        else {  return false; }
    }
    void touchJumpTile()
    {
        //버튼을 누르고 있으면
        if (Input.GetMouseButton(0))
        {
            //게임종료 상황이 아니면(종료시 터치 막힘)
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //어떤 오브젝트를 터치했는지 알아내서 
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
                        guideText.deleteMessage("터치가 이어져야 한다찌!");
                        touchedObject.transform.GetChild(0).gameObject.SetActive(false);
                        board.putTileInTouchList(key);  //첫번째터치는 완료된 셈이므로 터치리스트에 넣어준다. 
                    }
                    

                }
            }
        }
    }
}
