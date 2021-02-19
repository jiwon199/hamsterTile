using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GuideText : MonoBehaviour
{
    public Text guideText1;
    public Text guideText2;
    public Text SeqText;

    List<Text> guideList = new List<Text>();
    void Start()
    {
        //ó���� "" �� ����
        guideText1 = GameObject.Find("guideText1").GetComponent<Text>();
        guideText2 = GameObject.Find("guideText2").GetComponent<Text>();
        SeqText = GameObject.Find("SeqText").GetComponent<Text>();
       
    }
    //���� ���������� ������ �ȳ��޽��� �����ֱ�
    public void addMessage(string message)
    {
        //�̹� �����ϰ� �ִ� �ȳ� �޼������ �ߺ����� �ȳ��� �ʿ� ����. 
        if (!(guideText1.text.Equals(message) || guideText2.text.Equals(message))) {  
         //0 -> 1
         if(guideText1.text.Equals("")&& guideText2.text.Equals(""))
        {
            setSlot1(message);
        }
        //1->2
       else if (!guideText1.text.Equals("") && guideText2.text.Equals(""))
        {
            setSlot2(message);
        }
        }
    }
    //��Ȳ�� ���������� ���ֱ�
    public void deleteMessage(string message) //���־� �� �޼����� ���޹�����
    {
      
        if (message.Equals(guideText1.text))
        {
             
            //�޼��� �ΰ��� ���¿��� ù��° ������ �޼����� �����ؾ� �Ǹ� ����2 �޼����� ����1�� �ø���        
            if (!guideText2.text.Equals(""))
            {                
                guideText1.text = guideText2.text;
                guideText2.text = "";
            }
            else
            {
                guideText1.text = "";
            }
        }
        //���� 2 �޼����� �׳� �����ϸ� ��
        else if (message.Equals(guideText2.text))
        {
            guideText2.text = "";
        }
       
    }
    private void setSlot1(string message)
    {
        guideText1.text = message;
    }
    private void setSlot2(string message)
    {
        guideText2.text = message;
    }
    public void createSeqText(int colorNum)
    {
        
        string[] mycolor = { "���� ", "�Ķ� ", "�ʷ� ", "��� ", "���� " };
        for (int i = 0; i < colorNum; i++)
        {

            if (i == colorNum - 1) SeqText.text = SeqText.text + mycolor[i];
            else SeqText.text = SeqText.text + mycolor[i] + "- ";
        }
        SeqText.text = SeqText.text + "������ ��ġ!";
    }

}
