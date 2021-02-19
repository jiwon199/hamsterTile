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
    {
        
    }
    public void goToPuzzle()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName);
        stageNum= int.Parse(buttonName);
        SceneManager.LoadScene("puzzleScene");
        DontDestroyOnLoad(stageNumObject);
    }
    

}
