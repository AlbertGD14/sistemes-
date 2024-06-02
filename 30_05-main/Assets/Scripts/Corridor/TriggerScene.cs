using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour
{
    private Collider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            SceneManager.UnloadSceneAsync("Corridor_Scene");
            SceneManager.LoadScene("Puzzle", LoadSceneMode.Additive);
        }
    }
}
