using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class blink : MonoBehaviour
{
     
    float time;
    public GameObject myImage; //���������� ������Ʈ�� �Ͼ߶�Ű���� �巡��
   
  
    // Update is called once per frame
    void Update()
    {
        //�׵θ��� ���������ϰ�..
        if (time < 0.5f)
        {
             myImage.GetComponent<Image>().color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            myImage.GetComponent<Image>().color = new Color(1, 1, 1, time);
            if (time > 1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }
}
