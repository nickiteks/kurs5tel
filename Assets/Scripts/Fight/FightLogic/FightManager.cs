using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : Singleton<FightManager>
{
    [SerializeField]
    private GameObject[] monstersPrefabs;
    [SerializeField]
    [Tooltip("Объекты на сцене")]
    private GameObject[] monstersGameObjects;
    [SerializeField]
    private Character[] playerCharacters = new Character[3];


    private bool IsStepFriendly { get; set; }
    
    private FightController CurrentController;
    [Space(20)]
    public FightController playerController;
    public FightController enemyController;

    [Tooltip("индекс персонажа текущей команды")]
    private int indexCurrentCharacter = 0;

    [SerializeField]
    private LoadSaveInventory loadSave;
    [SerializeField]
    private Color activeColorCharacter;
    [SerializeField]
    private Color basicColorCharacter;

    private void Start()
    {
        IsStepFriendly = true;

        // загрузка игрока
        playerCharacters[0].BaseStatsScript = loadSave.StatsWarrior;
        playerCharacters[1].BaseStatsScript = loadSave.StatsMage;
        playerCharacters[2].BaseStatsScript = loadSave.StatsRogue;

        playerController.characters = new List<Character>();
        foreach (Character character in playerCharacters)
        {
            //character.BaseStatsScript.MaxHelth = 200;
            //character.BaseStatsScript.Health = 200;
            playerController.characters.Add(character);
            
        }

        // загрузка монстров
        int randomMonsterIndex = Random.Range(0, monstersPrefabs.Length);
        GameObject monsterPrefab = monstersPrefabs[randomMonsterIndex];

        (enemyController as EnemyFightController).opponents = playerCharacters.ToList();

        // заполение объектов на сцене
        enemyController.characters = new List<Character>();
        foreach (GameObject monster in monstersGameObjects)
        { 
            monster.GetComponent<Image>().sprite = monsterPrefab.GetComponent<SpriteRenderer>().sprite;
            monster.GetComponent<Image>().SetNativeSize();

            BaseStatsScript baseStatsScriptMonster = monster.GetComponent<BaseStatsScript>();
            BaseStatsScript statsPrefab = monsterPrefab.GetComponent<BaseStatsScript>();

            baseStatsScriptMonster.ManaMax = statsPrefab.ManaMax;
            baseStatsScriptMonster.Mana = statsPrefab.Mana;
            baseStatsScriptMonster.MaxHelth = statsPrefab.MaxHelth;
            baseStatsScriptMonster.Health = statsPrefab.Health;
            baseStatsScriptMonster.Armor = statsPrefab.Armor;
            baseStatsScriptMonster.Damage = statsPrefab.Damage;

            enemyController.characters.Add(monster.GetComponent<Character>());
        }

        // запуск битвы
        CurrentController = playerController;
        CurrentController.characters[indexCurrentCharacter].GetComponent<Image>().color = activeColorCharacter;
        CurrentController.StartStep(CurrentController.characters[indexCurrentCharacter]);
    }

    public void ControlEndStep(FightState state)
    {
        if (state == FightState.EndStep)
        {
            ControlSwitchTurn();
            if (ControlEndGame()) return;
            StartCoroutine(StartNewStep());
        }
    }

    private bool ControlEndGame()
    {
        if (playerController.characters.Count == 0)
        {
            UIFightManager.Instance.OpenEndGamePanel();
            return true;
        }
        else if (enemyController.characters.Count == 0)
        {
            UIFightManager.Instance.OpenWinPanel();
            //loadSave.SaveInventory();
            return true;
        }

        return false;
    }

    private void ControlSwitchTurn()
    {
        if (indexCurrentCharacter + 1 >= CurrentController.characters.Count)
        {
            CurrentController.characters[indexCurrentCharacter].GetComponent<Image>().color = basicColorCharacter;
            IsStepFriendly = !IsStepFriendly;
            if (IsStepFriendly)
            {
                CurrentController = playerController;
                
                UIFightManager.Instance.OpenActionPanel();
                ControlDeleteCharacters(playerController);
            }
            else
            {
                CurrentController = enemyController;
                UIFightManager.Instance.CloseAllPanel();
                ControlDeleteCharacters(enemyController);
            }
            
            indexCurrentCharacter = 0;
            CurrentController.characters[indexCurrentCharacter].GetComponent<Image>().color = activeColorCharacter;
        }
        else
        {
            CurrentController.characters[indexCurrentCharacter].GetComponent<Image>().color = basicColorCharacter;
            indexCurrentCharacter++;
            CurrentController.characters[indexCurrentCharacter].GetComponent<Image>().color = activeColorCharacter;
            
            if (IsStepFriendly)
            {
                UIFightManager.Instance.CloseTargetPanel();
                UIFightManager.Instance.OpenActionPanel();
            }

            if (IsStepFriendly) ControlDeleteCharacters(enemyController);
            else ControlDeleteCharacters(playerController);
        }

        ControlDeleteCharacters(playerController);
        ControlDeleteCharacters(enemyController);

    }

    private void ControlDeleteCharacters(FightController controller)
    {
        //foreach (Character character in controller.characters)
        //{
        //    if (character.BaseStatsScript == null || character.BaseStatsScript.Health <= 0)
        //    {
        //        controller.characters.Remove(character);
        //        (enemyController as EnemyFightController).opponents.Remove(character);
        //    }
        //}

        for (int i = 0; i < controller.characters.Count; i++)
        {
            if (controller.characters[i].BaseStatsScript == null || controller.characters[i].BaseStatsScript.Health <= 0)
            {
                if (controller is PlayerFightController)
                {
                    (enemyController as EnemyFightController).opponents.RemoveAt(i);
                }

                controller.characters.RemoveAt(i);
                i--;
            }
                
            
        }
    }

    private IEnumerator StartNewStep()
    {
        yield return new WaitForSeconds(1);

        CurrentController.StartStep(CurrentController.characters[indexCurrentCharacter]);
    }
}
