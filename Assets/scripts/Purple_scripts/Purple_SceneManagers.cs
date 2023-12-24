using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Purple_SceneManagers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey (KeyCode.Return)){
            if(Purple_Playermove.isEnd){
                SceneManager.LoadScene("Title");
                Purple_Playermove.isEnd = false;
            }
            else{
                SceneManager.LoadScene("Purple");
            }


        }
    }
}
