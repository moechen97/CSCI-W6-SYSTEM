using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholePassengerManager : MonoBehaviour
{
    [SerializeField] private float movingSpeed;
    [SerializeField] private float level1Buffer;
    [SerializeField] private float level2Buffer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getMovingSpeed()
    {
        return movingSpeed;
    }

    public float getLevel1Buffer()
    {
        return level1Buffer;
    }

    public float getLevel2Buffer()
    {
        return level2Buffer;
    }
}
