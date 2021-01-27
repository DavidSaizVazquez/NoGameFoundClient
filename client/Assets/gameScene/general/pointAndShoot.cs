using System;
using System.Collections;
using System.Collections.Generic;
using NoGameFoundClient;
using UnityEngine;
using UnityEngine.UI;

public class pointAndShoot : MonoBehaviour
{
    public GameObject cross;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject shieldBulletPrefab;
    public Animator playerAnimator;

    public float bulletSpeed = 60.0f;
    public float bulletBuffer = 0.7f;
    public float bulletRate = 0.7f;

    private float counter = 0;
        
    private Vector2 target;
    private Vector2 diff;
    private Vector2 direction;

    private ServerConnection server;
    private string message;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        server = ServerConnection.getInstance();
    }

    //Moves user cursor to shoot and shoots if click is detected
    // Update is called once per frame
    void Update()
    {
        if (!globalData.GamePaused)
        {
            target = transform.GetComponent<Camera>()
                .ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            cross.transform.position = new Vector2(target.x, target.y);
            diff = new Vector2(target.x - player.transform.position.x, target.y - player.transform.position.y);
            counter += Time.deltaTime;
            if (counter > bulletRate)
            {
                if (!playerAnimator.GetBool("Dead"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        try
                        {
                            float distance = diff.magnitude;
                            direction = diff / distance;
                            direction.Normalize();
                            fireBullet(direction, 0);
                            message = "21/" + player.transform.position.x + "/"
                                      + player.transform.position.y + "/" +
                                      direction.x + "/" + direction.y + "~";
                            counter = 0;
                            server.SendMessage(message);
                        }
                        catch
                        {
                        }

                    }
                }

            }
        }
    }

    //Creates bullet that goes from player to user pointer and given velocity
    void fireBullet(Vector3 dir, float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = player.transform.position+dir*bulletBuffer;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), b.GetComponent<Collider2D>());
        b.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
    }
    public void fireOnlineBullet(Vector3 dir,Vector3 position,float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), b.GetComponent<Collider2D>());
        b.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
    }
    public void fireShieldBullet()
    {
        float distance = diff.magnitude;
        direction = diff / distance;
        direction.Normalize();
        GameObject b = Instantiate(shieldBulletPrefab) as GameObject;
        b.transform.position = player.transform.position+(Vector3)direction*bulletBuffer;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), b.GetComponent<Collider2D>());
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed/4;
        message = "31/" + player.transform.position.x + "/"
                  + player.transform.position.y + "/" +
                  direction.x + "/" + direction.y + "~";
        server.SendMessage(message);
    }
    public void fireOnlineShieldBullet(Vector3 dir,Vector3 position,float rotationZ)
    {
        GameObject b = Instantiate(shieldBulletPrefab) as GameObject;
        b.transform.position = position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), b.GetComponent<Collider2D>());
        b.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed/4;
    }
    
    
}
