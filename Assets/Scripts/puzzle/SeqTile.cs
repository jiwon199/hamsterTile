using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqTile : MonoBehaviour
{
    //��ġ����
    public bool touched;
     
    Color alpha;
    private float moveSpeed;
    private float alphaSpeed;
    // Start is called before the first frame update
    void Start()
    {
        touched = false;
        alpha = this.GetComponent<SpriteRenderer>().color;
        moveSpeed = 1f;
        alphaSpeed = 7f;
       
    }

    // Update is called once per frame
    void Update()
    {
        //�ش�Ǵ� ���� ��ġ�Ǹ�
        if (touched)
        {
            //���� �Ʒ���..
            transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0));
            //���� �����ϰ�...
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
            this.GetComponent<SpriteRenderer>().color = alpha;
            //������ ���������� ����
            Invoke("DestroyObject", 0.4f);
        }

        
    }
    
    private void DestroyObject()
    {
      DestroyObject(gameObject);
    }
}
