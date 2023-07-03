using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    float run_time;
    public Progress current_progress;
    public SkillTree current_tree;
    public int level;
    void Start()
    {
        level = 0;
        run_time = 120 + 2; //2 seconds for like 3,2,1 go
        current_progress = new Progress();
        current_tree = new SkillTree();
        current_progress.load_progress();
    }

    // Update is called once per frame
    void Update()
    {
        run_time -= Time.deltaTime;
    }

    public void end_level()
    {
        current_progress.compare_time(level, run_time);
        current_tree.skill_points += Mathf.CeilToInt(run_time);
        current_progress.save_progress(current_tree);
        //SceneManager.SetActiveScene("Ice level");
    }
}