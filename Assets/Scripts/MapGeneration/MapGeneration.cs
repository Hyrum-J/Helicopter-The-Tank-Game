using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    //Empty Array that will hold the grid
    public GameObject[] gridPrefab;

    //Number of rows and columns for generation
    public int rows;
    public int cols;

    //How big the room is
    public float roomWidth;
    public float roomHeight;
    public Room[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Gets a random room from the list
    public GameObject RandomRoomPrefab()
    {
        return gridPrefab[Random.Range(0, gridPrefab.Length)];
    }

    //Generates a random map
    public void GenerateMap()
    {
        grid = new Room[rows, cols];

        for (int currentRow = 0; currentRow < rows; currentRow++) 
        {
            for( int currentCol = 0; currentCol < cols; currentCol++)
            {
                //Sets Location of room
                float xPosition = currentRow * roomWidth;
                float zPosition = currentCol * roomHeight;
                Vector3 newPosition = new Vector3(xPosition, 0, zPosition);
                Vector3 newSize = new Vector3(roomWidth / 50, roomHeight / 50, roomWidth / 50);

                //Sets the actual room
                GameObject temporaryRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity);
                temporaryRoomObj.transform.parent = this.transform;
                temporaryRoomObj.name = "Room_" + currentCol + ", " + currentRow;
                temporaryRoomObj.transform.localScale = newSize;
                Room tempRoom = temporaryRoomObj.GetComponent<Room>();
                grid[currentRow, currentCol] = tempRoom;

                //Disables doors based on location
                if(currentCol == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentCol == rows - 1) 
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }

                if (currentRow == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (currentRow == cols - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorWest.SetActive(false);
                    tempRoom.doorEast.SetActive(false);
                }
            }
        }
    }
}
