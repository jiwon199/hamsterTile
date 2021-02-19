using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//���۾��� ó������, �̾��ϱ� ���� ��ũ��Ʈ
public class startScenePopup : MonoBehaviour
{
    //ù�÷��̽ÿ� �̾��ϱ� ��ư �Ⱥ��̰�+�����ϱ��ư�� ���οö����
    public GameObject continueBtn;
   
    //�˾��� ����
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
    //ó������ ��ư ��������
    public void clickBegining()
    {
        //���� �����Ͱ� �ִ°��
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
    //����� ���� �Ͻðڽ��ϱ�? �˾����� ���� ����
    public void clickReset()
    {
        //�������ʱ�ȭ
        GameManager.instance.makeD();
        GameManager.instance.Load();
        sound.BtnClick();
        //���丮�� �ε�
        StartCoroutine(WaitForSound());
    }
    IEnumerator WaitForSound()
    {
        while (sound.btnAudio.isPlaying)
            yield return null;
        SceneManager.LoadScene("story");  //����� �ε�
    }
    //����� ���� �Ͻðڽ��ϱ�? �˾����� �ƴϿ��� ����
    public void clickNoReset()
    {
        //�˾�����
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
