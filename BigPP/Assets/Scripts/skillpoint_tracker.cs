using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillpoint_tracker : MonoBehaviour
{
    public SkillTree tree;
    Text text;
    private void Start() {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = tree.skill_points.ToString();
    }
}
