using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleCamera : MonoBehaviour
{
    public Camera cm;
    public Board board;
    // Start is called before the first frame update
    void Start()
    {
    
        float width = (float)board.m_Width/2.0f - 1.5f;
        float height = (float)board.m_Height/ 2.0f - 1.5f;
        //�׽�Ʈ�÷��̽� ��������� �ٷ� ������ ���
        if (width < 0 || height < 0)
        {
            width = width + 1.5f;
            height = height + 1.5f;
        }
        transform.Translate(new Vector3( width,height, 0));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
