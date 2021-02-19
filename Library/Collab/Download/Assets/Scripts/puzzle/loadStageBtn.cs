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
     GameObject[] buttons = new GameObject[40];  //�ϴ� 40���� �˳��� �ΰ�, �ܽ�������(24)�� �ڿ��� ��ĭ���� �����
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
        {  //Ȩ��->�������������� ���������� �Ѿ���� ���� ��쿡�� ���� �ȳ���(�׽�Ʈ�÷��̿�)
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
            GameObject btn=  Instantiate(Resources.Load("Prefabs/stageButton")) as GameObject;  //�����շε�
            btn.transform.SetParent(this.transform);  //���� �������� parent�� content(�̾ȿ� ������ ��ġ�� �˾Ƽ���)
            btn.name = (i + 1).ToString();   //�̸��� ���ڷ� �ٲٱ�
            btn.transform.GetChild(0).GetComponent<Text>().text= (i + 1).ToString();  //�ؽ�Ʈ�� ���ڷ� �ٲٱ�
            btn.GetComponent<Button>().onClick.AddListener(clickBtn);   //clickBtn �Լ��� �����ͷ� �߰�
         
        }
    }
    //�迭�� �־��ִ��Լ�(createBtn���� ������ ���ε�� �迭����)
    void setBtnList(int stage)
    {
        for(int i = 0; i < stage; i++)
        {
            string name = (i + 1).ToString();
           GameObject btn = GameObject.Find(name);
            buttons[i] = btn;
           
        }
    }
    //��ưŬ���� ȣ��Ǵ� �Լ�
    public void clickBtn()
    {
             
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        stagenum.stageNum = int.Parse(buttonName);   //Ŭ���� ��ư �̸��� ���������� ��ü�� ���������ѹ���
        sound.BtnClick();  
        StartCoroutine(WaitForSound());  //��ư�� �鸰 ������ �� �Ѿ�� �ڷ�ƾ        
        DontDestroyOnLoad(stageNumObject);   //���������� ��ü�� �ı����� �ʰ� ���޵ǵ���
    }
    IEnumerator WaitForSound()
    {
        while (sound.btnAudio.isPlaying)
            yield return null;
        SceneManager.LoadScene("puzzleScene");  //����� �ε�
    }
    //��ư �̹��� �ٲٴ� �Լ�-��������
    public void changeBtnImg_vil()
    {
        int count = 0;
        //Ŭ������ ���������� �� ���� ��ư �̹�����
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
        //�� �������� �Ȳ����� ��ư Ŭ�� ����
        for (int i = 1; i < lastStage; i++)
        {

            if (!GameManager.instance.localClearInfo[i - 1])
            {
                buttons[i].transform.GetChild(1).gameObject.SetActive(true);
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }

    }
    //��ư �̹��� �ٲٴ� �Լ�-��Ƽ��
    public void changeBtnImg_city()
    {
        int count = 0;
        //Ŭ������ ���������� �� ���� ��ư �̹�����
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
        //�� �������� �Ȳ����� ��ư Ŭ�� ����
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
