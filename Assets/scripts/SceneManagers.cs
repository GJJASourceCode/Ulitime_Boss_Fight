using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey (KeyCode.Return)){
            if(Playermove.isEnd){
                SceneManager.LoadScene("Title");
                Playermove.isEnd = false;
            }
            else{
                SceneManager.LoadScene("Green");
            }


        }
    }
}
