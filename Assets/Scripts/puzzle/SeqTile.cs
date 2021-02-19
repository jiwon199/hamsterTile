using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqTile : MonoBehaviour
{
    //터치여부
    public bool touched;
     //초기상태 여부
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

       
        //해당되는 색이 터치되면 
        if (touched)
        {
            
            init = false;
            //점점 아래로..             
            transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0));

            //점점 투명하게...
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * 10f);
            //  alpha.a -= (Time.deltaTime ) / alphaSpeed;     
           
              //완전히 투명해지면 감추기. 
              Invoke("HideObject", 0.2f);
            
        }
        //초기상태가 아니면서 터치가 x=되돌리기로.. 위로점점올라가기
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
        //gameObject.GetComponent<Renderer>().enabled = false;  //감추기
       
        gameObject.SetActive(false);  //끄기
    }
     
    private void DestroyObject()
    {
      Destroy(gameObject);
    }
}
