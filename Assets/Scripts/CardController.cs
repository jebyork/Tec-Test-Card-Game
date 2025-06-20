using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public bool isFaceUp = false;
    public bool canFlip = false;

    public void Flip(bool bByPlayer)
    {
        if (canFlip == false)
        {
            return;
        }
        if (bByPlayer)
        {
            canFlip = false;
        }
        
        if (isFaceUp)
            FaceDown();
        else
            FaceUp();
    }

    public void FaceUp()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        isFaceUp = true;
    }

    public void FaceDown()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        isFaceUp = false;
    }
}