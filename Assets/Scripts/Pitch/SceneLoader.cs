using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader instance;

    /*
     * THIS IS NOT PERMANENT CHANGE THIS AFTER PITCH
     */
    public Slider player1HP;
    public Slider player2HP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * THIS IS NOT PERMANENT CHANGE THIS AFTER PITCH
         */
        if (player1HP.value <= 0 && player2HP.value <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
