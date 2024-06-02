using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Scene scene = SceneManager.GetSceneByName("Outside");
            if (scene.isLoaded)
            {
                SceneManager.UnloadScene("Outside");
            }
            else
            {
                SceneManager.LoadScene("Outside", LoadSceneMode.Additive);
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {


            Scene scene = SceneManager.GetSceneByName("Corridor_Scene");
            if (scene.isLoaded)
            {
                SceneManager.UnloadScene("Corridor_Scene");
            }
            else
            {
                SceneManager.LoadScene("Corridor_Scene", LoadSceneMode.Additive);
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            
            Scene scene = SceneManager.GetSceneByName("Mummy_Scene");
            if (scene.isLoaded)
            {
                SceneManager.UnloadScene("Mummy_Scene");
            }
            else
            {   
                SceneManager.LoadScene("Mummy_Scene", LoadSceneMode.Additive);
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Scene scene = SceneManager.GetSceneByName("Platform");
            if (scene.isLoaded)
            {
                SceneManager.UnloadScene("Platform");
            }
            else
            {
                SceneManager.LoadScene("Platform", LoadSceneMode.Additive);
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Scene scene = SceneManager.GetSceneByName("Puzzle");
            if (scene.isLoaded)
            {
                SceneManager.UnloadScene("Puzzle");
            }
            else
            {
                SceneManager.LoadScene("Puzzle", LoadSceneMode.Additive);
            }

        }
    }
}
