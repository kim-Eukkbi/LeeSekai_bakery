using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillUI : MonoBehaviour
{
    public MillUIMove millUIMove;

    public float startPos;
    public float endPos;

    void Update()
    {
        //나중에 InputManager를 만들자
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition.x;

            if(startPos < endPos)
            {
                millUIMove.MoveRight();
            }
            else if (startPos > endPos)
            {
                millUIMove.MoveLeft();
            }
        }
    }
}
