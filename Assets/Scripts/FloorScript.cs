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

    //temporary list of traps before I get resource manager setup
    public GameObject trap1;
    public GameObject trap2;
    public GameObject trap3;

    private void Start()
    {
        if (!startingSegment)
        {
            respawnTraps();
        }
    }
    void Update()
    {
        starting = false;
        if (isOutOfView() && needToBeMoved && !starting)
        {
            updatePosition();
            
        }
        //if (Input.GetKey(KeyCode.R))
        //{
        //    respawnTraps(); 
        //}
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
            int trapDecider = Random.Range(1, 3);
            GameObject t;
            switch (trapDecider)
            {
                case 1:

                    t = Instantiate(trap1);
                    traps.Add(t);

                    break;
                case 2:
                     t = Instantiate(trap2);
                    traps.Add(t);
                    break;
                case 3:
                     t = Instantiate(trap3);
                    traps.Add(t);
                    break;
            }
            
           

           

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
    public void updatePosition()
    {
        Vector3 curPos = this.transform.position;
        Vector3 newPos = new Vector3();
        newPos.y = curPos.y;
        newPos.z = curPos.z;
        newPos.x = curPos.x + GameObject.FindGameObjectsWithTag("FLOOR").Length * this.transform.localScale.x;
        this.transform.position = newPos;
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
