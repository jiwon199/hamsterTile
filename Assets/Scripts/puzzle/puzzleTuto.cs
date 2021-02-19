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
   string[] text_vil = { "�׷� ���� ���� �����Ұ�!"+"\n"+"��ġ�� ������ ����Ÿ�Ͽ���," ,
            "���� �� Ÿ�Ͽ��� �̷������ ����." ,
            "�� Ÿ���� ��ġ������ ��� Ÿ���� ��ġ�� ���¿��� Ŭ����� ������ ��.",
             "�������� ���������� ��ġ�� Ÿ�ϰ� ������ Ÿ�ϸ� ��ġ�� �� �ִٴ� �ž�."+"\n"+"�׷��ϱ� �Ѻױ׸��� ��������." ,
             "��Ÿ���� ������ �߿���."+"\n"+ "�ؿ� �ִ� ������ ������� ��ġ�ؾ� Ŭ����� ������ �ǰŵ�.",
             "�̷��� +1�� �Ǿ��ִ� Ÿ���� �� �� ��ġ�ؾ� ��ġ�� ������ ������ �Ǵϱ� ������.",
             "��, �׷� �������� 1�� �����غ�!"
    };
    string[] text_city = { "���ο� Ÿ���� �ֳ�."+"\n"+"���� �ͽ�Ÿ���̶�� ��!" ,
            "�ͽ�Ÿ���� ���� �ִ� +1Ÿ�ϰ����ž�."+"\n"+"�� ������ ���� �� �� ��ġ�ؾ� ��ġ�� ������ ������ �ǰŵ�." ,         
             "�� �ͽ�Ÿ���� �Ķ��� �ʷ� Ÿ���� ��ġ�� �� �ι�° ��ġ�� �ؾ� �´� ������ �ǰ���." ,
             "���� �ִ� ���̵� Ÿ���� �����ϸ� ������ �ɰž�!",
             "��, �׷� �������� 1�� �����غ�!",            
    };
    // Start is called before the first frame update
    void Start()
    {
        StageManager = GameObject.Find("stagenum");
        nowStage = stagenum.stageNum;
    
         //�������� 1 ������ ó�� �÷����ϴ°Ÿ�
        if (GameManager.instance.localWorldInfo == 0&&nowStage==1&&!GameManager.instance.localVilTuto)
        {
            tutorial_vil();
        }
        //��Ƽ�� 1������ ó�� �÷����ϴ� �Ÿ�
        else if (GameManager.instance.localWorldInfo == 1 && nowStage == 1 && !GameManager.instance.localCityTuto)
        {
            tutorial_city();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //��������-Ʃ��
    void tutorial_vil()
    {
        vil_tuto.SetActive(true);
        background.SetActive(true);
        guideHam.SetActive(true);
        vil_tuto.transform.GetChild(0).gameObject.SetActive(true);
        vil_tuto.transform.GetChild(1).gameObject.SetActive(true);
        text.text = text_vil[0];
    }
    //��Ƽ��-Ʃ��
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
