using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Requirements : MonoBehaviour
{
    public GameObject[] linked_buttons;
    public SkillTree Tree;
    public string skill_name;
    public bool achieved;
    void Start()
    {
        GetComponent<Button>().interactable = false;
        Check();
    }

    public void Check() {
        for (int i = 0; i < Tree.skills.Length; i++) {
            if (Tree.skills[i].name == skill_name) {
                if (Tree.skills[i].acquired){
                    GetComponent<Button>().interactable = true;
                }
            }
        }
        if (achieved) {
            for (int i = 0; i < linked_buttons.Length; i++) {
                linked_buttons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    void Check_click() {
        GetComponent<Button>().interactable = achieved;
        if (achieved) {
            for (int i = 0; i < linked_buttons.Length; i++) {
                linked_buttons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Purchased() {
        achieved = true;
        Check_click();
    }

}
