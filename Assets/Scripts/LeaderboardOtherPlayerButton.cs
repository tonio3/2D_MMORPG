using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.CloudCode;
using Unity.Services.CloudSave;
using UnityEngine;

public class LeaderboardOtherPlayerButton : MonoBehaviour
{
    public string playerName { get; set; }
    [SerializeField] private CharacterDataSO _otherCharacterDataSO;

    public string playerId{ get; set; }


    public void OnClick()
    {
       OpenOtherPlayerCard(playerId);
    }
 

    private void OpenOtherPlayerCard(string otherPlayerNameId)
    {
       
        _otherCharacterDataSO.CharacterIdentity.CharacterName = playerName;
        _otherCharacterDataSO.CharacterIdentity.CharacterId = otherPlayerNameId;
     
    }

}
