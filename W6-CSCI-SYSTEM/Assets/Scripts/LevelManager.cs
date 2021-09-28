using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelOneCamera;
    [SerializeField] private float CameraTransferSpeed;
    [SerializeField] private int level1CompleteCount;
    [SerializeField] private int WholeLevelCount;
    [SerializeField] private GameObject FinishGameParticle;
    [SerializeField] private GameObject level1Buffer1;
    [SerializeField] private GameObject level1Buffer2;
    [SerializeField] private GameObject level2Buffer1;
    [SerializeField] private GameObject level2Buffer2;
    [SerializeField] private AudioSource CompleteSound;
    [SerializeField] private GameObject verticalPassengerP;
    [SerializeField] private GameObject horizontalPassengerP;
    [SerializeField] private GameObject WinPanel;
    
    private int PassCarCount = 0;
    private bool islevel1 = true;
    private bool islevel2 = false;
    private bool isComplete = false;

    private bool AlreadyChangeLevel = false;
    private int wholePassenger = 0;

    private CinemachineVirtualCamera _cinemachineVirtual;
    
    // Start is called before the first frame update
    void Start()
    {
        _cinemachineVirtual = levelOneCamera.GetComponent<CinemachineVirtualCamera>();
        wholePassenger = verticalPassengerP.transform.childCount + horizontalPassengerP.transform.childCount;
        Debug.Log("whole passenger"+wholePassenger);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PassCarCount);
        if (PassCarCount >= level1CompleteCount && !AlreadyChangeLevel)
        {
            if (_cinemachineVirtual.m_Lens.OrthographicSize < 11.19f)
            {
                _cinemachineVirtual.m_Lens.OrthographicSize +=CameraTransferSpeed*Time.deltaTime;
            }
            else if (_cinemachineVirtual.m_Lens.OrthographicSize >= 11.19f)
            {
                AlreadyChangeLevel = true;
            }
            
            
            islevel1 = false;
            islevel2 = true;
           
            level1Buffer1.SetActive(false);
            level1Buffer2.SetActive(false);
            level2Buffer1.SetActive(true);
            level2Buffer2.SetActive(true);
        }
        
        if (PassCarCount >= wholePassenger&&!isComplete)
        {
            CompleteSound.Play();
            Instantiate(FinishGameParticle, transform.position, quaternion.identity);
            Invoke("winPanelAppear",4f);
            isComplete = true;
        }
    }


    void winPanelAppear()
    {
        WinPanel.SetActive(true);
    }
    
    public void AddOneCount()
    {
        PassCarCount+=1;
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
