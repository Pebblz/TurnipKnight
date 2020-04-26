using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObstacle : Obstacle
{
    public GameObject cheese;
    public GameObject meat;
    public GameObject apple;

    public bool swapped = false;


    public override void Start()
    {
        this.gameObject.AddComponent<CollectableModifier>();
        this.gameObject.AddComponent<ParticleEffectModifier>();
        modifiers.Add(this.gameObject.GetComponent<CollectableModifier>());
        modifiers.Add(this.gameObject.GetComponent<ParticleEffectModifier>());

        if (swapped == false && this.gameObject.tag == "COLLECT")
        {
            foodDecider();
        }
    }


    public void foodDecider()
    {
        int pUp = Random.Range(0, 3);

        switch (pUp)
        {
            case 0:
                Instantiate(meat, transform.position, transform.rotation);
                Destroy(gameObject);
                break;

            case 1:
                Instantiate(cheese, transform.position, transform.rotation);
                Destroy(gameObject);
                break;

            case 2:
                Instantiate(apple, transform.position, transform.rotation);
                Destroy(gameObject);
                break;
        }

        swapped = true;
    }

    public override void AdditionalEffects()
    {
        GameManager.soundSource.pitch = Random.Range(0.8f, 1.2f);
        GameManager.soundSource.PlayOneShot(GameObject.Find("Player").GetComponent<PlayerScript>().scoreSound);
        Destroy(this.gameObject);
    }
}
