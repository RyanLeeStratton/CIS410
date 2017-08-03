using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Variables : MonoBehaviour {

    public float knockback;
    public int damage;
    
    public float getKnockback()
    {
        return knockback;
    }

    public int getDamage()
    {
        return damage;
    }
}
