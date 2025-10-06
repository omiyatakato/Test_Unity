using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameScane : MonoBehaviour
{
    public GameObject Game_Set_Panel;
    public GameObject ResultPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ResultPanelOn()
    {
        ResultPanel.SetActive(true);
    }

}
