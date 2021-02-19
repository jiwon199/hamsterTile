using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect2 : MonoBehaviour
{
    public Text tx;
    private string[] texts = { "������ Ǯ�� ������ �ܽ������� ��ǿ� ������ ������..", "������ ǳ��ο����� Ȱ�⸦ ��ã�Ҵ�.", "�׷��� �����, ������� ������ ��δ��� ã�ƿ� ���ߴ�.", "������.. ���� �������� �ܽ�Ƽ��� ���ÿ� ������ �ִٳ�..", "�츮������ ������ �װ� �ܽ�Ƽ�� �����ָ� ��ڳ�?", "������� ��� ��������� ������ ������ �����ߴ�.", "�׷��� ������� �ܽ�Ƽ�� ���ϱ����� ���� ������.." };

    private bool[] textsShow = { false, false, false, false, false, false, false };  //������ ���� bool �迭
    public GameObject[] illust = new GameObject[4];  //���丮 �� �׸�

    public GameObject typingbgm;//���丮 �պκ�,�޺κ� �������, Ÿ����ȿ����

    public GameObject btn; //���� ������ ���� ��ư
    public GameObject popupPanel; //�˾��㶧 �׸� ������ �뵵�� �˾�
    public GameObject popup;  //�˾�â 
    int line = 0;
    bool CR_running;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_typing());
        CR_running = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator _typing()
    {

        yield return new WaitForSeconds(0.05f);// �̰� �����ؼ� �������� ������ �ð� �� �ٿ���

        for (; line < texts.Length;)
        {

            if (!textsShow[line])
            {   //���� ��� �ȵ� ����϶��� �������ϱ� (�� ó�� ���ϸ� ������縦 �ݺ��ؼ� �����)
                typingbgm.SetActive(true);
                // btn.SetActive(false);  //������϶� ��ư���α�
                CR_running = true;
                for (int i = 0; i <= texts[line].Length; i++) //���ڼ���ŭ �ݺ�
                {
                    tx.text = texts[line].Substring(0, i); //(n,m) -> n��°���� m������ ���               
                    yield return new WaitForSeconds(0.08f);
                }
                textsShow[line] = true;
                typingbgm.SetActive(false);
            }
            yield return new WaitForSeconds(0.03f);
            CR_running = false;
            btn.SetActive(true);//���� ��ư �ٽ� Ȱ��ȭ.

        }
    }
    public void nextBtn()
    {
        if (CR_running)
        {
            tx.text = texts[line];
            textsShow[line] = true;
            //����ڰ� ���� ��ư�� ��Ÿ�ϸ� Ÿ���� �ڷ�ƾ�� �ߴ��ϰ� ��縦 ��� ���
            StopAllCoroutines();
            typingbgm.SetActive(false);
            StartCoroutine(_typing());
        }
        else
        {
            if (line < 7) line++;   //Ÿ���� �ݺ����� line������ ��ư Ŭ���ÿ� �����ϵ���

            //��翡 ���缭 �˾�, �׸��� ���̰� �Ѵ�.
            if (line == 3) illust[1].SetActive(true);
            if (line == 5)
            {
                illust[2].SetActive(true);
            }
            if(line==6) illust[3].SetActive(true);
            if (line == 7) Invoke("ShowPopup", 0.5f);  //invoke�� ��~¦ ������
        }


    }

    //�˾����̰��ϴ� �Լ�
    public void ShowPopup()
    {
        popupPanel.SetActive(true);
        popup.SetActive(true);
    }
    //���丮 ��û���θ� Ʈ���-����� ��ư������ ȣ���
    public void updateStory2()
    {
        GameManager.instance.updateStoryShow2();
    }

}


