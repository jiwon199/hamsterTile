using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class soundManager : MonoBehaviour
{
    public AudioSource btnAudio;
    public AudioClip btnSound;
    bool plusStage;
    // Start is called before the first frame update
    void Start()
    {
        this.btnAudio = this.gameObject.AddComponent<AudioSource>();
        this.btnAudio.clip = this.btnSound;
        this.btnAudio.loop = false;
        this.btnAudio.volume = 0.1f;
        plusStage = false;
    }

// Update is called once per frame
    void Update()
    {
        
    }
    //버튼클릭
    public void BtnClick()
    {
        this.btnAudio.Play();
       
    }
    //퍼즐씬-퍼즐씬 이동시 버튼클릭 사운드가 막혀서 따로..
    //효과음 출력된 다음에 다음씬으로 이동하기
    public void puzzleBtnClick()
    {
        this.btnAudio.Play();
        StartCoroutine(WaitForSound());
    }
    public void goToNextPuzzle()
    {

        int lastStage;
        if (GameManager.instance.localWorldInfo == 0) lastStage = 24;
        else lastStage = 40;
        GameObject.Find("stagenum");
        if (stagenum.stageNum < lastStage&&!plusStage) {
            stagenum.stageNum++;
            plusStage = true;
        }
        this.btnAudio.Play();
        StartCoroutine(WaitForSound());

    }
    IEnumerator WaitForSound()
    {
        while ( btnAudio.isPlaying)
            yield return null;
        SceneManager.LoadScene("puzzleScene");  //퍼즐씬 로드
        plusStage = true;
    }

    //홈씬 월드바꿀 때도 버튼 사운드가 막혀서...따로......
    public void changeBtnClick()
    {
        this.btnAudio.Play();
        StartCoroutine(WaitForSound1());
    }
    IEnumerator WaitForSound1()
    {
        while (btnAudio.isPlaying)
            yield return null;
        SceneManager.LoadScene("homeScene");  //홈씬 로드
    }

}
