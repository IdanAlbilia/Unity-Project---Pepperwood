using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effectToSpawn;
    private float timeToFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Spell") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
            SpawnVFX(); 
        }
    }

    public void SpawnVFX()
    {
        GameObject vfx;

        if(firePoint != null)
        {
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, new Quaternion(firePoint.transform.rotation.x, firePoint.transform.rotation.y, firePoint.transform.rotation.z, firePoint.transform.rotation.w));
        }
        else
        {
            Debug.Log("no fire point");
        }
    }

    public float GetTimeToFire()
    {
        return timeToFire;
    }

    public void SetTimeToFire(float newTimeToFire)
    {
        timeToFire = newTimeToFire;
    }

    public GameObject GetEffect()
    {
        return effectToSpawn;
    }
}
