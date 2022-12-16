using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject[] gridPrefab;
    public int rows;
    public int cols;
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

    public GameObject RandomRoomPrefab()
    {
        return gridPrefab[Random.Range(0, gridPrefab.Length)];
    }

    public void GenerateMap()
    {
        grid = new Room[rows, cols];

        for (int currentRow = 0; currentRow < rows; currentRow++) 
        {
            for( int currentCol = 0; currentCol < cols; currentCol++)
            {
                float xPosition = currentRow * roomWidth;
                float zPosition = currentCol * roomHeight;
                Vector3 newPosition = new Vector3(xPosition, 0, zPosition);
                Vector3 newSize = new Vector3(roomWidth / 50, roomHeight / 50, roomWidth / 50);

                GameObject temporaryRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity);
                temporaryRoomObj.transform.parent = this.transform;
                temporaryRoomObj.name = "Room_" + currentCol + ", " + currentRow;
                temporaryRoomObj.transform.localScale = newSize;
                Room tempRoom = temporaryRoomObj.GetComponent<Room>();
                grid[currentRow, currentCol] = tempRoom;

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
