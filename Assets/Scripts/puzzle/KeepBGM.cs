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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
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
   
     
 
