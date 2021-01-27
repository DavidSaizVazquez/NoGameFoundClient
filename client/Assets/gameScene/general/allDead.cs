using System.Collections;
using System.Collections.Generic;
using NoGameFoundClient;
using UnityEngine;
using UnityEngine.UI;

public class allDead : MonoBehaviour
{
    
    public float tPhases = 10000;
    public bool death=false;
    public Text text;

    private float counter = 0;
    private List<Animator> _animators;
    private Color intermediate;
    
    
    public void chargePlayers()
    {
        GameObject [] go= GameObject.FindGameObjectsWithTag("Player");
        _animators = new List<Animator>();
        foreach (var gameObject in go)
        {
            if(gameObject.name.Equals("Player")||gameObject.name.Equals("NPCPlayer(Clone)"))
                _animators.Add(gameObject.GetComponent<Animator>());
        }
        text.enabled = false;
        intermediate = new Color();
    }

    // Update is called once per frame
    void Update()
    {
        globalData.Score -= Time.deltaTime;
        foreach (var animator in _animators)
        {
            if (!animator.GetBool("Dead"))
            {
                death = false;
                return;
            }
        }
        if (death == false)
        {
            death = true;
            text.enabled = true;
            text.GetComponent<Animator>().Play("over");
        }

    }
}
