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
        Debug.Log(selectScene.isSelecting);
        if(!selectScene.isSelecting){
            if((Playermove.isEnd && Input.GetKey(KeyCode.Return))||(Purple_Playermove.isEnd && Input.GetKey(KeyCode.Return))){
                Playermove.isEnd = false;
                Purple_Playermove.isEnd = false;
                SceneManager.LoadScene("Title");
            }
            else {
                if(Input.GetKey(KeyCode.Return)){
                    SceneManager.LoadScene("Select");
                }
            }
        }
        else{
            if(Input.GetKey(KeyCode.Alpha1)){
                Debug.Log("1번");
                selectScene.isSelecting = false;
                SceneManager.LoadScene("Green");
                
            }
            if(Input.GetKey(KeyCode.Alpha2)){
                selectScene.isSelecting = false;
                SceneManager.LoadScene("PurpleZero");
            }
        }
            


    }
}
