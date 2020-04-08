using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public CharacterView player;
    public GameObject ExitObject;
    Vector3 ExitPoint;

    private void Init()
    {
        ExitPoint = ExitObject.transform.position;
    }

    private void Awake()
    {
        Init();
    }
}
