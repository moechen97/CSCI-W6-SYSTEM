using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelTwoCamera;
    [SerializeField] private int level1CompleteCount;
    [SerializeField] private int WholeLevelCount;
    [SerializeField] private GameObject FinishGameParticle;
    [SerializeField] private GameObject level1Buffer1;
    [SerializeField] private GameObject level1Buffer2;
    [SerializeField] private GameObject level2Buffer1;
    [SerializeField] private GameObject level2Buffer2;
    private int PassCarCount = 0;
    private bool islevel1 = true;
    private bool islevel2 = false;
    private bool isComplete = false;
    
    // Start is called before the first frame update
    void Start()
    {
        levelTwoCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PassCarCount);
        if (PassCarCount >= level1CompleteCount)
        {
            levelTwoCamera.SetActive(true);
            islevel1 = false;
            islevel2 = true;
           
            level1Buffer1.SetActive(false);
            level1Buffer2.SetActive(false);
            level2Buffer1.SetActive(true);
            level2Buffer2.SetActive(true);
        }
        
        if (PassCarCount >= WholeLevelCount&&!isComplete)
        {
            Instantiate(FinishGameParticle, transform.position, quaternion.identity);
            isComplete = true;
        }
    }

    public void AddOneCount()
    {
        PassCarCount++;
    }

    public int levelState()
    {
        if (islevel1)
        {
            return 1;
        }
        else if (islevel2)
        {
            return 2;
        }
        else
        {
            return 0;
        }
        
    }
}
