using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    [Range(2000f, 10000f)]
    public float bombSpeed = 2000f;
    public float moveSpeed =10f;
    public float rotSpeed =240f;
    public Transform other;
    public Transform turret;
    public Rigidbody bombPrefab;
    public Transform bombSpawn;
    public Material mater;
    public Rigidbody rb { get { return GetComponent<Rigidbody>(); } }

    
    private void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    protected abstract IEnumerator LookAt(Transform other);
    

    protected void Fire()
    {
        var bomb = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
        bomb.AddForce(turret.forward* bombSpeed);
    }

    protected void createMoveEffect(float moveAxis)
    {
        mater.mainTextureOffset += new Vector2(moveAxis, 0);
    }
}
