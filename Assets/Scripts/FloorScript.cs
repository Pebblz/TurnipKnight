using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    private bool starting = true;
    private bool needToBeMoved = false;
    void Update()
    {
        starting = false;
        if (isOutOfView() && needToBeMoved && !starting)
        {
            updatePosition();
            
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


    public void updatePosition()
    {
        Vector3 curPos = this.transform.position;
        Vector3 newPos = new Vector3();
        newPos.y = curPos.y;
        newPos.z = curPos.z;
        newPos.x = curPos.x + GameObject.FindGameObjectsWithTag("FLOOR").Length * this.transform.localScale.x;
        this.transform.position = newPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PLAYER")
        {
            this.needToBeMoved = true;
        }
    }
}
