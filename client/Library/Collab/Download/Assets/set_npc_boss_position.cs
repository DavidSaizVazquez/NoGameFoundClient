using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class set_npc_boss_position : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
     
    }

    public void setPosition(string command)
    {
        rb.transform.position = new Vector3(rb.transform.position.x, float.Parse(command), rb.transform.position.z);
    }

    public void fireBossBullet(Vector3 dir)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = rb.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        Debug.Log("bossShoot");
        b.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
    }


}
