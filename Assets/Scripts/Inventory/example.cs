using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class example : MonoBehaviour
{
     
    void Start()
    {
        /* 1��°, 3��°, 5��°, 6��°, 8��°, 12��° ���������� ������ + ���൵�� 100�ۼ�Ʈ�� �Ǿ����� ������(Ȳ�ݵ���) ���� =��7��
         * GameManager�� localClearInfo�� Ʃ�丮�� �������� �� 12���� Ŭ���� ���θ� ���� �迭( inspector �󿡼��� Ȯ�ΰ��� )
         * �� �迭�� �о �÷��̾ �� �������� ������� �ƴ��� �Ǻ�(localClearInfo[0]=true�� ù��° �������� ���� ����)        
         * �� ��ũ��Ʈ�� ��� ���ø� �����ֱ� ���� ��ũ��Ʈ�ϻ��̹Ƿ�, ����ų� ��ũ��Ʈ �̸��� �ٲٰų��ϼŵ� �˴ϴ�. 
        */
        //��� ����.  �Ͼ߶�Ű��  gameObject ���� �ű⿡ ��ũ��Ʈ �巡���ؼ� ���̱�
        Debug.Log("ù��° �������� Ŭ���� ����: " +GameManager.instance.localClearInfo[0]);
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
