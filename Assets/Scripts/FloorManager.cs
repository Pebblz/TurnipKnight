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
        floors[0].transform.position = new Vector3(0, -2, 0);

        floors[1] = Instantiate(floorPrefab);
        float pos = floors[0].transform.position.x + floors[0].GetComponent<FloorScript>().getBigOlLength() + padding;
        floors[1].transform.position = new Vector3(pos, -2, 0);
        floors[2] = Instantiate(floorPrefab);
        floors[2].transform.position = new Vector3(floors[1].transform.position.x + floors[1].GetComponent<FloorScript>().getBigOlLength() + padding, - 2, 0);
        for (int i = 0; i < floors.Length; i++)
        {

            for(int j = 0; j < segCount; j++ )
            {
                float segLength = floors[i].GetComponent<FloorScript>().getBigOlLength() / 3f;
                GameObject empty = (visibleTrapSpawnLocations) ? GameObject.CreatePrimitive(PrimitiveType.Sphere) : new GameObject();
                floors[i].GetComponent<FloorScript>().segs.Add(empty);
                floors[i].GetComponent<FloorScript>().segs[j].transform.position = new Vector3(0, 800, -segLength + this.transform.position.x + segLength * j);
                floors[i].GetComponent<FloorScript>().segs[j].transform.parent = floors[i].GetComponent<FloorScript>().transform;
                floors[i].GetComponent<FloorScript>().segs[j].transform.localPosition = new Vector3(0, 420f + 10f, 650f - 650f * j);
            }
            floors[i].GetComponent<FloorScript>().LoadWalls();
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
        this.floors[floorIdx].GetComponent<FloorScript>().LoadWalls();
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
                scaleX = this.floors[2].GetComponent<FloorScript>().getBigOlLength();
                floors[0].transform.position = new Vector3(posX + scaleX + this.padding, -2, 0);
                floors[0].GetComponent<FloorScript>().clearChildren();
                floors[0].GetComponent<FloorScript>().randomTrapsList.Clear();
                this.floorCount++;
                spawnTrapsOnFloor(0);
                
                break;
            case 1:   
                posX = this.floors[0].transform.position.x;
                scaleX = this.floors[0].GetComponent<FloorScript>().getBigOlLength();
                floors[1].transform.position = new Vector3(posX + scaleX + this.padding, -2, 0);
                floors[1].GetComponent<FloorScript>().clearChildren();
                floors[1].GetComponent<FloorScript>().randomTrapsList.Clear();
                spawnTrapsOnFloor(1);
                this.floorCount++;
                break;
            case 2:
                posX = this.floors[1].transform.position.x;
                scaleX = this.floors[1].GetComponent<FloorScript>().getBigOlLength();
                floors[2].transform.position = new Vector3(posX + scaleX + this.padding, -2, 0);
                floors[2].GetComponent<FloorScript>().clearChildren();
                floors[2].GetComponent<FloorScript>().randomTrapsList.Clear();
                spawnTrapsOnFloor(2);
                this.floorCount++;

                break;
            default:
                break;
        }
    }
}
