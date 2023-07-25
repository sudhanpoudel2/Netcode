using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShootFireball : NetworkBehaviour
{

    [SerializeField] private GameObject fireball;//Fireball prefab
    [SerializeField] private Transform shootTransform;//Shoot Position

    //List to hold all the instantiated fireballs
    [SerializeField] private List<GameObject> spawnedFireBalls = new List<GameObject>();

    // Update is called once per frame
    public override void OnNetworkSpawn()
    {
        Debug.Log("Hello");
    }
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootServerRpc();
            //DestroyServerRpc();
        }

    }
    [ServerRpc]
    private void ShootServerRpc()
    {
        GameObject go = Instantiate(fireball, shootTransform.position, shootTransform.rotation);
        spawnedFireBalls.Add(go);
        go.GetComponent<MoveProjectile>().parent = this;
        go.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc]
   public void DestroyServerRpc()
    {
        GameObject toDestroy = spawnedFireBalls[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedFireBalls.Remove(toDestroy);
        Destroy(toDestroy);
    }
}
