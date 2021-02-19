using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class puzzleTuto : MonoBehaviour
{
    GameObject StageManager;
    int nowStage;
    public GameObject vil_tuto;
    public GameObject city_tuto;
    public GameObject background;
    public GameObject guideHam;
    int line = 0;
    public Text text;
    public Text text2;
   string[] text_vil = { "그럼 퍼즐 룰을 설명할게!"+"\n"+"터치의 시작은 시작타일에서," ,
            "끝은 끝 타일에서 이루어져야 하지." ,
            "끝 타일을 터치했을때 모든 타일을 터치한 상태여야 클리어로 판정이 돼.",
             "주의점은 마지막으로 터치한 타일과 인접한 타일만 터치할 수 있다는 거야."+"\n"+"그러니까 한붓그리기 같은거지." ,
             "색타일은 순서가 중요해."+"\n"+ "밑에 있는 색부터 순서대로 터치해야 클리어로 판정이 되거든.",
             "이렇게 +1이 되어있는 타일은 두 번 터치해야 터치한 것으로 인정이 되니까 조심해.",
             "자, 그럼 스테이지 1을 시작해봐!"
    };
    string[] text_city = { "새로운 타일이 있네."+"\n"+"저건 믹스타일이라고 해!" ,
            "믹스타일은 색이 있는 +1타일같은거야."+"\n"+"색 순서에 맞춰 두 번 터치해야 터치한 것으로 인정이 되거든." ,         
             "저 믹스타일은 파랑과 초록 타일을 터치한 후 두번째 터치를 해야 맞는 순서가 되겠지." ,
             "위에 있는 가이드 타일을 참고하면 도움이 될거야!",
             "자, 그럼 스테이지 1을 시작해봐!",            
    };
    // Start is called before the first frame update
    void Start()
    {
        StageManager = GameObject.Find("stagenum");
        nowStage = stagenum.stageNum;
    
         //빌리지의 1 레벨를 처음 플레이하는거면
        if (GameManager.instance.localWorldInfo == 0&&nowStage==1&&!GameManager.instance.localVilTuto)
        {
            tutorial_vil();
        }
        //시티의 1레벨을 처음 플레이하는 거면
        else if (GameManager.instance.localWorldInfo == 1 && nowStage == 1 && !GameManager.instance.localCityTuto)
        {
            tutorial_city();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //빌리지용-튜토
    void tutorial_vil()
    {
        vil_tuto.SetActive(true);
        background.SetActive(true);
        guideHam.SetActive(true);
        vil_tuto.transform.GetChild(0).gameObject.SetActive(true);
        vil_tuto.transform.GetChild(1).gameObject.SetActive(true);
        text.text = text_vil[0];
    }
    //시티용-튜토
    void tutorial_city()
    {
        city_tuto.SetActive(true);
        background.SetActive(true);
        guideHam.SetActive(true);
        city_tuto.transform.GetChild(0).gameObject.SetActive(true);
        city_tuto.transform.GetChild(1).gameObject.SetActive(true);
        text2.text = text_city[0];

    }
    public void nextBtn()
    {       
        line++;
        if(line==7)
        {
            GameManager.instance.updateTuto_vil();
            vil_tuto.SetActive(false);
            background.SetActive(false);
            guideHam.SetActive(false);
            line--;
        }
        text.text = text_vil[line];
        CircleStep(line);
    }
    public void formerBtn()
    {
        if(line!=0)line--;
        text.text = text_vil[line];
        CircleStep(line);
    }
    public void nextBtn_city()
    {
        line++;
        if (line == 5)
        {
            GameManager.instance.updateTuto_cityl();
            city_tuto.SetActive(false);
            background.SetActive(false);
            guideHam.SetActive(false);
            line--;
        }
        text2.text = text_city[line];
        CircleStep_city(line);
    }
    public void formerBtn_city()
    {
        if (line != 0) line--;
        text2.text = text_city[line];
        CircleStep_city(line);
    }
    void CircleStep(int line)
    {
        hideAll();
        if (line == 0) vil_tuto.transform.GetChild(1).gameObject.SetActive(true);
        else if (line >= 1 && line <= 3) vil_tuto.transform.GetChild(2).gameObject.SetActive(true);
        else if (line == 4) vil_tuto.transform.GetChild(3).gameObject.SetActive(true);
        else if (line == 5) vil_tuto.transform.GetChild(4).gameObject.SetActive(true);
        else if (line == 6) hideAll();
    }
    void CircleStep_city(int line)
    {
        hideAll();
        if(line>=0&&line<2) city_tuto.transform.GetChild(1).gameObject.SetActive(true);
        else if (line == 2) city_tuto.transform.GetChild(2).gameObject.SetActive(true);
        else if (line==3) city_tuto.transform.GetChild(3).gameObject.SetActive(true);
        else if (line == 4) hideAll();      
    }
    void hideAll()
    {
        for(int i = 1; i <= 4; i++)
        {
            vil_tuto.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 1; i <= 3; i++)
        {
            city_tuto.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
