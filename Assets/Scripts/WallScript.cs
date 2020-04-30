using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasShields;
    public GameObject easyShield;
    public GameObject medShield;
    public GameObject hardShield;
    void Start()
    {
        if (this.hasShields)
        {
            spawnShields();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutOfView())
        {
            moveWall();
        }
    }

    public bool isOutOfView()
    {

        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 dist = cam.WorldToScreenPoint(this.transform.position);

        if (dist.x < -1 * Screen.width)
        {
            return true;
        }

        return false;
    }


    public float getLength()
    {
        Transform child1 = transform.GetChild(0);

        Transform child2 = transform.GetChild(1);

        return Mathf.Abs(child1.position.x - child2.position.x);
    }


    public void moveWall()
    {
        int numberOfActiveWalls = GameObject.FindGameObjectsWithTag("WALL").Length;
        float wallLength = this.getLength();
        float startX = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;
        Vector3 newPos = new Vector3(startX + wallLength * numberOfActiveWalls, y, z);
        this.transform.position = newPos;
        if(this.hasShields)
        {
            spawnShields();
        }
    }


    public void spawnShields()
    {
        GameObject shield1 = null;
        GameObject shield2 = null;

        switch (FloorManager.gameDifficulty)
        {
            case FloorManager.DIFFICULTY.EASY:
                shield1 = Instantiate(easyShield);
                shield2 = Instantiate(easyShield);
                break;
            case FloorManager.DIFFICULTY.MEDIUM:
                shield1 = Instantiate(medShield);
                shield2 = Instantiate(medShield);
                break;
            case FloorManager.DIFFICULTY.HARD:
                shield1 = Instantiate(hardShield);
                shield2 = Instantiate(hardShield);
                break;
        }

        float y = this.transform.position.y + 4;
        float z = this.transform.position.z;
        float x1 = this.transform.position.x - getLength() / 3;
        float x2 = this.transform.position.x + getLength() / 3  -1.5f;
        shield1.transform.position = new Vector3(x1, y, z);
        shield2.transform.position = new Vector3(x2, y, z);
    }
}
