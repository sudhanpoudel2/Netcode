using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AutoDestroy : NetworkBehaviour
{
    public float delayBeforeDestroy = 5f;
    

  
    [ServerRpc(RequireOwnership = false)]
    private void DestroyParticlesServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
        Destroy(gameObject , delayBeforeDestroy);
    }
}
