using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class stagenum : MonoBehaviour
{
    public static int stageNum;
    public GameObject stageNumObject;

    // Start is called before the first frame update
    void Start()
    {/*
        string nowbutton = EventSystem.current.currentSelectedGameObject.name;
        if (nowbutton == "1") { stageNum = 1; Debug.Log("1¹øÅ¬¸¯"); }
        else if (nowbutton == "2") stageNum = 2;
        */
    }
    public void goToPuzzle()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        stageNum= int.Parse(buttonName);
        SceneManager.LoadScene("puzzleScene");
        DontDestroyOnLoad(stageNumObject);
    }

}
