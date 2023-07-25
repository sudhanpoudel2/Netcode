using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class playerSetting : NetworkBehaviour
{
    [SerializeField] private TextMeshPro playerName;
    [SerializeField] private NetworkVariable<FixedString128Bytes> networkplayerName = new NetworkVariable<FixedString128Bytes>("Player:0",
        NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        networkplayerName.Value = "Player:" + (OwnerClientId + 1);
        playerName.text = networkplayerName.Value.ToString();
    }

}
