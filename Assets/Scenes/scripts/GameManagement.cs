using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

    public static GameManagement instance = null;
    public Animator animator;
    private int levelToLoad;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this) {
            Destroy(this);
        }
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FadeToNextLevel();
        }
    }

    public void FadeToNextLevel()
    {
        levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        if (levelToLoad > SceneManager.sceneCount)
        {
            levelToLoad = 0;//SceneManager.sceneCount - 1;
        }
        FadeToLevel(levelToLoad);
    }

    public void FadeToLevel(int levelIndex)
    {
        Debug.Log("Load Scene " + levelToLoad.ToString());
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        Debug.Log("Loaded Scene " + levelToLoad.ToString());
        SceneManager.LoadScene(levelToLoad);
    }
}
