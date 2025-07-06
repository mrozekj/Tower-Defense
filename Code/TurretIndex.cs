using System;
using UnityEngine;

[Serializable]
public class TurretIndex {

    public string name;
    public int cost;
    public GameObject prefab;  // The GameObject prefab associated with the turret

    public TurretIndex (string _name, int _cost, GameObject _prefab) {    // Constructor to initialize a new TurretIndex
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }

}