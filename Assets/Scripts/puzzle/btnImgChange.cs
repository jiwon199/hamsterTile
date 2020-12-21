using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class btnImgChange : MonoBehaviour
{
    public GameObject[] buttons = new GameObject[12];
    
    // Update is called once per frame
    void Update()
    {
        changeBtnImg();
    }
    //버튼 이미지 바꾸는 함수
    public void changeBtnImg()
    {

        for (int i = 0; i < 12; i++)
        {
            if (GameManager.instance.localClearInfo[i])
            {
                buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("btnclear") as Sprite;
            }
        }

    }
}
