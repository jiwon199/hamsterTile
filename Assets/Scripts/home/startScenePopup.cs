using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//시작씬의 처음부터, 이어하기 관리 스크립트
public class startScenePopup : MonoBehaviour
{
    //첫플레이시엔 이어하기 버튼 안보이게+종료하기버튼이 위로올라오게
    public GameObject continueBtn;
   
    //팝업용 변수
    public GameObject panel;
    public GameObject popup;
    soundManager sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = FindObjectOfType<soundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowContinueBtn();
    }
    //처음부터 버튼 눌렀을때
    public void clickBegining()
    {
        //이전 데이터가 있는경우
        if (GameManager.instance.localWatchStory)
        {
            sound.BtnClick();
            panel.SetActive(true);
            popup.SetActive(true);
        }
        else
        {
            sound.BtnClick();
            StartCoroutine(WaitForSound());
        }
    }
    //지우고 새로 하시겠습니까? 팝업에서 예를 선택
    public void clickReset()
    {
        //데이터초기화
        GameManager.instance.makeD();
        GameManager.instance.Load();
        sound.BtnClick();
        //스토리씬 로드
        StartCoroutine(WaitForSound());
    }
    IEnumerator WaitForSound()
    {
        while (sound.btnAudio.isPlaying)
            yield return null;
        SceneManager.LoadScene("story");  //퍼즐씬 로드
    }
    //지우고 새로 하시겠습니까? 팝업에서 아니오를 선택
    public void clickNoReset()
    {
        //팝업끄기
        panel.SetActive(false);
        popup.SetActive(false);

    }
    public void ShowContinueBtn()
    {
        if (GameManager.instance.localWatchStory)
        {
            continueBtn.SetActive(true);
        }
        else
        {
            continueBtn.SetActive(false);
        }
    }
    
}
