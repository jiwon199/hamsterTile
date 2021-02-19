using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bangEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //bang애니메이션이 끝나면 bang오브젝트 파괴
    void Destory(){
        Destroy(this.gameObject);
    }
}
