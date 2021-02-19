using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class TypingEffect: MonoBehaviour
{
    public Text tx;
    private string[] texts = { "태초에 말씀이 있었다..", "『 마을을 풍요롭게 하고 싶다면 퍼즐을 풀어라.. 』","하지만 퍼즐을 푸는 것은 햄스터가 아닌 인간의 일..", "햄스터들의 마을은 갈수록 황폐해져갔다.","그러던 어느날...","아니 이것은?","지식이...흘러들어온다!","이 지식이라면...! 퍼즐을 풀어낼 수 있을거야!"};
    private bool[] textsShow ={false,false,false,false,false,false,false,false};  //대사출력 여부 bool 배열
    public GameObject[] illust = new GameObject[3];  //스토리 씬 그림
    AudioSource audioSource;
    public GameObject typingbgm;//스토리 앞부분,뒷부분 배경음악, 타이핑효과음

    public AudioClip[] Music = new AudioClip[2];
    public GameObject btn; //다음 씬으로 가는 버튼
    public GameObject popupPanel; //팝업뜰때 그림 가리는 용도의 팝업
    public GameObject popup;  //팝업창 
    int line=0;
    bool CR_running ;
    
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_typing());
        CR_running = true;

        audioSource.clip = Music[0];
        audioSource.Play();

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
            
            if (!textsShow[line]) {   //아직 출력 안될 대사일때만 대사출력하기 (이 처리 안하면 같은대사를 반복해서 출력함)
                typingbgm.SetActive(true);
                // btn.SetActive(false);  //출력중일땐 버튼꺼두기
                CR_running = true;
                for (int i = 0; i <= texts[line].Length; i++) //글자수만큼 반복
                {                   
                    tx.text = texts[line].Substring(0, i); //(n,m) -> n번째부터 m개까지 출력               
                    yield return new WaitForSeconds(0.08f);
                }
                textsShow[line] = true;
                typingbgm.SetActive(false);
            }
            yield return new WaitForSeconds(0.03f);
            CR_running = false;
            btn.SetActive(true);//다음 버튼 다시 활성화.
            
        }
    }
    public void nextBtn()
    {
        if (CR_running)
        {         
            tx.text = texts[line];
            textsShow[line] = true;
            //사용자가 다음 버튼을 연타하면 타이핑 코루틴을 중단하고 대사를 모두 출력
            StopAllCoroutines();
            typingbgm.SetActive(false);
            StartCoroutine(_typing());
        }
        else
        {          
            if (line < 8) line++;   //타이핑 반복문의 line변수는 버튼 클릭시에 증가하도록
             
            //대사에 맞춰서 팝업, 그림을 보이게 한다.
            if (line == 2) illust[1].SetActive(true);
            if (line == 5)
            {
                illust[2].SetActive(true);          
                audioSource.clip = Music[1];
                audioSource.Play();            
            }
            if (line == 8) Invoke("ShowPopup", 0.5f);  //invoke로 살~짝 딜레이
        }
             
        
    }
    
    //팝업보이게하는 함수
    public void ShowPopup()
    {
        popupPanel.SetActive(true);
        popup.SetActive(true);       
    }
    //스토리 시청여부를 트루로-가즈아 버튼누르면 호출됨
    public void updateStory()
    {
        GameManager.instance.updateStoryShow();
    }
     
}
 
