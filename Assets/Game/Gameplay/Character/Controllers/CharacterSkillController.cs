using UnityEngine;

public class CharacterSkillController : MonoBehaviour
{
    public void ActivateSkill(string skillName)
    {
        switch (skillName)
        {
            case "Skill1":
                Debug.Log("Skill1 activated");
                break;
            case "Skill2":
                Debug.Log("Skill2 activated");
                break;
            case "Skill3":
                Debug.Log("Skill3 activated");
                break;
            default:
                Debug.LogWarning("Skill inconnue : " + skillName);
                break;
        }
    }
}