using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KeepBGM : MonoBehaviour
{
    GameObject BackgroundMusic;
    AudioSource backmusic;
    public static KeepBGM instance;
    void Awake()
    {
        /*
         GameObject[] obj = GameObject.FindGameObjectsWithTag("sound");
         for (int i = 1; i < obj.Length; i++)
         {
             Destroy(obj[i]);
         }
        */
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    public void deleteInstance()
    {
        Destroy(instance);
    }
    public void preserveBgm()
    {
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        GameObject g=GameObject.Find("KeepBGM");
        
    }
    void Update()
    {
        
    }
}
   
     
 
