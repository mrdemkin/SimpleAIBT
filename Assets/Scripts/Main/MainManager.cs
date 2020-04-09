using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public CharacterView player;
    public GameObject ExitObject;
    Vector3 ExitPoint;

    private CharacterPresenter playerPresenter;

    private void Init()
    {
        ExitPoint = ExitObject.transform.position;
        playerPresenter = player._presenter;

    }

    private void Awake()
    {
        Init();
    }
}
