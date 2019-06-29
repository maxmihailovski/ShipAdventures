using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private float initHp;
    private float hp;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float regenerationSpeed;
    [SerializeField]
    private float ylevelWater;
    [SerializeField]
    private float invertForcecCoef;
    [SerializeField]
    private float waterFriction;
    private Rigidbody2D myRigidbody;
    private void GameOver(){;}

    private float Hp {
        get
        {
            return hp; 
        }
        set
        {
            hp = value; 
            if (value > initHp)
                hp = initHp;
            if (value <= 0.0f)
                GameOver();
        }
   }

    private void Stabilization()
    {
        float h = ylevelWater - transform.position.y;
        if (h > 0)
        {
            myRigidbody.velocity += new Vector2(0.0f, invertForcecCoef*h*h* Time.deltaTime);
            myRigidbody.velocity *= 1 - waterFriction* Time.deltaTime;
        }
    }

    private void Regeneration()
    {
        Hp += regenerationSpeed*Time.deltaTime;
    }

    private void Move() {; }

    private delegate void OnEachFrame();
    private event OnEachFrame onEachFrame;

    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        onEachFrame += Regeneration;
        onEachFrame += Stabilization;
        Hp = initHp;
    }

    void Update()
    {
        onEachFrame();
        if (Input.GetKeyDown(KeyCode.Space))
            myRigidbody.velocity += new Vector2(0.0f, jumpForce);
    }
}
