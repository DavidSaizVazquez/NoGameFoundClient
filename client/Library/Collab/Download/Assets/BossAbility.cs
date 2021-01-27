using System.Collections.Generic;
using NoGameFoundClient;
using UnityEngine;

public class BossAbility : MonoBehaviour
{
    private Animator animator;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletBuffer = 0.7f;


    Vector2 diff;
    Vector2 direction;
    ServerConnection serverConnection = ServerConnection.getInstance();
    private List<Transform> players;
    public void chargePlayers()
    {
        players = new List<Transform>();
        GameObject [] go= GameObject.FindGameObjectsWithTag("Player");
        foreach (var gameObject in go)
        {
            if (gameObject.name.Equals("Player") || gameObject.name.Equals("NPCPlayer(Clone)"))
                players.Add(gameObject.transform);
        }
    }
    
    public void fireBullet()
    {
        Vector3 pos = gameObject.transform.position;
        int index = Random.Range(0, players.Count);
        Vector3 ppos = players[index].position;
        diff = new Vector2(ppos.x-pos.x, ppos.y - pos.y);
        float distance = diff.magnitude;
        direction = diff / distance;
        direction.Normalize();
        string message = "23/1,"+direction.x+ ","+direction.y+"/0~";
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        bulletPrefab.layer = 13;
        b.transform.position = pos;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), b.GetComponent<Collider2D>());
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        serverConnection.SendMessage(message);
    }
}
