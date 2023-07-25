using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public ShootFireball parent;
    [SerializeField] private GameObject hitParticals;
    [SerializeField] private float shootForce;
    private Rigidbody rb;
    private readonly bool IsOwner;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.transform.forward * shootForce;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        InstantiateHitParticalsServerRpc();
        parent.DestroyServerRpc();
    }

    [ServerRpc]
    private void InstantiateHitParticalsServerRpc()
    {
        GameObject hitImpact = Instantiate(hitParticals, transform.position, Quaternion.identity);
        hitImpact.GetComponent<NetworkObject>().Spawn() ; 
        hitImpact.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
    }


}
