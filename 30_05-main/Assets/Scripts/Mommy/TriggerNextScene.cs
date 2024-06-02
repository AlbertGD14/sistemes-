using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextScene : MonoBehaviour
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
        if (other.CompareTag("Mummy"))
        {
            SceneManager.UnloadSceneAsync("Mummy_Scene");
            SceneManager.LoadScene("Platform", LoadSceneMode.Additive);
        }
    }
}
