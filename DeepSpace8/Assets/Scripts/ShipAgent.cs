using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

// The Space Ship Agent class that contains the reward structure, observations, actions, and movement
public class ShipAgent : Agent
{
    public float movementSpeed;  // Configurable fwd movement speed from the Unity editor
    public float turnSpeed; // Configurable turn speed from the Unity editor

    public Transform Target; // Target Space Station Transform reference
    public Transform[] Asteroids; // Asteroids in 1st level
    public Transform[] Asteroids2; // Asteroids in 2nd level
    public Transform[] Planets; // Planets (currently not utilized)
    public Transform[] Galaxies; // Galaxies (currently not utilized)
    public GameObject playerScoreText; // Space Ship score gameobject on Score Board
    public GameObject numRunsText; // Number of Runs gameobject on Score Board

    Rigidbody rBody; // Rigidbody of the Space Ship

    private Vector3 defPos; // position variable of the space ship, used for space ship position reset
    private Quaternion defRot; // rotation variable of the space ship, used for space ship rotation reset
    private Vector3 defTargetPos; // position variable of the space station
    private Quaternion defTargetRot; // rotation variable of the space station

    //private float forwardVelocityReward = 10f; // magnitude factor for FWD velocity reward (currently not utilized)
    private float rewardStep = -1f / 10000; // additive reward for each time step
    private float rewardGoal = 4000f; // reward for reaching space station
    private float rewardCollision = -2f; // reward for colliding with asteroid
    private float rewardBoundary = -2f; // reward for colliding with boundary
    private float distanceToTarget; // variable that tracks distance to space station
    private float originalDistanceToTarget = 2000f; // starting distance from space ship to space station
    private float spaceShuttleScore = 0f; // initiliazes space shuttle score to 0
    private float runs = 0f; // initiliazes runs to 0

    // Method that is run at the beginning of running the Unity scene
    void Start()
    {
        defPos = new Vector3(0,0,0); // initialize to 0
        defRot = transform.localRotation; //  captures initial rotation
        defTargetPos = Target.transform.localPosition; // captures initial space station position
        defTargetRot = Target.transform.localRotation; // captures initial space station rotation

        rBody = GetComponent<Rigidbody>(); // gets space ship rigidbody

        playerScoreText.GetComponent<TextMesh>().text = spaceShuttleScore.ToString(); // initializes player score on Unity screen
        numRunsText.GetComponent<TextMesh>().text = runs.ToString(); // initalizes runs number on Unity screen
    }

    // Resets the Agent after each episode and sets its position to its original position
    public void AgentReset()
    {
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
        transform.localPosition = defPos;
        transform.localRotation = defRot;
    }

    // Resets the Target space station to a randomized position.  Currently not being utilized.
    public void TargetReset()
    {
        int randX = Random.Range(-250, 250);
        int randY = Random.Range(-250, 250);
        int randZ = Random.Range(-250, 250);

        Target.transform.localPosition = new Vector3(randX, randY, randZ);
    }

    // Resets the 1st level of asteroids to randomly generated positions.
    public void ObstacleReset()
    {
        foreach (Transform asteroid in Asteroids)
        {
            int randX = Random.Range(-350, 350);
            int randY = Random.Range(-350, 350);
            int randZ = Random.Range(-350, 350);

            asteroid.transform.localPosition = new Vector3(randX, randY, randZ);
        }

        foreach (Transform planet in Planets) // currently not utilized
        {
            int randX = Random.Range(-350, 350);
            int randY = Random.Range(-350, 350);
            int randZ = Random.Range(-350, 350);

            planet.transform.localPosition = new Vector3(randX, randY, randZ);
        }
    }

    // Resets the 2nd level of asteroids to randomly generated positions.
    public void ObstacleReset2()
    {
        foreach (Transform asteroid in Asteroids2)
        {
            int randX = Random.Range(-350, 350);
            int randY = Random.Range(-350, 350);
            int randZ = Random.Range(-350, 350);

            asteroid.transform.localPosition = new Vector3(randX, randY, randZ);
        }

        foreach (Transform planet in Planets) // currently not utilized
        {
            int randX = Random.Range(-350, 350);
            int randY = Random.Range(-350, 350);
            int randZ = Random.Range(-350, 350);

            planet.transform.localPosition = new Vector3(randX, randY, randZ);
        }
    }

    // Runs at the start of each episode. It resets the Agent and the Obstacles.
    public override void OnEpisodeBegin() 
    {
        AgentReset();
        ObstacleReset();
        ObstacleReset2();
    }

    // Collects the Observations at each step in the episode
    public override void CollectObservations(VectorSensor sensor)
    {
        // Current distance from the space ship to the space station
        distanceToTarget = Vector3.Distance(transform.localPosition, Target.localPosition);

        sensor.AddObservation(Target.localPosition); // 3 floats that capture the space station position
        sensor.AddObservation(transform.localPosition); // 3 floats that capture the space ship position
        sensor.AddObservation(distanceToTarget); // 1 float that captures the current distance to target
        sensor.AddObservation(rBody.velocity); // 3 floats that capture the space ship velocity in x, y, z
        sensor.AddObservation(rBody.angularVelocity);    // 3 floats that capture the space ship angular velcocity in x, y, z
    }

    // After collecting the observations, the Agents runs an action from the vector Action (continuos variable between -1 and 1).
    // This method runs at each of these actions and performs the action on the environment and assigns the reward
    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(rewardStep); // additive reward that pushes the Agent to be quicker in getting to the Target

        // Scaled reward for the space ships's current distance to Target. Only provided if the space ship is moving forward.
        if (rBody.velocity.z > 0)
        {
            AddReward((originalDistanceToTarget - distanceToTarget)/ originalDistanceToTarget);

        }
        
        // Calls the Boundary Check function which checks if the Agent has hit a boundary.
        BoundaryCheck();

        float thrust = vectorAction[0]; // fwd ship acceleration action
        float pitch = vectorAction[1]; // rotation in pitch action
        float roll = vectorAction[2]; // rotation in roll action

        // These 3 methods apply a forward acceleratio and rotation to the space ship according to the input
        // vector action.
        rBody.AddForce(transform.forward * thrust * movementSpeed, ForceMode.VelocityChange);
        rBody.AddTorque(transform.right * pitch * turnSpeed, ForceMode.VelocityChange);
        rBody.AddTorque(transform.forward * -roll * turnSpeed, ForceMode.VelocityChange);       
    }

    // Checks if the Agent has hit a boundary. If so, a negative reward is set for the episode and the episode
    // resets. In addition, the score screen is updated.
    void BoundaryCheck()
    {
        if (transform.localPosition.x < -300)
        {
            SetReward(rewardBoundary);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (transform.localPosition.x > 300)
        {
            SetReward(rewardBoundary);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (transform.localPosition.y < -300)
        {
            SetReward(rewardBoundary);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (transform.localPosition.y > 300)
        {
            SetReward(rewardBoundary);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (transform.position.z < -10)
        {
            SetReward(rewardBoundary);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (transform.position.z > 1999)
        {
            SetReward(rewardBoundary);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
    }

    // This method is only called upon if a collision is triggered from the space ship. The episode is ended upon collision.
    // If the trigger tag shows as the space station 'goal', then a positive reward is provided. Otherwise a negative reward
    // is set. In addition, the score screen is updated.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("goal"))
        {
            SetReward(rewardGoal);
            spaceShuttleScore = spaceShuttleScore + 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("asteroid"))
        {
            SetReward(rewardCollision);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("smallAsteroid"))
        {
            SetReward(rewardCollision);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("bigAsteroid"))
        {
            SetReward(rewardCollision);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("planet"))
        {
            SetReward(rewardCollision);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("sun"))
        {
            SetReward(rewardCollision);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("supernova"))
        {
            SetReward(rewardCollision);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("galaxy"))
        {
            SetReward(rewardCollision);
            spaceShuttleScore = spaceShuttleScore - 1f;
            runs = runs + 1f;

            ChangeScoreScreen();

            EndEpisode();
        }
    }

    // Heuristic function that is only run if the user would like to test the simulation themselves.
    // The user then takes over the controls (via ActionsOut) and flies the space ship through the simulation.
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Pitch");
        actionsOut[2] = Input.GetAxis("Roll");
    }

    // Updates the score screen and also resets both values to 0 if 5000 runs has been reached.
    public void ChangeScoreScreen()
    {
        if (runs < 5000)
        {
            numRunsText.GetComponent<TextMesh>().text = runs.ToString();
            playerScoreText.GetComponent<TextMesh>().text = spaceShuttleScore.ToString();
        }
        else
        {
            runs = 0f;
            spaceShuttleScore = 0f;

            numRunsText.GetComponent<TextMesh>().text = runs.ToString();
            playerScoreText.GetComponent<TextMesh>().text = spaceShuttleScore.ToString();
        }
    }
}
