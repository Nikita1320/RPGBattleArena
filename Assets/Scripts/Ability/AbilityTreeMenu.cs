using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTreeMenu : MonoBehaviour
{
    [SerializeField] private CharactersPanel charactersPanel;
    [SerializeField] private AbilityDescription abilityDescription;
    [SerializeField] private Button improveButton;
    [SerializeField] private AbilityCell[] cellsFirstBranch;
    [SerializeField] private AbilityCell[] cellsSecondBranch;
    [SerializeField] private AbilityCell[] cellsThirdBranch;
    [SerializeField] private Text freePoints;
    private Dictionary<int, AbilityCell[]> branchs = new();

    private AbilityCell selectedAbilityCell;
    private int bracnhSelectedAbility;

    private Character character;

    public void OpenAbilityTreeMenu()
    {
        if (charactersPanel.DemonstrationCharacter == null)
        {
            return;
        }
        character = charactersPanel.DemonstrationCharacter;
        freePoints.text = "Free point: " + character.AbilityTree.FreePoints.ToString();
        gameObject.SetActive(true);
        abilityDescription.gameObject.SetActive(false);
        InitializeAbilityCell();
    }
    private void Init()
    {
        branchs.Add(0, cellsFirstBranch);
        branchs.Add(1, cellsSecondBranch);
        branchs.Add(2, cellsThirdBranch);

        improveButton.onClick.AddListener(() => ImproveAbility());

        foreach (var branch in branchs)
        {
            foreach (var item in branch.Value)
            {
                item.CellButton.onClick.AddListener(() => SelectAbility(branch.Key, item));
            }
        }
    }
    private void InitializeAbilityCell()
    {
        Debug.Log(character.Rank);
        if (branchs.Count == 0)
        {
            Init();
        }
        for (int i = 0; i < branchs.Count; i++)
        {
            for (int j = 0; j < branchs[i].Length; j++)
            {
                branchs[i][j].Init(character.AbilityTree.AbilityBranches[i].Abilities[j]);
            }
        }

        if (character.Rank >= character.AbilityTree.OpenBranchRanks[2])
        {
            Debug.Log("OpenAllBranch");
            for (int i = 0; i < branchs.Count; i++)
            {
                for (int j = 0; j < branchs[i].Length; j++)
                {
                    branchs[i][j].UnLockCell();
                    if (character.AbilityTree.AbilityBranches[i].ImprovmentAbility[branchs[i][j].Ability] > 0)
                    {
                        branchs[i][j].RemoveNotActiveImage();
                    }
                    else
                    {
                        branchs[i][j].SetNotActiveImage();
                    }
                }
            }
        }
        else if (character.Rank >= character.AbilityTree.OpenBranchRanks[1])
        {
            Debug.Log("Open 1 and 2");
            for (int i = 0; i < branchs.Count - 1; i++)
            {
                for (int j = 0; j < branchs[i].Length; j++)
                {
                    branchs[i][j].UnLockCell();
                    if (character.AbilityTree.AbilityBranches[i].ImprovmentAbility[branchs[i][j].Ability] > 0)
                    {
                        branchs[i][j].RemoveNotActiveImage();
                    }
                    else
                    {
                        branchs[i][j].SetNotActiveImage();
                    }
                }
            }
        }
        else if(character.Rank >= character.AbilityTree.OpenBranchRanks[0])
        {
            Debug.Log("Open 1");
            for (int i = 0; i < branchs.Count - 2; i++)
            {
                for (int j = 0; j < branchs[i].Length; j++)
                {
                    branchs[i][j].UnLockCell();
                    if (character.AbilityTree.AbilityBranches[i].ImprovmentAbility[branchs[i][j].Ability] > 0)
                    {
                        branchs[i][j].RemoveNotActiveImage();
                    }
                    else
                    {
                        branchs[i][j].SetNotActiveImage();
                    }
                }
            }
        }
    }
    private void SelectAbility(int branch, AbilityCell abilityCell)
    {
        bracnhSelectedAbility = branch;
        selectedAbilityCell = abilityCell;

        RenderDescription();
    }
    public void ImproveAbility()
    {
        character.AbilityTree.UpgradeAbility(bracnhSelectedAbility, selectedAbilityCell.Ability);
        if (character.AbilityTree.AbilityBranches[bracnhSelectedAbility].ImprovmentAbility[selectedAbilityCell.Ability] > 0)
        {
            selectedAbilityCell.RemoveNotActiveImage();
        }
        freePoints.text = "Free point: " + character.AbilityTree.FreePoints.ToString();

        RenderDescription();
    }
    private void RenderDescription()
    {
        improveButton.interactable = false;

        abilityDescription.Init(selectedAbilityCell.Ability, 
            character.AbilityTree.AbilityBranches[bracnhSelectedAbility].ImprovmentAbility[selectedAbilityCell.Ability]);
        abilityDescription.gameObject.SetActive(false);
        abilityDescription.gameObject.SetActive(true);

        if (selectedAbilityCell.IsLock)
        {
            improveButton.interactable = false;
            return;
        }

        if (character.AbilityTree.FreePoints == 0)
        {
            improveButton.interactable = false;
            return;
        }

        if (selectedAbilityCell.Ability == branchs[bracnhSelectedAbility][0].Ability)
        {
            Debug.Log("Its Active Ability");
            if (character.AbilityTree.AbilityBranches[bracnhSelectedAbility].ImprovmentAbility[selectedAbilityCell.Ability] == character.AbilityTree.MaxLevelActiveAbility)
            {
                improveButton.interactable = false;
                return;
            }
        }
        else
        {
            Debug.Log("Its Passive Ability");
            if (character.AbilityTree.AbilityBranches[bracnhSelectedAbility].ImprovmentAbility[selectedAbilityCell.Ability] == character.AbilityTree.MaxLevelPassiveAbility)
            {
                improveButton.interactable = false;
                return;
            }
            else
            {
                if (!((character.AbilityTree.StepImprovedPassiveAbility + character.AbilityTree.AbilityBranches[bracnhSelectedAbility].ImprovmentAbility[branchs[bracnhSelectedAbility][0].Ability]) >= character.AbilityTree.AbilityBranches[bracnhSelectedAbility].ImprovmentAbility[selectedAbilityCell.Ability]
                    && character.AbilityTree.AbilityBranches[bracnhSelectedAbility].ImprovmentAbility[branchs[bracnhSelectedAbility][0].Ability] > 0))
                {
                    improveButton.interactable = false;
                    return;
                }
            }
        }
        improveButton.interactable = true;
    }
    private void OnDisable()
    {
        for (int i = 0; i < branchs.Count; i++)
        {
            for (int j = 0; j < branchs[i].Length; j++)
            {
                branchs[i][j].UnLockCell();
            }
        }
    }
}
