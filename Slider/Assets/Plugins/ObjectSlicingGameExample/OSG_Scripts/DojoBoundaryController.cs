using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The DojoBoundary Class handles deactivating the ball objects when they enter the colliders of the GameController Parent Object.  It also
/// places the Red X's at the position of the lost ball in RegularGameMode Mode.  They are positioned just above the bottom of the screen.
/// </summary>
public class DojoBoundaryController : MonoBehaviour
{

    private Vector3 resetPosition = new Vector3(0, 0, 0);       // the position that the ball should be returned to after colliding with the boundary
    private int usedRedXs;                                      // this is an int that represents the number of "red x's we are on".  The red X's that spawn in regular mode above lost ball.
    public GameObject[] ballMissedX;                            // this is an array of GameObjects that holds the red X's
    public float redXHeight;                                    // this is the height that the red X's spawn at when a ball is lost.


    // OnTriggerEnter is called when the Collider other enters the trigger
    public void OnTriggerEnter(Collider other)
    {
        //if the other object that collides with us is a "Ball"
        if (other.CompareTag(Tags.ballTag))
        {
            //we create a variable named ballIntruder which we use to cache the reference to the "other" gameObject.
            GameObject ballIntruder = other.gameObject;

            //if the current selected gameMode is RegularGameMode Mode
            if (GameController.GameControllerInstance.gameModes == GameModes.RegularGameMode && GameVariables.BallsMissed < 3)
            {
                //if "BallsMissed" variable of our "GameVariables" Static class is less than 3
                if (GameVariables.BallsMissed < 3 && GameController.GameControllerInstance.gameIsRunning)
                {
                    //then... we access the first entry in our gameObject List (ballMissedX), and we set it's position to a new Vector3 ((same x value as ball , our redXHeight , same z value as ball))
                    ballMissedX[usedRedXs].transform.position = /*ballIntruder.transform.position +*/ new Vector3(ballIntruder.transform.position.x, redXHeight, ballIntruder.transform.position.z);
                    //then we set the ballMissedX[0 for first pass] redX to active.
                    ballMissedX[usedRedXs].SetActive(true);
                    //we increment the usedRedXs++;
                    usedRedXs++;

                }


            }

            //if we are NOT in RegularGameMode Mode

            //set the impacted ball inactive
            ballIntruder.SetActive(false);
            //then we take the ball and put it at position 0 , 0 , 0
            ballIntruder.transform.position = resetPosition;
            //we also set the rotation to rotation 0 , 0 , 0
            ballIntruder.transform.eulerAngles = resetPosition;
            //Ball Missed gets increased.
            GameVariables.BallsMissed++;
        }

        //if the object that collides with the boundary is a bomb... we want to disable it too
        if (other.CompareTag(Tags.bombTag))
        {
            //deactivate
            other.gameObject.SetActive(false);
        }
    }

}
