using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class firstStart : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(!GameManager.instance.localWatchStory) {
            if(Input.GetMouseButtonUp(0)){
                if (!transform.Find("speechbubble1").gameObject.activeSelf){
                    GameObject.Find("soundManager").GetComponent<soundManager>().BtnClick();
                    transform.Find("speechbubble0").gameObject.SetActive(false);
                    transform.Find("speechbubble1").gameObject.SetActive(true);
                }
                else if (transform.Find("speechbubble1").gameObject.activeSelf){
                    GameObject.Find("soundManager").GetComponent<soundManager>().BtnClick();
                    GameManager.instance.updateStoryShow();
                    GameObject.Find("firstAdvicePanel").SetActive(false);
                }
            }
        }
    }
}
