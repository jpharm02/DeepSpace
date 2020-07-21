using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SCRIPT IS LEFT IN THIS PROJECT FOR FUTURE WORK ONLY.  CURRENTLY NOT UTILIZED IN SOLUTION.
public class LevelGeneration : MonoBehaviour
{
    public GameObject startingRoom;
    public GameObject[] rooms; 
    public GameObject[] finalRooms;
    public GameObject Target;

    private int direction;
    private int pathChoice;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom; // 0.1f

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;
    public bool stopGeneration;
    public bool firstRoom;

    public LayerMask room;

    public void CleanLevelArea()
    {
        GameObject[] roomsToRemove = GameObject.FindGameObjectsWithTag("theRoom");
        GameObject[] finalRoomsToRemove = GameObject.FindGameObjectsWithTag("finalRoom");
        GameObject startingRoomToRemove = GameObject.FindGameObjectWithTag("startingRoom");

        foreach (GameObject room in roomsToRemove)
        {
            Destroy(room);
        }

        foreach (GameObject room in finalRoomsToRemove)
        {
            Destroy(room);
        }

        Destroy(startingRoomToRemove);

        firstRoom = true;
        direction = Random.Range(1, 60);
        pathChoice = Random.Range(1, 9);

        transform.position = new Vector3(0,0,0);
    }

    private void Start()
    {
        Debug.Log("Generating Levels");

        CleanLevelArea();
    }

    private void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
        {
            if (pathChoice == 1)
            {
                MoveUpFWDRight();
                timeBtwRoom = startTimeBtwRoom;
            }
            else if (pathChoice == 2)
            {

                MoveUpFWDLeft();
                timeBtwRoom = startTimeBtwRoom;
            }
            else if (pathChoice == 3)
            {
                MoveDownFWDRight();
                timeBtwRoom = startTimeBtwRoom;
            }
            else if (pathChoice == 4)
            {
                MoveDownFWDLeft();
                timeBtwRoom = startTimeBtwRoom;
            }
            else if (pathChoice == 5)
            {
                MoveUpBackRight();
                timeBtwRoom = startTimeBtwRoom;
            }
            else if (pathChoice == 6)
            {
                MoveUpBackLeft();
                timeBtwRoom = startTimeBtwRoom;
            }
            else if (pathChoice == 7)
            {
                MoveDownBackRight();
                timeBtwRoom = startTimeBtwRoom;
            }
            else if (pathChoice == 8)
            {
                MoveDownBackLeft();
                timeBtwRoom = startTimeBtwRoom;
            }
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;

            Target.transform.position = transform.position;
        }      
    }

    private void MoveUpFWDRight()
    {
        if (direction >= 1 && direction <= 16) // direction 1 -- right
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos5 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos5;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 17 || direction == 18);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 17 || direction == 18) // direction 2 -- left
        {
            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos14 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos14;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 1 && direction <= 16);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 19 && direction <= 34) // direction 3 -- up
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos20 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
                transform.position = newPos20;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 52 || direction == 53);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 35 && direction <= 49) // direction 4 -- forward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos22 = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
                transform.position = newPos22;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 50 || direction == 51);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 50 || direction == 51) // direction 5 -- backward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
            {
                Vector3 newPos23 = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos23;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 35 && direction <= 49);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 52 || direction == 53) // direction 6 -- down
        {
            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
            {
                Vector3 newPos25 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
                transform.position = newPos25;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 19 && direction <= 34);
            }
            else
            {
                stopGeneration = true;
            }
        }

        //Collider[] roomDetection = Physics.OverlapSphere(transform.position, 50, room);
        if (firstRoom == true)
        {
            Instantiate(startingRoom, transform.position, Quaternion.identity);
        }

        firstRoom = false;

        if (stopGeneration == false)
        {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
        }
        else if (stopGeneration == true)
        {
            int rand = Random.Range(0, finalRooms.Length);
            Instantiate(finalRooms[rand], transform.position, Quaternion.identity);
        }
    }

    private void MoveUpFWDLeft()
    {
        if (direction >= 1 && direction <= 16) // direction 1 -- left
        {
            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos14 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos14;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 17 || direction == 18);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 17 || direction == 18) // direction 2 -- right
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos5 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos5;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 1 && direction <= 16);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 19 && direction <= 34) // direction 3 -- up
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos20 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
                transform.position = newPos20;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 52 || direction == 53);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 35 && direction <= 49) // direction 4 -- forward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos22 = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
                transform.position = newPos22;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 50 || direction == 51);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 50 || direction == 51) // direction 5 -- backward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
            {
                Vector3 newPos23 = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos23;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 35 && direction <= 49);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 52 || direction == 53) // direction 6 -- down
        {
            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
            {
                Vector3 newPos25 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
                transform.position = newPos25;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 19 && direction <= 34);
            }
            else
            {
                stopGeneration = true;
            }
        }

        Collider[] roomDetection = Physics.OverlapSphere(transform.position, 50, room);

        if (firstRoom == true)
        {
            Instantiate(startingRoom, transform.position, Quaternion.identity);
        }

        firstRoom = false;

        if (stopGeneration == false)
        {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
        }
        else if (stopGeneration == true)
        {
            int rand = Random.Range(0, finalRooms.Length);
            Instantiate(finalRooms[rand], transform.position, Quaternion.identity);
        }
    }

    private void MoveDownFWDRight()
    {
        if (direction >= 1 && direction <= 16) // direction 1 -- right
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos5 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos5;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 17 || direction == 18);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 17 || direction == 18) // direction 2 -- left
        {
            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos14 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos14;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 1 && direction <= 16);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 19 && direction <= 34) // direction 3 -- down
        {
            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
            {
                Vector3 newPos25 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
                transform.position = newPos25;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 52 || direction == 53);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 35 && direction <= 49) // direction 4 -- forward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos22 = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
                transform.position = newPos22;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 50 || direction == 51);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 50 || direction == 51) // direction 5 -- backward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
            {
                Vector3 newPos23 = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos23;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 35 && direction <= 49);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 52 || direction == 53) // direction 6 -- up
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos20 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
                transform.position = newPos20;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 19 && direction <= 34);
            }
            else
            {
                stopGeneration = true;
            }
        }

        // Collider[] roomDetection = Physics.OverlapSphere(transform.position, 50, room);

        if (firstRoom == true)
        {
            Instantiate(startingRoom, transform.position, Quaternion.identity);
        }

        firstRoom = false;

        if (stopGeneration == false)
        {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
        }
        else if (stopGeneration == true)
        {
            int rand = Random.Range(0, finalRooms.Length);
            Instantiate(finalRooms[rand], transform.position, Quaternion.identity);
        }
    }

    private void MoveDownFWDLeft()
    {
        if (direction >= 1 && direction <= 16) // direction 1 -- left
        {
            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos14 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos14;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 17 || direction == 18);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 17 || direction == 18) // direction 2 -- right
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos5 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos5;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 1 && direction <= 16);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 19 && direction <= 34) // direction 3 -- down
        {
            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
            {
                Vector3 newPos25 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
                transform.position = newPos25;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 52 || direction == 53);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 35 && direction <= 49) // direction 4 -- forward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos22 = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
                transform.position = newPos22;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 50 || direction == 51);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 50 || direction == 51) // direction 5 -- backward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
            {
                Vector3 newPos23 = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos23;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 35 && direction <= 49);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 52 || direction == 53) // direction 6 -- up
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos20 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
                transform.position = newPos20;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 19 && direction <= 34);
            }
            else
            {
                stopGeneration = true;
            }
        }

        //Collider[] roomDetection = Physics.OverlapSphere(transform.position, 50, room);

        if (firstRoom == true)
        {
            Instantiate(startingRoom, transform.position, Quaternion.identity);
        }

        firstRoom = false;

        if (stopGeneration == false)
        {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
        }
        else if (stopGeneration == true)
        {
            int rand = Random.Range(0, finalRooms.Length);
            Instantiate(finalRooms[rand], transform.position, Quaternion.identity);
        }
    }

    private void MoveUpBackRight()
    {
        if (direction >= 1 && direction <= 16) // direction 1 -- right
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos5 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos5;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 17 || direction == 18);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 17 || direction == 18) // direction 2 -- left
        {
            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos14 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos14;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 1 && direction <= 16);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 19 && direction <= 34) // direction 3 -- up
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos20 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
                transform.position = newPos20;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 52 || direction == 53);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 35 && direction <= 49) // direction 4 -- backward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
            {
                Vector3 newPos23 = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos23;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 50 || direction == 51);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 50 || direction == 51) // direction 5 -- forward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos22 = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
                transform.position = newPos22;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 35 && direction <= 49);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 52 || direction == 53) // direction 6 -- down
        {
            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
            {
                Vector3 newPos25 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
                transform.position = newPos25;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 19 && direction <= 34);
            }
            else
            {
                stopGeneration = true;
            }
        }

        //Collider[] roomDetection = Physics.OverlapSphere(transform.position, 50, room);

        if (firstRoom == true)
        {
            Instantiate(startingRoom, transform.position, Quaternion.identity);
        }

        firstRoom = false;

        if (stopGeneration == false)
        {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
        }
        else if (stopGeneration == true)
        {
            int rand = Random.Range(0, finalRooms.Length);
            Instantiate(finalRooms[rand], transform.position, Quaternion.identity);
        }
    }

    private void MoveUpBackLeft()
    {
        if (direction >= 1 && direction <= 16) // direction 1 -- left
        {
            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos14 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos14;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 17 || direction == 18);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 17 || direction == 18) // direction 2 -- right
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos5 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos5;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 1 && direction <= 16);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 19 && direction <= 34) // direction 3 -- up
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos20 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
                transform.position = newPos20;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 52 || direction == 53);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 35 && direction <= 49) // direction 4 -- backward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
            {
                Vector3 newPos23 = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos23;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 50 || direction == 51);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 50 || direction == 51) // direction 5 -- forward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos22 = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
                transform.position = newPos22;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 35 && direction <= 49);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 52 || direction == 53) // direction 6 -- down
        {
            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
            {
                Vector3 newPos25 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
                transform.position = newPos25;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 19 && direction <= 34);
            }
            else
            {
                stopGeneration = true;
            }
        }

        //Collider[] roomDetection = Physics.OverlapSphere(transform.position, 50, room);

        if (firstRoom == true)
        {
            Instantiate(startingRoom, transform.position, Quaternion.identity);
        }

        firstRoom = false;

        if (stopGeneration == false)
        {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
        }
        else if (stopGeneration == true)
        {
            int rand = Random.Range(0, finalRooms.Length);
            Instantiate(finalRooms[rand], transform.position, Quaternion.identity);
        }
    }

    private void MoveDownBackRight()
    {
        if (direction >= 1 && direction <= 16) // direction 1 -- right
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos5 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos5;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 17 || direction == 18);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 17 || direction == 18) // direction 2 -- left
        {
            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos14 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos14;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 1 && direction <= 16);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 19 && direction <= 34) // direction 3 -- down
        {
            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
            {
                Vector3 newPos25 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
                transform.position = newPos25;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 52 || direction == 53);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 35 && direction <= 49) // direction 4 -- backward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
            {
                Vector3 newPos23 = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos23;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 50 || direction == 51);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 50 || direction == 51) // direction 5 -- forward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos22 = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
                transform.position = newPos22;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 35 && direction <= 49);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 52 || direction == 53) // direction 6 -- up
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos20 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
                transform.position = newPos20;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 19 && direction <= 34);
            }
            else
            {
                stopGeneration = true;
            }
        }

        //Collider[] roomDetection = Physics.OverlapSphere(transform.position, 50, room);

        if (firstRoom == true)
        {
            Instantiate(startingRoom, transform.position, Quaternion.identity);
        }

        firstRoom = false;

        if (stopGeneration == false)
        {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
        }
        else if (stopGeneration == true)
        {
            int rand = Random.Range(0, finalRooms.Length);
            Instantiate(finalRooms[rand], transform.position, Quaternion.identity);
        }
    }

    private void MoveDownBackLeft()
    {
        if (direction >= 1 && direction <= 16) // direction 1 -- left
        {
            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos14 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos14;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 17 || direction == 18);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 17 || direction == 18) // direction 2 -- right
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos5 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos5;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 1 && direction <= 16);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 19 && direction <= 34) // direction 3 -- down
        {
            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
            {
                Vector3 newPos25 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
                transform.position = newPos25;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 52 || direction == 53);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction >= 35 && direction <= 49) // direction 4 -- backward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
            {
                Vector3 newPos23 = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
                transform.position = newPos23;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction == 50 || direction == 51);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 50 || direction == 51) // direction 5 -- forward
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos22 = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
                transform.position = newPos22;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 35 && direction <= 49);
            }
            else
            {
                stopGeneration = true;
            }
        }
        else if (direction == 52 || direction == 53) // direction 6 -- up
        {
            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
            {
                Vector3 newPos20 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
                transform.position = newPos20;

                do
                {
                    direction = Random.Range(1, 54);
                }
                while (direction >= 19 && direction <= 34);
            }
            else
            {
                stopGeneration = true;
            }
        }

        //Collider[] roomDetection = Physics.OverlapSphere(transform.position, 50, room);

        if (firstRoom == true)
        {
            Instantiate(startingRoom, transform.position, Quaternion.identity);
        }

        firstRoom = false;

        if (stopGeneration == false)
        {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
        }
        else if (stopGeneration == true)
        {
            int rand = Random.Range(0, finalRooms.Length);
            Instantiate(finalRooms[rand], transform.position, Quaternion.identity);
        }
    }


    //private void FullMove()
    //{
    //    switch (direction)
    //    {
    //        case 1: // MOVE RIGHT 1
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos1 = new Vector3(transform.position.x + moveAmount, transform.position.y + moveAmount, transform.position.z + moveAmount);
    //                transform.position = newPos1;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 2: // MOVE RIGHT 2
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos2 = new Vector3(transform.position.x + moveAmount, transform.position.y + moveAmount, transform.position.z);
    //                transform.position = newPos2;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 3: // MOVE RIGHT 3
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
    //            {
    //                Vector3 newPos3 = new Vector3(transform.position.x + moveAmount, transform.position.y + moveAmount, transform.position.z - moveAmount);
    //                transform.position = newPos3;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 4: // MOVE RIGHT 4
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos4 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z + moveAmount);
    //                transform.position = newPos4;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 5: // MOVE RIGHT 5
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos5 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
    //                transform.position = newPos5;

    //                // create a random direction for next move
    //                // make sure it does not go backwards here by only allowing for random generation of numbers that go forwards
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 6: // MOVE RIGHT 6
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
    //            {
    //                Vector3 newPos6 = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z - moveAmount);
    //                transform.position = newPos6;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 7: // MOVE RIGHT 7
    //            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos7 = new Vector3(transform.position.x + moveAmount, transform.position.y - moveAmount, transform.position.z + moveAmount);
    //                transform.position = newPos7;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 8: // MOVE RIGHT 8
    //            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos8 = new Vector3(transform.position.x + moveAmount, transform.position.y - moveAmount, transform.position.z);
    //                transform.position = newPos8;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 9: // MOVE RIGHT 9
    //            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z > minZ)
    //            {
    //                Vector3 newPos9 = new Vector3(transform.position.x + moveAmount, transform.position.y - moveAmount, transform.position.z - moveAmount);
    //                transform.position = newPos9;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 10: // MOVE LEFT 1
    //            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos10 = new Vector3(transform.position.x - moveAmount, transform.position.y + moveAmount, transform.position.z + moveAmount);
    //                transform.position = newPos10;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 11: // MOVE LEFT 2
    //            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos11 = new Vector3(transform.position.x - moveAmount, transform.position.y + moveAmount, transform.position.z);
    //                transform.position = newPos11;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 12: // MOVE LEFT 3
    //            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z > minZ)
    //            {
    //                Vector3 newPos12 = new Vector3(transform.position.x - moveAmount, transform.position.y + moveAmount, transform.position.z - moveAmount);
    //                transform.position = newPos12;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 13: // MOVE LEFT 4
    //            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos13 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z + moveAmount);
    //                transform.position = newPos13;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 14: // MOVE LEFT 5
    //            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos14 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
    //                transform.position = newPos14;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 15: // MOVE LEFT 6
    //            if (transform.position.x > minX && transform.position.y < maxY && transform.position.z > minZ)
    //            {
    //                Vector3 newPos15 = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z - moveAmount);
    //                transform.position = newPos15;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 16: // MOVE LEFT 7
    //            if (transform.position.x > minX && transform.position.y > minY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos16 = new Vector3(transform.position.x - moveAmount, transform.position.y - moveAmount, transform.position.z + moveAmount);
    //                transform.position = newPos16;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 17: // MOVE LEFT 8
    //            if (transform.position.x > minX && transform.position.y > minY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos17 = new Vector3(transform.position.x - moveAmount, transform.position.y - moveAmount, transform.position.z);
    //                transform.position = newPos17;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 18: // MOVE LEFT 9
    //            if (transform.position.x > minX && transform.position.y > minY && transform.position.z > minZ)
    //            {
    //                Vector3 newPos18 = new Vector3(transform.position.x - moveAmount, transform.position.y - moveAmount, transform.position.z - moveAmount);
    //                transform.position = newPos18;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 19: // MOVE MIDDLE 1
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos19 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z + moveAmount);
    //                transform.position = newPos19;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 20: // MOVE MIDDLE 2
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos20 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
    //                transform.position = newPos20;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 21: // MOVE MIDDLE 3
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
    //            {
    //                Vector3 newPos21 = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z - moveAmount);
    //                transform.position = newPos21;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 22: // MOVE MIDDLE 4
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos22 = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
    //                transform.position = newPos22;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 23: // MOVE MIDDLE 6 (5 is where the room currently is, so skipped)
    //            if (transform.position.x < maxX && transform.position.y < maxY && transform.position.z > minZ)
    //            {
    //                Vector3 newPos23 = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveAmount);
    //                transform.position = newPos23;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 24: // MOVE MIDDLE 7
    //            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos24 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z + moveAmount);
    //                transform.position = newPos24;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 25: // MOVE MIDDLE 8
    //            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z < maxZ)
    //            {
    //                Vector3 newPos25 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
    //                transform.position = newPos25;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        case 26: // MOVE MIDDLE 9
    //            if (transform.position.x < maxX && transform.position.y > minY && transform.position.z > minZ)
    //            {
    //                Vector3 newPos26 = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z - moveAmount);
    //                transform.position = newPos26;
    //            }
    //            else
    //            {
    //                stopGeneration = true;
    //            }

    //            break;
    //        default:
    //            break;
    //    }

    //    Instantiate(rooms[0], transform.position, Quaternion.identity);
    //    direction = Random.Range(1, 27);
    //}

}

