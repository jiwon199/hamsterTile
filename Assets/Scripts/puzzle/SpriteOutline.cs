using UnityEngine;


//[ExecuteInEditMode]
public class SpriteOutline : MonoBehaviour
{
    //Color color ;
    public bool lastTouch;
    //  [Range(0, 25)]
    //    public int outlineSize ;
    //Color alpha = new Color(0f, 0f, 0f, 255f);         
    public GameObject spt;
    private SpriteRenderer spriteRenderer;
    
    float time;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       // board = FindObjectOfType<Board>();
    }

    /*
   void OnEnable()
   {
       

       UpdateOutline(true);
   }

   void OnDisable()
   {
       UpdateOutline(false);
   }
   */
    void Update()
    {

        //Å×µÎ¸®°¡ ±ôºý±ôºýÇÏ°Ô..
        if (time < 0.5f)
        {
            spriteRenderer.color = new Color(0, 0, 0, 1 - time);
        }
        else
        {
            spriteRenderer.color = new Color(0, 0, 0, time);
            if (time > 1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
         
   //     if(!lastTouch) UpdateOutline(false);
   //     else UpdateOutline(true);

    }
 /*
    void UpdateOutline(bool outline)
    {
       
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", outlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }
  */
}

 
