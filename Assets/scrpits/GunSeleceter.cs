using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GunSettings", menuName = "ScriptableObjects/GunSelectorObject", order = 2)]
public class GunSeleceter : ScriptableObject
{
    private int selectedIndex;

    public void SetIndex(int idx) {
        selectedIndex = idx;
    }

    public int GetIndex() {
        return selectedIndex;
    }

}
