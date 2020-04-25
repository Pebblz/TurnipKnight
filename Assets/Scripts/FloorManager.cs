using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject floorPrefab;
    public GameObject[] trapPrefabs;
    private GameObject[] floors;

    public float padding = 2f;
    public int segCount = 3;
    void Start()
    {
        floors = new GameObject[3];
        trapPrefabs = Resources.LoadAll<GameObject>("TrapPrefabs");
        floors[0] = Instantiate(floorPrefab);
        floors[0].transform.position = new Vector3(0, 0, 0);
        floors[1] = Instantiate(floorPrefab);
        floors[1].transform.position = new Vector3(floors[0].transform.position.x + floors[0].transform.localScale.x + padding, 0, 0);
        floors[2] = Instantiate(floorPrefab);
        floors[2].transform.position = new Vector3(floors[1].transform.position.x + floors[1].transform.localScale.x + padding, 0, 0);
        for (int i = 0; i < floors.Length; i++)
        {

            for(int j = 0; j < segCount; j++ )
            {
                floors[i].GetComponent<FloorScript>().segs.Add(Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere)));
                floors[i].GetComponent<FloorScript>().segs[j].transform.parent = floors[i].GetComponent<FloorScript>().transform;

                
                floors[i].GetComponent<FloorScript>().segs[j].transform.localPosition = new Vector3(-0.33f  +  0.33f * j, 2, 0);
            }
        


        }

    }

    // Update is called once per frame
    void Update()
    {
        int outOfView = floorIsOutOfView();
        if(outOfView >= 0)
        {
            moveFloor(outOfView);
        }
    }

    public int floorIsOutOfView()
    {
        int i = 0;
        for (; i < this.floors.Length; i++)
            if(floors[i].GetComponent<FloorScript>().isOutOfView())
            {
                return i;
            }
        return -1;
            
    }

    public void moveFloor(int floorIdx)
    {
        float posX;
        float scaleX;
        switch (floorIdx)
        {
            
            case 0:
                posX = this.floors[2].transform.position.x;
                scaleX = this.floors[2].transform.localScale.x;
                floors[0].transform.position = new Vector3(posX + scaleX + this.padding, 0, 0);
                floors[0].GetComponent<FloorScript>().UpdateFloor();
                break;
            case 1:
                posX = this.floors[0].transform.position.x;
                scaleX = this.floors[0].transform.localScale.x;
                floors[1].transform.position = new Vector3(posX + scaleX + this.padding, 0, 0);
                floors[1].GetComponent<FloorScript>().UpdateFloor();

                break;
            case 2:
                posX = this.floors[1].transform.position.x;
                scaleX = this.floors[1].transform.localScale.x;
                floors[2].transform.position = new Vector3(posX + scaleX + this.padding, 0, 0);
                floors[2].GetComponent<FloorScript>().UpdateFloor();

                break;
            default:
                break;
        }
    }
}
