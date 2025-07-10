using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int life = 100;
    public void Hits(int damage) {
        life -= damage;
        if (life <= 0) {
            Destroy(gameObject);
        }
    }

}
