using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private float pressRtime = 2f;
   
    private bool isPressed = false;
    private float totalPressedTime = 0f;
    private bool canResetBall = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
       DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isPressed = true;
            totalPressedTime = 0f;
        }

        if (isPressed && Input.GetKey(KeyCode.R))
        {
            totalPressedTime += Time.deltaTime;
            if (totalPressedTime > pressRtime)
            {
                SceneManager.LoadScene(0);
            }

        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
}
