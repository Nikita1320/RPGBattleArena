using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleArena : MonoBehaviour
{
    [SerializeField] private TeemsManager teemsManager;
    [SerializeField] private BattleArenaData battleArenaData;
    [SerializeField] private TypePassingLevel typePassingLevel;
    [SerializeField] private InputController inputController;
    [SerializeField] private List<CharacterBehaviour> enemyBehaviours;
    [SerializeField] private Health playerCharacter;
    [SerializeField] private List<Health> enemyes = new();

    [Header("SpawnCharactersSettings")]
    [SerializeField] private List<Vector3> spawnPoints;
    private List<Vector3> freeSpawnPoints;

    [Header("CoutDownPanelSettings")]
    [SerializeField] private float timeToStart = 6;
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private Text countdownText;
    [SerializeField] private Image clock;
    [SerializeField] private Coroutine clockAnimationCororutine;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private RewardsPanel rewardsPanel;
    [SerializeField] private List<ResourceRewardCell> rewardCells = new();

    private void Start()
    {
        teemsManager = TeemsManager.Instance;

        freeSpawnPoints = spawnPoints;

        battleArenaData = BattleArenaMenu.SelectedBattleArenaData;
        typePassingLevel = BattleArenaMenu.TypeCurrentPassingLevel;

        var playerGameobject = SpawnCharacter(CharactersManager.Instance.SelectedCharacter);
        playerGameobject.GetComponent<MovementController>().InitializeStat(CharactersManager.Instance.SelectedCharacter);
        playerGameobject.GetComponent<CombatSystem>().InitializeStat(CharactersManager.Instance.SelectedCharacter);
        var playerHealth = playerGameobject.GetComponent<Health>();
        teemsManager.AddCharacterToTeem(playerGameobject, Color.green);
        playerHealth.InitializeStat(CharactersManager.Instance.SelectedCharacter, Color.green);
        playerHealth.diedEvent += DeathPlayer;

        inputController.Init(playerGameobject);

        Debug.Log("InstancePlayer");
        for (int i = 0; i < battleArenaData.NumberOfEnemy; i++)
        {
            var enemy = new Character(battleArenaData.PossibleEnemys[Random.Range(0, battleArenaData.PossibleEnemys.Length)],
            Random.Range(battleArenaData.MinRank, battleArenaData.MaxRank));
            enemy.AbilityTree.RandomImprove();

            var enemyGameobject = SpawnCharacter(enemy);
            enemyBehaviours.Add(enemyGameobject.GetComponent<CharacterBehaviour>());
            enemyGameobject.GetComponent<MovementController>().InitializeStat(enemy);
            enemyGameobject.GetComponent<CombatSystem>().InitializeStat(enemy);
            //enemyGameobject.GetComponent<EffectManager>().InitializeStat(enemy);
            var health = enemyGameobject.GetComponent<Health>();
            teemsManager.AddCharacterToTeem(enemyGameobject, Color.red);
            health.InitializeStat(enemy, Color.red);
            health.diedEvent += () => 
            {
                enemyes.Remove(health);
                if (enemyes.Count == 0) 
                { 
                    Win(); 
                } 
            };
            enemyes.Add(health);
            Debug.Log("InstanceEnemy: "+ i);
        }
        StartBattle();
    }
    private void DeathPlayer()
    {
        Loose();
    }
    private void StartBattle()
    {
        StartCoroutine(CountdownToStart());
    }
    private IEnumerator CountdownToStart()
    {
        countdownPanel.SetActive(true);
        var remainingTime = timeToStart;
        while (true)
        {
            remainingTime--;
            if (clockAnimationCororutine != null)
            {
                StopCoroutine(clockAnimationCororutine);
            }
            clockAnimationCororutine = StartCoroutine(ClockAnimation());
            countdownText.text = remainingTime.ToString();
            if (remainingTime == 0)
            {
                countdownPanel.SetActive(false);
                ActivateCharacters();
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }
    private IEnumerator ClockAnimation()
    {
        if (clock.fillClockwise == true)
            clock.fillAmount = 1;
        else
            clock.fillAmount = 0;

        clock.fillClockwise = !clock.fillClockwise;

        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (clock.fillClockwise == true)
                clock.fillAmount += Time.deltaTime;
            else
                clock.fillAmount -= Time.deltaTime;
        }
    }
    private void ActivateCharacters()
    {
        foreach (var item in enemyBehaviours)
        {
            item.enabled = true;
        }
        inputController.enabled = true;
    }
    private void Win()
    {
        BattleArenaMenu.PassLevel();
        StopGame();
        StartCoroutine(WinAnimation());
    }
    private IEnumerator WinAnimation()
    {
        winPanel.SetActive(true);
        while (true)
        {
            yield return new WaitForSeconds(3);
            winPanel.SetActive(false);
            GetReward();
            break;
        }
    }
    private void GetReward()
    {
        List<ResourceReward> resourceRewards = new();
        if (typePassingLevel == TypePassingLevel.Primary)
        {
            foreach (var item in battleArenaData.BattleRewardsForPassing)
            {
                var randomValue = Random.Range(item.MinAmmount, item.MaxAmmount);
                if (randomValue > 0)
                {
                    resourceRewards.Add(new ResourceReward(item.ResourceType, randomValue));
                }
            }
            rewardsPanel.OpenRewardPanel(resourceRewards: resourceRewards.ToArray());
        }
        else
        {
            foreach (var item in battleArenaData.BattleRewardsForSecondaryPassing)
            {
                var randomValue = Random.Range(item.MinAmmount, item.MaxAmmount);
                if (randomValue > 0)
                {
                    resourceRewards.Add(new ResourceReward(item.ResourceType, randomValue));
                }
            }
            rewardsPanel.OpenRewardPanel(resourceRewards: resourceRewards.ToArray());
        }
    }
    private void Loose()
    {
        StopGame();
    }
    private GameObject SpawnCharacter(Character character)
    {
        var randomPoint = freeSpawnPoints[Random.Range(0, freeSpawnPoints.Count)];
        freeSpawnPoints.Remove(randomPoint);
        var characterGameobject = (Instantiate(character.CharacterData.PrefabCharacter));
        characterGameobject.transform.position = randomPoint;
        return characterGameobject;
    }
    private void StopGame()
    {
        foreach (var item in enemyBehaviours)
        {
            if (item != null)
            {
                item.enabled = false;
            }
        }
        inputController.enabled = false;
    }
    public void BackToMainMenu()
    {
        SceneLoader.Instance.LoadScene(0);
    }
}
