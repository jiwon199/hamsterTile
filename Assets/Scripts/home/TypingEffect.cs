using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class TypingEffect: MonoBehaviour
{
    public Text tx;
    private string[] texts = { "���ʿ� ������ �־���..", "�� ������ ǳ��Ӱ� �ϰ� �ʹٸ� ������ Ǯ���.. ��","������ ������ Ǫ�� ���� �ܽ��Ͱ� �ƴ� �ΰ��� ��..", "�ܽ��͵��� ������ ������ Ȳ����������.","�׷��� �����...","�ƴ� �̰���?","������...�귯���´�!","�� �����̶��...! ������ Ǯ� �� �����ž�!"};
    private bool[] textsShow ={false,false,false,false,false,false,false,false};  //������ ���� bool �迭
    public GameObject[] illust = new GameObject[3];  //���丮 �� �׸�
    AudioSource audioSource;
    public GameObject typingbgm;//���丮 �պκ�,�޺κ� �������, Ÿ����ȿ����

    public AudioClip[] Music = new AudioClip[2];
    public GameObject btn; //���� ������ ���� ��ư
    public GameObject popupPanel; //�˾��㶧 �׸� ������ �뵵�� �˾�
    public GameObject popup;  //�˾�â 
    int line=0;
    bool CR_running ;
    
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_typing());
        CR_running = true;

        audioSource.clip = Music[0];
        audioSource.Play();

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
            
            if (!textsShow[line]) {   //���� ��� �ȵ� ����϶��� �������ϱ� (�� ó�� ���ϸ� ������縦 �ݺ��ؼ� �����)
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
            if (line < 8) line++;   //Ÿ���� �ݺ����� line������ ��ư Ŭ���ÿ� �����ϵ���
             
            //��翡 ���缭 �˾�, �׸��� ���̰� �Ѵ�.
            if (line == 2) illust[1].SetActive(true);
            if (line == 5)
            {
                illust[2].SetActive(true);          
                audioSource.clip = Music[1];
                audioSource.Play();            
            }
            if (line == 8) Invoke("ShowPopup", 0.5f);  //invoke�� ��~¦ ������
        }
             
        
    }
    
    //�˾����̰��ϴ� �Լ�
    public void ShowPopup()
    {
        popupPanel.SetActive(true);
        popup.SetActive(true);       
    }
    //���丮 ��û���θ� Ʈ���-����� ��ư������ ȣ���
    public void updateStory()
    {
        GameManager.instance.updateStoryShow();
    }
     
}
 
