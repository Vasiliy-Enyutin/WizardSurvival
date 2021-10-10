﻿using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    #region Fields

    private EnemyBaseState currentState;
    public event Action OnAnimationEnded;

    private CheckerPlayerInAttackingZone checkerPlayerInAttackingZone;
    private Animator enemyAnimator;
    private EnemyStats enemyStats;
    private NavMeshAgent enemyAgent;
    private Player player;

    #endregion

    #region Properties

    public EnemyMoveState EnemyMoveState { get; private set; }
    public EnemyAttackState EnemyAttackState { get; private set; }
    public EnemyReloadState EnemyReloadState { get; private set; }

    #endregion

    #region Methods

    private void Awake()
    {
        checkerPlayerInAttackingZone = GetComponent<CheckerPlayerInAttackingZone>();
        enemyAnimator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        enemyAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        InstantiateStates();
        TransitionToState(EnemyMoveState);
    }

    private void Update()
    {
        currentState.Update();
    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState();
    }

    private void InstantiateStates()
    {
        EnemyMoveState = new EnemyMoveState(this, enemyAnimator, enemyAgent, checkerPlayerInAttackingZone, player);
        EnemyAttackState = new EnemyAttackState(this, enemyAnimator, enemyStats, player);
        EnemyReloadState = new EnemyReloadState(this, enemyAnimator, enemyStats, checkerPlayerInAttackingZone);
    }

    private void EndOfAnimation()   // called when attack animation ended
    {
        OnAnimationEnded.Invoke();
    }

    #endregion
}
