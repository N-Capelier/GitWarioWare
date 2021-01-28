using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : TimedBehaviour
{
    public float speed;
    private float speedIncrementation;
    

    public override void Start()
    {
        base.Start(); //Do not erase this line!

        speed = 0f;

        switch (currentDifficulty)
        {
            case Difficulty.EASY:
                speedIncrementation = 25.5f;
                break;

            case Difficulty.MEDIUM:
                speedIncrementation = 24f;
                break;

            case Difficulty.HARD:
                speedIncrementation = 22.5f;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        speed -= (speed/2) * Time.deltaTime * 20f;
        speed = Mathf.Max(0f, speed);
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }

    public void PlayerInput()
    {
        speed += speedIncrementation;
    }

    public void PlayerStop()
    {
        speed = 0.4f;
    }

    public void PlayerEnd()
    {
        speed = 0;
        speedIncrementation = 0;
            
    }
}
