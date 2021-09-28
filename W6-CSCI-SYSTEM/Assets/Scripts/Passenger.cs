using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    [SerializeField] private int directionType;
    [SerializeField] private int Width;
    [SerializeField] private Vector3 movingDistanceAndDirection;

    private WholePassengerManager _PManager;
    float movingSpeed;
    float level1Buffer;
    float level2Buffer;
    
    
    private float timeCount;
    private float timeRecord;
    

    private bool canMove=false;
    private bool TimerStart=false;
    private bool managerHasSameDirection;
    private bool timeLetThisMove=true;

    private bool isCrashed=false;

    private bool isCount = false;

    
    private SpriteRenderer ChildSprite=null;

    private Triangle manager;
    private LevelManager _levelManager;
    private AngerBar _angerBar;

    private AudioSource successfulPassSound;
    
    // Start is called before the first frame update
    void Start()
    {
       
        if (transform.childCount != 0)
        {
            ChildSprite=transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        
        manager = FindObjectOfType<Triangle>();
        _levelManager = FindObjectOfType<LevelManager>();
        _angerBar = FindObjectOfType<AngerBar>();
        
        _PManager = FindObjectOfType<WholePassengerManager>();
        movingSpeed = _PManager.getMovingSpeed();
        level1Buffer = _PManager.getLevel1Buffer();
        level2Buffer = _PManager.getLevel2Buffer();

        successfulPassSound = FindObjectOfType<SuccessfullySound>().transform.GetComponent<AudioSource>();
        
        timeCount = 0;
        timeLetThisMove=true;
    }

    // Update is called once per frame
    void Update()
    {
        //Move Timer
        if (!TimerStart)
        {
            timeRecord=Time.time;
            TimerStart = true;
            timeLetThisMove=false;
        }
        else if (TimerStart)
        {
            timeCount = Time.time;
            if (timeCount - timeRecord >= movingSpeed)
            {
                timeLetThisMove=true;
                TimerStart = false;
            }
        }

        //judge Whether triangle let it move, 0 = vertical, 1 = horizontal
        if (directionType == 0)
        {
            managerHasSameDirection = manager.GetVerticalCanMoveState();
        }
        else if (directionType == 1)
        {
            managerHasSameDirection = manager.GetHorizontalCanMoveState();
        }

        
        
        //judge whether this passenger can move
        if (managerHasSameDirection)
        {
            canMove = true;
        }
        else if (!managerHasSameDirection)
        {
            canMove = false;
        }
        
        
        //judge whether there is passenger in front of itself
        if (!managerHasSameDirection && directionType == 0 && transform.position.y > 1.5f)
        {
            if (Width == 1)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,1f);
                Debug.DrawRay(transform.position, -Vector2.up, Color.green);
                if (hit.collider == null)
                {
                    //Debug.Log(transform.name+"no collider before");
                    canMove = true;
                }
                else
                {
                    //Debug.Log(transform.name+"there is collider before"+hit.collider.name);
                    canMove = false;
                }
            }
            else if (Width == 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,1f);

                Vector3 hit2Position = transform.position + Vector3.right;
                
                RaycastHit2D hit2 = Physics2D.Raycast(hit2Position, -Vector2.up,1f);
                
                Debug.DrawRay(hit2Position, -Vector2.up, Color.green);
                if (hit.collider == null&&hit2.collider==null)
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
            
        }
        else if (!managerHasSameDirection && directionType == 1 && transform.position.x >1.5f)
        {
            if (Width == 1)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left,1f);
            
            
                if (hit.collider == null)
                {
                    canMove = true;
                }
           
                else
                {
                    canMove = false;
                }
            }
            else if (Width == 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left,1f);
                
                Vector3 hit2Position = transform.position + Vector3.down;
                
                RaycastHit2D hit2 = Physics2D.Raycast(hit2Position, Vector2.left,1f);
            
            
                if (hit.collider == null && hit2.collider==null)
                {
                    canMove = true;
                }
               
                else
                {
                    canMove = false;
                }
            }
        }

        //already pass line can still move
        if (directionType == 0 && transform.position.y <= 1f)
        {
            canMove = true;
        }
        else if (directionType == 1 && transform.position.x <= 1f)
        {
            canMove = true;
        }
        
        //whether this car is crushed
        if (isCrashed)
        {
            canMove = false;
        }
        
        
        

        //movement
        if (canMove&&timeLetThisMove)
        {
            transform.position += movingDistanceAndDirection;
        }
        else
        {
           
        }

        //if piled up,add anger
        if (!canMove)
        {
            int levelState = _levelManager.levelState();
            if (levelState == 1)
            {
                if (directionType == 0 && transform.position.y>=level1Buffer)
                {
                    if (_angerBar.CanAngerState())
                    {
                        Debug.Log(transform.name+"add anger");
                        _angerBar.addAnger();
                    }
                }

                if (directionType == 1 && transform.position.x >= level1Buffer)
                {
                    if (_angerBar.CanAngerState())
                    {
                        Debug.Log(transform.name+"add anger");
                        _angerBar.addAnger();
                    }
                }
            
            }

            if (levelState == 2)
            {
                if (directionType == 0 && transform.position.y>=level2Buffer)
                {
                    Debug.Log(transform.name+"add anger");
                    if (_angerBar.CanAngerState())
                    {
                        _angerBar.addAnger();
                    }
                }

                if (directionType == 1 && transform.position.x >= level2Buffer)
                {
                    Debug.Log(transform.name+"add anger");
                    if (_angerBar.CanAngerState())
                    {
                        _angerBar.addAnger();
                    }
                }
            }
        }
       
        
        
        //if pass the crossroad level manager add one point
        if (!isCrashed&&!isCount)
        {
            float positionJudge = transform.position.y + transform.GetChild(0).transform.localScale.y;
            float positionJudge2 = transform.position.x + transform.GetChild(0).transform.localScale.x;
            if (directionType == 0  && positionJudge == -1f)
            {
                _levelManager.AddOneCount();
                successfulPassSound.Play();
                isCount = true;
            }
            else if (directionType == 1 && positionJudge2==-1f)
            {
                _levelManager.AddOneCount();
                successfulPassSound.Play();
                isCount = true;
            }
        }
       
        
        
        //Destroy itself if Go too far
        if (directionType == 0 && transform.position.y<-15f)
        {
            Destroy(this.gameObject);
        }
        else if (directionType == 1 && transform.position.x<-15f)
        {
            Destroy(this.gameObject);
        }
        
    }

    public void CrashHappended()
    {
        isCrashed = true;
    }

    public bool GetCanMoveState()
    {
        return canMove;
    }

  
}
