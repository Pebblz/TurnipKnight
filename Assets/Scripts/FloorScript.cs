using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    private bool starting = true;
    private bool needToBeMoved = false;
    public bool startingSegment;
    public List<GameObject> traps = new List<GameObject>();
    public List<GameObject> randomTrapsList = new List<GameObject>();
    private GameObject[] resources;
    public float padding;
    public List<GameObject> segs = new List<GameObject>();
    public GameObject wall;
    GameObject wallInstance;
    GameObject wallInstance2;
    private GameObject easyShield;
    private GameObject medShield;
    private GameObject hardShield;
    public float sheildz = 6.9f;
    public List<GameObject> wallList = new List<GameObject>();



    private void Start()
    {
        easyShield = Resources.Load<GameObject>("Shields/shieldeasy");
        medShield = Resources.Load<GameObject>("Shields/shieldmedium");
        hardShield = Resources.Load<GameObject>("Shields/shieldmedium");

    }
    void Update()
    {

        if (Input.GetKey(KeyCode.R))
        {
            clearChildren();
            LoadTraps();
        }
    }

    public bool isOutOfView()
    {

        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 dist = cam.WorldToScreenPoint(this.transform.position);
        
        if(dist.x < -1 * Screen.width)
        {
            return true;
        }

        return false;



    }

    public float getBigOlLength()

    {

        Transform child1 = transform.GetChild(0);

        Transform child2 = transform.GetChild(1);

        return Mathf.Abs(child1.position.x - child2.position.x);

    }

    public void LoadTraps()
    {
        int count = 0;
        foreach(GameObject child in segs)
        {

            var trapPrefab = randomTrapsList[Random.Range(0, randomTrapsList.Count)];
            var trapToLoad = Instantiate(trapPrefab);
            trapToLoad.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, 0);
            traps.Add(trapToLoad);
            count++;
        }

    }

    public void LoadWalls()
    {
        wallInstance = Instantiate(wall);
        wallInstance.transform.position = new Vector3(segs[1].transform.position.x, -5, 7);
        wallInstance2 = Instantiate(wall);
        wallInstance2.transform.position = new Vector3(segs[1].transform.position.x + 9, -5, 7);
        wallInstance2.transform.localScale = new Vector3(2, 2, 3);
        wallList.Add(wallInstance);
        wallList.Add(wallInstance2);
    }

    public void spawnShield()
    {
        GameObject shieldToSpawn = null;
        if(easyShield == null || medShield == null || hardShield == null)
        {
            return;
        }
        switch (FloorManager.gameDifficulty)
        {
            case FloorManager.DIFFICULTY.EASY:
                shieldToSpawn = Instantiate(easyShield);
                break;
            case FloorManager.DIFFICULTY.MEDIUM:
                shieldToSpawn = Instantiate(medShield);
                break;
            case FloorManager.DIFFICULTY.HARD:
                shieldToSpawn = Instantiate(hardShield);
                break;
        }
        float posX = this.transform.position.x - this.getBigOlLength()/2;
        shieldToSpawn.transform.position = new Vector3(posX -1f, 8, sheildz);

    }

    public void clearChildren()
    {
         for(int i = 0; i < traps.Count; i++)
        {
            Destroy(traps[i]);
       

        }
        for(int i = 0; i < wallList.Count; i++)
        {
            Destroy(wallList[i]);

        }

        
        
        traps.Clear();
    }


    


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PLAYER")
        {
            this.needToBeMoved = true;
        }
    }


}
