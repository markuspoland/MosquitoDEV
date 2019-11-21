using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static Swipe swipeDirection;

    public float minSwipeLength = 200f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public enum Swipe { None, Up, Down, Left, Right };



    void update()
    {

        DetectSwipe();
    }


    public void DetectSwipe()
    {



        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength)
                {
                    swipeDirection = Swipe.None;
                    return;
                }

                currentSwipe.Normalize();

                if (!(secondPressPos == firstPressPos))
                {
                    if (Mathf.Abs(currentSwipe.x) > Mathf.Abs(currentSwipe.y))
                    {
                        if (currentSwipe.x < 0)
                        {
                            Debug.Log("right");
                            swipeDirection = Swipe.Right;
                        }
                        else
                        {
                            Debug.Log("left");
                            swipeDirection = Swipe.Left;
                        }
                    }
                    else
                    {
                        if (currentSwipe.y < 0)
                        {
                            Debug.Log("down");
                        }
                        else
                        {
                            Debug.Log("up");
                        }
                    }
                }



            }
            else
            {
                swipeDirection = Swipe.None;
            }
        }

    }
}
