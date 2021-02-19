using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class homePopup : MonoBehaviour
{
    public GameObject completePopup, completePopup2, checkbuttonPopup;

    Animator animator;
    private int world;
    void Awake()
    {
        //애니메이터 불러옴
        animator=checkbuttonPopup.GetComponent<Animator>();
    }
    void Start()
    {
        world=GameManager.instance.localWorldInfo;
    }

    void Update()
    {
        if(world == 0 && GameObject.Find("Progress Bar").GetComponent<Slider>().value>99.9 && !GameManager.instance.localWatchStory2)
        {
            completePopup.SetActive(true);
        }
        else if (world == 1 && GameObject.Find("Progress Bar").GetComponent<Slider>().value>99.9 && !GameManager.instance.localCompleteInfo[1])
        {
            completePopup2.SetActive(true);
        }

        if(!GameManager.instance.localCompleteInfo[0] && GameManager.instance.localWatchStory2)
        {
            checkbuttonPopup.SetActive(true);
        }
    }

    //Checkbutton Popup에서 확인버튼을 눌렀는지 확인
    public void onClick(){
        GameManager.instance.updateComplete(0);
        animator.SetBool("isClicked",true);
        Invoke("hidePanel",1);
    }
    private void hidePanel(){
        checkbuttonPopup.SetActive(false);
        Debug.Log("햄스빌리지 클리어");
    }

    //completePopup2에서 확인버튼을 누르면 스테이지 클리어상태 변경
    public void onClickForEnding(){
        GameManager.instance.updateComplete(1);
    }
}
