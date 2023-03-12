using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour
{
    [SerializeField] private int countEnemy;
    [SerializeField] private GameObject[] typeEnemy;
    [SerializeField] private Vector3[] spawnPoint;
    [SerializeField] private GameObject testCharacterPlayer;
    [SerializeField] private InputController inputController;
    private GameObject playerCharacter;

    private void Awake()
    {
        PlayerManager player = PlayerManager.Instance;
        PlayerManager.selectedCharacter = testCharacterPlayer;
        playerCharacter = Instantiate(PlayerManager.selectedCharacter);
        playerCharacter.transform.position = spawnPoint[0];
        Destroy(playerCharacter.GetComponent<CharacterBehaviour>());
        inputController.Init(playerCharacter);
    }
    private void Start()
    {
        for (int i = 0; i < countEnemy; i++)
        {
            Instantiate(typeEnemy[Random.Range(0, typeEnemy.Length)]).transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)];
        }
    }
}
