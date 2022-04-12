using UnityEngine;

public class PlayerInterface : MonoBehaviour //WIP
{
    GameObject[] rightButtons = new GameObject[4];

    void Awake()
    {
        for (int i = 0; i < rightButtons.Length; i++)
        {
            rightButtons[i] = GameObject.Find($"Button{i + 1}");
            rightButtons[i].SetActive(false);
        }
    }

    public void ToggleForm(int button)
    {
        rightButtons[button].SetActive(!rightButtons[button].activeInHierarchy);

        for (int i = 0; i < rightButtons.Length; i++)
        {
            if (i != button)
                rightButtons[i].SetActive(false);
        }
    }
}
