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
        //처음엔 "" 인 상태
        guideText1 = GameObject.Find("guideText1").GetComponent<Text>();
        guideText2 = GameObject.Find("guideText2").GetComponent<Text>();
        SeqText = GameObject.Find("SeqText").GetComponent<Text>();
       
    }
    //룰이 안지켜지고 있으면 안내메시지 보여주기
    public void addMessage(string message)
    {
        //이미 존재하고 있는 안내 메세지라면 중복으로 안내할 필요 없다. 
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
    //상황이 괜찮아지면 없애기
    public void deleteMessage(string message) //없애야 할 메세지를 전달받으면
    {
      
        if (message.Equals(guideText1.text))
        {
             
            //메세지 두개인 상태에서 첫번째 슬롯의 메세지를 삭제해야 되면 슬롯2 메세지를 슬롯1로 올리기        
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
        //슬롯 2 메세지면 그냥 삭제하면 됨
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
        
        string[] mycolor = { "빨강 ", "파랑 ", "초록 ", "노랑 ", "보라 " };
        for (int i = 0; i < colorNum; i++)
        {

            if (i == colorNum - 1) SeqText.text = SeqText.text + mycolor[i];
            else SeqText.text = SeqText.text + mycolor[i] + "- ";
        }
        SeqText.text = SeqText.text + "순으로 터치!";
    }

}
