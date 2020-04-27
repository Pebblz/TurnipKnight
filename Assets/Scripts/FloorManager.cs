using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorManager : MonoBehaviour
{

    public List<GameObject> easyLayouts;
    public List<GameObject> mediumLayouts;
    public List<GameObject> hardLayouts;
    public List<GameObject> usedEasyLayouts;
    public List<GameObject> usedMediumLayouts;
    public List<GameObject> usedHardLayouts;

    // Start is called before the first frame update
    public GameObject floorPrefab;
    private GameObject[] floors;
    public bool visibleTrapSpawnLocations = false;
    private bool starting;

    public float padding = 2f;
    public int segCount = 3;
    public DIFFICULTY gameDifficulty = DIFFICULTY.EASY;
    public int floorCount = 0;
    public enum DIFFICULTY
    {
        EASY,
        MEDIUM,
        HARD
    }

    void Start()
    {
        starting = true;
        easyLayouts = Resources.LoadAll<GameObject>(@"layouts\Easy").ToList<GameObject>();
        mediumLayouts = Resources.LoadAll<GameObject>(@"layouts\Medium").ToList<GameObject>();
        hardLayouts = Resources.LoadAll<GameObject>(@"layouts\Hard").ToList<GameObject>();
        usedEasyLayouts = new List<GameObject>(easyLayouts);
        usedMediumLayouts = new List<GameObject>(mediumLayouts);
        usedHardLayouts = new List<GameObject>(hardLayouts);
        floors = new GameObject[3];
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
                GameObject empty = (visibleTrapSpawnLocations) ? GameObject.CreatePrimitive(PrimitiveType.Sphere) : new GameObject();
                floors[i].GetComponent<FloorScript>().segs.Add(empty);
                floors[i].GetComponent<FloorScript>().segs[j].transform.parent = floors[i].GetComponent<FloorScript>().transform;
                floors[i].GetComponent<FloorScript>().segs[j].transform.localPosition = new Vector3(-0.33f  +  0.33f * j, 1, 0);
            }

            spawnTrapsOnFloor(i);

        }

    }

    public void setDifficulty()
    {
        if(this.floorCount >= 10)
        {
            this.gameDifficulty = DIFFICULTY.HARD;
        } else if(this.floorCount >= 5)
        {
            this.gameDifficulty = DIFFICULTY.MEDIUM;
        }
    }
    public void spawnTrapsOnFloor(int floorIdx)
    {
        if(floorIdx == 0 && starting)
        {
            return;
        }
        starting = false;
        for(int i = 0; i < segCount; i++)
        {
            var randTrap = getRandomTrap();
            this.floors[floorIdx].GetComponent<FloorScript>().randomTrapsList.Add(randTrap);
        }
        this.floors[floorIdx].GetComponent<FloorScript>().LoadTraps();
    }

    public GameObject getRandomTrap()
    {
        if(this.usedEasyLayouts.Count <= 0)
        {
            usedEasyLayouts.AddRange(easyLayouts);
        }
        if (this.usedMediumLayouts.Count <= 0)
        {
            usedMediumLayouts.AddRange(mediumLayouts);
        }
        if (this.usedHardLayouts.Count <= 0)
        {
            usedHardLayouts.AddRange(hardLayouts);
        }


        GameObject trapToReturn = null;
        switch (this.gameDifficulty)
        {
            case DIFFICULTY.EASY:
                
                int easyrand = Random.Range(0, this.usedEasyLayouts.Count);
                trapToReturn = usedEasyLayouts[easyrand];
                usedEasyLayouts.RemoveAt(easyrand);
                break;

            case DIFFICULTY.MEDIUM:
                int medrand = Random.Range(0, this.usedMediumLayouts.Count);
                trapToReturn = usedMediumLayouts[medrand];
                usedMediumLayouts.RemoveAt(medrand);
                break;

            case DIFFICULTY.HARD:
                int hardRange = Random.Range(0, this.usedHardLayouts.Count);
                trapToReturn = usedHardLayouts[hardRange];
                usedHardLayouts.RemoveAt(hardRange);
                break;
            
            default:
                int defaultrand = Random.Range(0, this.usedEasyLayouts.Count);
                trapToReturn = usedEasyLayouts[defaultrand];
                usedEasyLayouts.RemoveAt(defaultrand);
                break;
        }
        return trapToReturn;
    }

    // Update is called once per frame
    void Update()
    {
        int outOfView = floorIsOutOfView();
        if(outOfView >= 0)
        {
            moveFloor(outOfView);
        }
        this.setDifficulty();
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
                floors[0].GetComponent<FloorScript>().clearChildren();
                this.floorCount++;
                spawnTrapsOnFloor(0);
                
                break;
            case 1:
                posX = this.floors[0].transform.position.x;
                scaleX = this.floors[0].transform.localScale.x;
                floors[1].transform.position = new Vector3(posX + scaleX + this.padding, 0, 0);
                floors[1].GetComponent<FloorScript>().clearChildren();
                spawnTrapsOnFloor(1);
                this.floorCount++;
                break;
            case 2:
                posX = this.floors[1].transform.position.x;
                scaleX = this.floors[1].transform.localScale.x;
                floors[2].transform.position = new Vector3(posX + scaleX + this.padding, 0, 0);
                floors[2].GetComponent<FloorScript>().clearChildren();
                spawnTrapsOnFloor(2);
                this.floorCount++;

                break;
            default:
                break;
        }
    }
}
