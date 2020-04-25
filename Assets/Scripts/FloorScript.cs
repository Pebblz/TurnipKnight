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
    private List<GameObject> traps = new List<GameObject>();
    private GameObject[] resources;
    public float padding;
    public GameObject seg1;
    public GameObject seg2;
    public GameObject seg3;
    public GameObject seg4;


    private void Start()
    {
        if (!startingSegment)
        {
            respawnTraps();
        }

        seg1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        seg2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        seg3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        seg4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        attachSegments();

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


    public int getNumberOfTrapGroups()
    {
        //I played around with this math a bit
        //it just seems that the sqrt of the width of the floor divided by the player speed
        //gives back a fair bit of traps per each floor segment, and it slowly decreases as the player 
        //gets faster.

        return 4;

        float playerspeed = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<PlayerScript>().speed;
        return Mathf.CeilToInt(Mathf.Sqrt(this.transform.localScale.x / playerspeed));
    } 

    public void LoadTraps()
    {
        int numberOfGroups = getNumberOfTrapGroups();
        int[] startPos = new int[numberOfGroups];
        int randMax = (int)this.transform.localScale.x / numberOfGroups;
        for(int i = 0; i < numberOfGroups; i++)
        {
            startPos[i] = Random.Range(0, randMax) + randMax * i;
        }

        for( int i = 0; i < numberOfGroups; i++)
        {
            int trapDecider = Random.Range(0, this.resources.Length) ;
            GameObject t;

            t = Instantiate(this.resources[trapDecider]);
            traps.Add(t);

        }

        int count = 0;
        foreach(GameObject child in this.traps)
        {
            Vector3 newPos = new Vector3();
            newPos.y = child.transform.position.y;
            newPos.z = child.transform.position.z;
            newPos.x = startPos[count] + ( this.transform.position.x - this.transform.localScale.x/2);
            child.transform.position = newPos;
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
        seg1.SetActive(true);
        seg2.SetActive(true);
        seg3.SetActive(true);
        seg4.SetActive(true);
        chooseSegmet();
    }

    public void chooseSegmet()
    {
        int whichseg = Random.Range(0, 3);

        switch(whichseg)
        {
            case 0:
                seg1.SetActive(false);
                break;

            case 1:
                seg2.SetActive(false);
                break;

            case 2:
                seg3.SetActive(false);
                break;

            case 3:
                seg4.SetActive(false);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PLAYER")
        {
            this.needToBeMoved = true;
        }
    }

    private void attachSegments()
    {
        seg1.transform.SetParent(this.gameObject.transform);
        seg2.transform.SetParent(this.gameObject.transform);
        seg3.transform.SetParent(this.gameObject.transform);
        seg4.transform.SetParent(this.gameObject.transform);

        seg1.transform.localPosition = new Vector3(0, 2, 0);
        seg2.transform.localPosition = new Vector3(8, 2, 0);
        seg3.transform.localPosition = new Vector3(15, 2, 0);
        seg4.transform.localPosition = new Vector3(27, 2, 0);

    }
}
