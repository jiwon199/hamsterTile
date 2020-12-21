using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqTile : MonoBehaviour
{
    //터치여부
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
        //해당되는 색이 터치되면
        if (touched)
        {
            //점점 아래로..
            transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0));
            //점점 투명하게...
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
            this.GetComponent<SpriteRenderer>().color = alpha;
            //완전히 투명해지면 삭제
            Invoke("DestroyObject", 0.4f);
        }

        
    }
    
    private void DestroyObject()
    {
      DestroyObject(gameObject);
    }
}
