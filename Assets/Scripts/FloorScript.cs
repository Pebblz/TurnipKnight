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
    public List<GameObject> wallList = new List<GameObject>();


    private void Start()
    {
    

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
            wallInstance = Instantiate(wall);
            wallInstance.transform.position = new Vector3(child.transform.position.x, -5, 3);

            traps.Add(trapToLoad);
            wallList.Add(wallInstance);
            count++;
        }

    }

    public void clearChildren()
    {
        for(int i = 0; i < traps.Count; i++)
        {
            Destroy(traps[i]);
       
            
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
