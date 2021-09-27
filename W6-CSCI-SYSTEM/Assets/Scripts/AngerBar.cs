using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerBar : MonoBehaviour
{
    [SerializeField] private float increaseExtent;
    [SerializeField] private float AngerBarIncreaseSpeed;
    [SerializeField] private float CrashIncreaseExtent;
    [SerializeField] private GameObject failPannel;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private float musicPitchIncreaseExtent;

    private bool isPiling=false;
    private bool TimerStart;
    
    private float timeCount;
    private float timeRecord;

    private bool canAddAnger;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AngerTimer
        if (!TimerStart)
        {
            timeRecord=Time.time;
            TimerStart = true;
            canAddAnger=false;
        }
        else if (TimerStart)
        {
            timeCount = Time.time;
            if (timeCount - timeRecord >= AngerBarIncreaseSpeed)
            {
                canAddAnger=true;
                TimerStart = false;
            }
        }

        if (transform.localScale.x >= 1)
        {
            failPannel.SetActive(true);
        }
        
        
    }

    public void addAnger()
    {
        transform.localScale += new Vector3(increaseExtent, 0, 0);
        backgroundMusic.pitch += musicPitchIncreaseExtent;
    }
    

    public bool CanAngerState()
    {
        return canAddAnger;
    }

    public void CrashAddAnger()
    {
        transform.localScale += new Vector3(CrashIncreaseExtent, 0, 0);
        backgroundMusic.pitch += musicPitchIncreaseExtent;
    }
}
