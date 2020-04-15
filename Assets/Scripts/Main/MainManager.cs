using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public CharacterView player;
    //TODO: test only! Use EnemyManager, pool and attack queue
    public CharacterView[] Enemyes;
    public GameObject ExitObject;
    Vector3 ExitPoint;

    public string[] tagsForEnemy;

    private CharacterPresenter playerPresenter;

    private void Init()
    {
        ExitPoint = ExitObject.transform.position;
        player.exitPoint = ExitPoint;
        playerPresenter = player._presenter;

        InitEnemyes();
    }

    private void InitEnemyes()
    {
        foreach (CharacterView _cv in Enemyes)
        {
            _cv.exitPoint = ExitPoint;
        }
    }

    private void Awake()
    {
        Init();
    }
}
