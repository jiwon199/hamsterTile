using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class barControl : MonoBehaviour
{
    [SerializeField] Slider progressbar;    //progressBar오브젝트 받아올 것
    [SerializeField] Text barPercentage;    //진행도 숫자로 표시
    [SerializeField] GameObject popup;

    Animator animator;

    private int maxItem;
    private int curItem;
    private int world;


    void Awake(){
        //애니메이터 불러옴
        animator=popup.GetComponent<Animator>();
    }


    void Start()
    {
        world=GameManager.instance.localWorldInfo;
        curItem=getItem();
        if(world==0)
        {
            progressbar.value = ((float)curItem / (float)GameManager.instance.localPlacedInfo.Length) * 100;
            barPercentage.text = Mathf.Round(((float)curItem / (float)GameManager.instance.localPlacedInfo.Length) * 100).ToString()+"%";
        }
        else
        {
            progressbar.value = ((float)curItem / (float)GameManager.instance.localPlacedInfo2.Length) * 100;
            barPercentage.text = Mathf.Round(((float)curItem / (float)GameManager.instance.localPlacedInfo2.Length) * 100).ToString()+"%";
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        curItem=getItem();
        if(world==0)
        {
            progressbar.value = Mathf.Lerp(progressbar.value,((float)curItem / (float)GameManager.instance.localPlacedInfo.Length) * 100,0.035f);
            barPercentage.text=Mathf.Round(((float)curItem / (float)GameManager.instance.localPlacedInfo.Length) * 100).ToString()+"%";
        }
        else
        {
            progressbar.value = Mathf.Lerp(progressbar.value,((float)curItem / (float)GameManager.instance.localPlacedInfo2.Length) * 100,0.035f);
            barPercentage.text=Mathf.Round(((float)curItem / (float)GameManager.instance.localPlacedInfo2.Length) * 100).ToString()+"%";
        }

        if(progressbar.value>99.9 && !GameManager.instance.localCompleteInfo[0]){
            popup.SetActive(true);
        }

    }


    //현재 배치된 아이템 갯수 받아오는 함수
    private int getItem() {
        int n=0;
        if(world == 0)
        {
            for(int i=0; i<GameManager.instance.localPlacedInfo.Length; i++) if(GameManager.instance.localPlacedInfo[i]) n++;
        }
        else
        {
            for(int i=0; i<GameManager.instance.localPlacedInfo2.Length; i++) if(GameManager.instance.localPlacedInfo2[i]) n++;
        }
        return n;
    }

    //Popup에서 확인버튼을 눌렀는지 확인
    public void onClick(){
        GameManager.instance.localCompleteInfo[0]=true;
        GameManager.instance.Save();
        animator.SetBool("isClicked",true);
        Invoke("hidePanel",1);
    }

    private void hidePanel(){
        GameObject.Find("Canvas").transform.Find("changeWorld (Button)").gameObject.SetActive(true);
        popup.SetActive(false);
        Debug.Log("햄스빌리지 클리어");
    }
}