using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqTile : MonoBehaviour
{
    //��ġ����
    public bool touched;
     //�ʱ���� ����
     public bool init;
    Color alpha;
    private float moveSpeed;
    
    SpriteRenderer spr;
    private float posY;
    public float time;
    public float revokeTime;
    public float touchTime;
 
    // Start is called before the first frame update
    void Start()
    {
        touched = false;
        init = true;
        time = 0f;
        alpha = this.GetComponent<SpriteRenderer>().color;
        moveSpeed = 1f;
      
        posY = gameObject.transform.position.y;
        spr = this.GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {

       
        //�ش�Ǵ� ���� ��ġ�Ǹ� 
        if (touched)
        {
            
            init = false;
            //���� �Ʒ���..             
            transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0));

            //���� �����ϰ�...
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * 10f);
            //  alpha.a -= (Time.deltaTime ) / alphaSpeed;     
           
              //������ ���������� ���߱�. 
              Invoke("HideObject", 0.2f);
            
        }
        //�ʱ���°� �ƴϸ鼭 ��ġ�� x=�ǵ������.. ���������ö󰡱�
        else if (!init && !touched)
        {
            
            if (gameObject.transform.position.y <= posY)
            {
                transform.Translate(new Vector3(0, +moveSpeed * Time.deltaTime, 0));
            }
            //alpha.a += (Time.deltaTime ) / alphaSpeed;
            
            alpha.a = Mathf.Lerp(alpha.a, 1, Time.deltaTime * 10f);
        }
       // SpriteRenderer spr = this.GetComponent<SpriteRenderer>();
        spr.color = alpha;

    }
    private void HideObject()
    {
        //gameObject.GetComponent<Renderer>().enabled = false;  //���߱�
       
        gameObject.SetActive(false);  //����
    }
     
    private void DestroyObject()
    {
      Destroy(gameObject);
    }
}
