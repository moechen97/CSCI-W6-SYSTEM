using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _horivontalStopLine;
    [SerializeField] private SpriteRenderer VerticalStopLine;
    [SerializeField] private Color CanPassColor;
    [SerializeField] private Color StopColor;
    
    private bool horizontalCanPass = false;
    private bool verticalCanPass = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if (verticalCanPass)
        {
            VerticalStopLine.color = CanPassColor;
            _horivontalStopLine.color = StopColor;
            
        }
        else if (horizontalCanPass)
        {
            _horivontalStopLine.color = CanPassColor;
            VerticalStopLine.color = StopColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (verticalCanPass)
            {
                transform.rotation = Quaternion.Euler(0f,0f,90f);
                horizontalCanPass = true;

                _horivontalStopLine.color = CanPassColor;
                VerticalStopLine.color = StopColor;
                
                verticalCanPass = false;
            }
            else if (horizontalCanPass)
            {
                transform.rotation = Quaternion.Euler(0f,0f,180f);
                verticalCanPass = true;
                
                VerticalStopLine.color = CanPassColor;
                _horivontalStopLine.color = StopColor;
                
                horizontalCanPass = false;
            }
            
        }
    }

    public bool GetHorizontalCanMoveState()
    {
        return horizontalCanPass;
    }
    
    public bool GetVerticalCanMoveState()
    {
        return verticalCanPass;
    }
}
