using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class example : MonoBehaviour
{
     
    void Start()
    {
        /* 1번째, 3번째, 5번째, 6번째, 8번째, 12번째 스테이지를 깼을때 + 진행도가 100퍼센트가 되었을때 아이템(황금동상) 제공 =총7개
         * GameManager의 localClearInfo는 튜토리얼 스테이지 총 12개의 클리어 여부를 담은 배열( inspector 상에서도 확인가능 )
         * 이 배열을 읽어서 플레이어가 이 아이템을 얻었는지 아닌지 판별(localClearInfo[0]=true면 첫번째 아이템을 얻은 상태)        
         * 이 스크립트는 사용 예시를 보여주기 위한 스크립트일뿐이므로, 지우거나 스크립트 이름을 바꾸거나하셔도 됩니다. 
        */
        //사용 예시.  하야라키에  gameObject 만들어서 거기에 스크립트 드래그해서 붙이기
        Debug.Log("첫번째 스테이지 클리어 여부: " +GameManager.instance.localClearInfo[0]);
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
