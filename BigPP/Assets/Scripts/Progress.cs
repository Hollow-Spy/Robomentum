using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Progress {
    public int skill_points;
    public float[] personal_best = new float[4];
    public Skill[] skills_unlocked = new Skill[11];

    public Progress() {
        skill_points = 10;

        for (int i = 0; i < 4; ++i) {
            personal_best[i] = 120f;
        }
    }

    public void compare_time(int level, float time) {
        if (personal_best[level] > time) {
            personal_best[level] = time;
        }
    }

    public void save_progress(SkillTree tree) {
        skill_points = tree.skill_points;
        skills_unlocked = tree.skills;
        SystemSave.SaveProgress(this);
    }
    public void load_progress() {
        Progress data = SystemSave.LoadProgress();

        this.personal_best = data.personal_best;
        this.skill_points = data.skill_points;
        this.skills_unlocked = data.skills_unlocked;
    }
}

[System.Serializable]

public class Skill {
    public string name;
    public int level;
    public bool acquired;
    public int cost;
    public Skill(string n, int l, bool a, int c) {
        name = n;
        level = l;
        acquired = a;
        cost = c;
    }
}