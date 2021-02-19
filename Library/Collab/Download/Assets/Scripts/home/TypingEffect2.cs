using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect2 : MonoBehaviour
{
    public Text tx;
    private string[] texts = { "퍼즐을 풀어 무사히 햄스빌리지 재건에 성공한 김햄찌..", "마을은 풍요로워졌고 활기를 되찾았다..", "그러던 어느날, 김햄찌에게 마을의 장로님이 찾아와 말했다..", "김햄찌.. 지금 옆동네의 햄시티라는 도시에 문제가 있다네..", "우리마을의 영웅인 네가 햄시티를 도와주면 어떻겠나?", "김햄찌는 잠시 고민했지만 흔쾌히 제안을 수락했다", "그렇게 김햄찌는 햄시티를 구하기위해 길을 떠났다.." };
    private bool[] textsShow = { false, false, false, false, false, false, false, false };  //대사출력 여부 bool 배열
    public GameObject[] illust = new GameObject[3];  //스토리 씬 그림

    public GameObject btn; //다음 씬으로 가는 버튼
    public GameObject popupPanel; //팝업뜰때 그림 가리는 용도의 팝업
    public GameObject popup;  //팝업창 
    int line = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_typing());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator _typing()
    {

        yield return new WaitForSeconds(0.05f);// 이거 포함해서 전반적인 딜레이 시간 좀 줄였음
        for (; line < texts.Length;)
        {
            if (!textsShow[line])
            {   //아직 출력 안될 대사일때만 대사출력하기 (이 처리 안하면 같은대사를 반복해서 출력함)
                btn.SetActive(false);  //출력중일땐 버튼꺼두기
                for (int i = 0; i <= texts[line].Length; i++) //글자수만큼 반복
                {
                    tx.text = texts[line].Substring(0, i); //(n,m) -> n번째부터 m개까지 출력               
                    yield return new WaitForSeconds(0.09f);
                }
                textsShow[line] = true;
            }
            yield return new WaitForSeconds(0.05f);
            btn.SetActive(true);//다음 버튼 다시 활성화.


        }
    }
    public void nextBtn()
    {
        if (line < 8) line++;   //타이핑 반복문의 line변수는 버튼 클릭시에 증가하도록
        //대사에 맞춰서 팝업, 그림을 보이게 한다.
        if (line == 2) illust[1].SetActive(true);
        if (line == 5) illust[2].SetActive(true);
        if (line == 8) Invoke("ShowPopup", 0.5f);  //invoke로 살~짝 딜레이
    }
    //팝업보이게하는 함수
    public void ShowPopup()
    {
        popupPanel.SetActive(true);
        popup.SetActive(true);
    }
    //스토리 시청여부를 트루로-가즈아 버튼누르면 호출됨
    public void updateStory2()
    {
        GameManager.instance.updateStoryShow2();
    }

}

