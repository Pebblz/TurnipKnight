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


    private void Start()
    {
    

    }
    void Update()
    {

        if (Input.GetKey(KeyCode.R))
        {
            respawnTraps();
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



    public void LoadTraps()
    {
        int count = 0;
        foreach(Transform child in this.transform)
        {
            var trapPrefab = randomTrapsList[Random.Range(0, randomTrapsList.Count)];
            var trapToLoad = Instantiate(trapPrefab);
            trapToLoad.transform.parent = child;
            trapToLoad.transform.localPosition = child.localPosition;
            traps.Add(trapToLoad);
            count++;
        }

    }

    public void clearChildren()
    {
        for(int i = 0; i < traps.Count; i++)
        {
            Destroy(traps[i]);
        }

        traps.Clear();
    }

    public void respawnTraps()
    {
        clearChildren();
        LoadTraps();
    }
    public void UpdateFloor()
    {

        respawnTraps();


    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PLAYER")
        {
            this.needToBeMoved = true;
        }
    }


}
