using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
public class loadStageBtn : MonoBehaviour
{
    int lastStage = 24;
    int lastStage2 = 40;
     GameObject[] buttons = new GameObject[40];  //일단 40으로 넉넉히 두고, 햄스빌리지(24)는 뒤에를 빈칸으로 남기기
    public static int stageNum;
    public GameObject stageNumObject;

    private GameObject btnPrefab;
   private Text scoreText;
    public GameObject background;
    soundManager sound;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
         sound = FindObjectOfType<soundManager>();
        background = GameObject.Find("backGround");
        try
        {  //홈씬->스테이지씬으로 정상적으로 넘어오지 않은 경우에도 에러 안나게(테스트플레이용)
            if (GameManager.instance.localWorldInfo == 0)
            {
                createBtn(lastStage);
                setBtnList(lastStage);
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("puzzle/Stageillust_vilage");
            }
            else
            {

                createBtn(lastStage2);
                setBtnList(lastStage2);
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("puzzle/Stageillust_city");
            }
        }catch(Exception)
        {
            
        }



    }

    // Update is called once per frame
    void Update()
    {
        try { 
            if (GameManager.instance.localWorldInfo == 0) changeBtnImg_vil();
            else changeBtnImg_city();
        }catch(Exception)
        {

        }


    }
    void createBtn(int stage)
    {
       
        for (int i = 0; i < stage; i++)
        {
            GameObject btn=  Instantiate(Resources.Load("Prefabs/stageButton")) as GameObject;  //프리팹로드
            btn.transform.SetParent(this.transform);  //만든 프리팹의 parent는 content(이안에 넣으면 배치가 알아서됨)
            btn.name = (i + 1).ToString();   //이름을 숫자로 바꾸기
            btn.transform.GetChild(0).GetComponent<Text>().text= (i + 1).ToString();  //텍스트를 숫자로 바꾸기
            btn.GetComponent<Button>().onClick.AddListener(clickBtn);   //clickBtn 함수를 리스터로 추가
         
        }
    }
    //배열에 넣어주는함수(createBtn에서 넣으면 리로드시 배열터짐)
    void setBtnList(int stage)
    {
        for(int i = 0; i < stage; i++)
        {
            string name = (i + 1).ToString();
           GameObject btn = GameObject.Find(name);
            buttons[i] = btn;
           
        }
    }
    //버튼클릭시 호출되는 함수
    public void clickBtn()
    {
             
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        stagenum.stageNum = int.Parse(buttonName);   //클릭한 버튼 이름을 스테이지넘 객체의 스테이지넘버로
        sound.BtnClick();  
        StartCoroutine(WaitForSound());  //버튼음 들린 다음에 씬 넘어가게 코루틴        
        DontDestroyOnLoad(stageNumObject);   //스테이지넘 객체는 파괴되지 않고 전달되도록
    }
    IEnumerator WaitForSound()
    {
        while (sound.btnAudio.isPlaying)
            yield return null;
        SceneManager.LoadScene("puzzleScene");  //퍼즐씬 로드
    }
    //버튼 이미지 바꾸는 함수-빌리지용
    public void changeBtnImg_vil()
    {
        int count = 0;
        //클리어한 스테이지는 별 붙은 버튼 이미지로
        for (int i = 0; i < lastStage; i++)
        {          
            if (GameManager.instance.localClearInfo[i])   
            {
                buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("puzzle/btnclear") as Sprite;
                buttons[i].transform.GetChild(1).gameObject.SetActive(false);
                count++;
            }
        }
        scoreText.text = count.ToString() + "/24";
        //전 스테이지 안깼으면 버튼 클릭 막기
        for (int i = 1; i < lastStage; i++)
        {

            if (!GameManager.instance.localClearInfo[i - 1])
            {
                buttons[i].transform.GetChild(1).gameObject.SetActive(true);
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }

    }
    //버튼 이미지 바꾸는 함수-시티용
    public void changeBtnImg_city()
    {
        int count = 0;
        //클리어한 스테이지는 별 붙은 버튼 이미지로
        for (int i = 0; i < lastStage2; i++)
        {
            if (GameManager.instance.localClearInfo2[i])   
            {
                buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("puzzle/btnclear") as Sprite;
                buttons[i].transform.GetChild(1).gameObject.SetActive(false);
                count++;
            }
        }
        scoreText.text = count.ToString() + "/40";
        //전 스테이지 안깼으면 버튼 클릭 막기
        for (int i = 1; i < lastStage2; i++)
        {

            if (!GameManager.instance.localClearInfo2[i - 1])
            {
                buttons[i].transform.GetChild(1).gameObject.SetActive(true);
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }

    }
}
