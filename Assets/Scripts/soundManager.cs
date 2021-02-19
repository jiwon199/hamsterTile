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
    //��ưŬ��
    public void BtnClick()
    {
        this.btnAudio.Play();
       
    }
    //�����-����� �̵��� ��ưŬ�� ���尡 ������ ����..
    //ȿ���� ��µ� ������ ���������� �̵��ϱ�
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
        SceneManager.LoadScene("puzzleScene");  //����� �ε�
        plusStage = true;
    }

    //Ȩ�� ����ٲ� ���� ��ư ���尡 ������...����......
    public void changeBtnClick()
    {
        this.btnAudio.Play();
        StartCoroutine(WaitForSound1());
    }
    IEnumerator WaitForSound1()
    {
        while (btnAudio.isPlaying)
            yield return null;
        SceneManager.LoadScene("homeScene");  //Ȩ�� �ε�
    }

}
