using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillTree : MonoBehaviour
{
    public Progress current_progress;
    public Skill[] skills = new Skill[11];
    public int skill_points;

    public void create_tree()
    {
        skills = new Skill[]{ new Skill("Jump", 1, true, 1), new Skill("Sprint", 1, true, 1), new Skill("Climb", 1, true, 1),
            new Skill("Slide", 1, false, 1), new Skill("Dash", 1, false, 5), new Skill("Double jump", 5, false, 5),
            new Skill("Wall riding", 1, false, 50), new Skill("Slower falling", 1, false, 50),
            new Skill("Double dash", 1, false, 50), new Skill("Wall jump", 1, false, 50), new Skill("Triple jump", 1, false, 50),
        };
    }

    void Start()
    {
        create_tree();
        current_progress = new Progress();
        skill_points = current_progress.skill_points;
        load();
        Debug.Log("Skill points: " + skill_points);
    }

    void load()
    {
        current_progress.load_progress();
        skills = current_progress.skills_unlocked;
        skill_points = current_progress.skill_points;
    }

    public void buy_skill(string skill_name)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            Debug.Log("Skill: " + skills[i].name);
            if (skills[i].name == skill_name)
            {
                //Debug.Log("Skill points: " + skills[i].cost);
                if (skills[i].cost > skill_points)
                {
                    return;
                }
                else
                {
                    skill_points -= skills[i].cost;
                    Debug.Log("Skill points: " + skill_points);
                    if (skills[i].acquired == false)
                    {
                        skills[i].acquired = true;
                    }
                    skills[i].cost = (skills[i].cost / skills[i].level) * (skills[i].level + 1);
                    skills[i].level++;
                    Debug.Log("Skill :" + skills[i].name + " level: " + skills[i].level);
                    current_progress.save_progress(this);
                }
                break;
            }
        }
    }

    public void Reset()
    {
        current_progress = new Progress();
        create_tree();
        skill_points = 10;
        current_progress.save_progress(this);
        Debug.Log("Skill points: " + skill_points);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}