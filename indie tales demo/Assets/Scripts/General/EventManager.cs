using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    private static EventManager _instance;

    //Events
    public event EventHandler OnJumpInitiated, OnPlayerGetsHit, 
        OnPlayerDeath, OnPlayerLittleAttack, OnPlayerHeavyAttack,
        OnStaminaLow, OnStaminaNotLow, OnHealthLow, OnHealthNotLow;

    public event PositionEvent OnEnemyStartChase, OnEnemyAttack, OnEnemyGetsHit, OnEnemyDie, OnBlockInitiated;

    public delegate void PositionEvent(Vector2 position);

    public static EventManager Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NotifyOfOnJumpInitiated(object sender) {
        OnJumpInitiated?.Invoke(sender, EventArgs.Empty); 
    }

    public void NotifyOfOnPlayerGetsHit(object sender) {
        OnPlayerGetsHit?.Invoke(sender, EventArgs.Empty); 
    }
    public void NotifyOfOnPlayerDeath(object sender) {
        OnPlayerDeath?.Invoke(sender, EventArgs.Empty); 
    }
    public void NotifyOfOnPlayerLittleAttack(object sender) {
        OnPlayerLittleAttack?.Invoke(sender, EventArgs.Empty);
    }
    public void NotifyOfOnPlayerHeavyAttack(object sender) {
        OnPlayerHeavyAttack?.Invoke(sender, EventArgs.Empty);
    }
    public void NotifyOfOnEnemyAttack(Vector2 senderPosition) {
        OnEnemyAttack?.Invoke(senderPosition);
    }
    public void NotifyOfOnBlockInitiated(Vector2 senderPosition) {
        OnBlockInitiated?.Invoke(senderPosition);
    }
    public void NotifyOfOnEnemyGetsHit(Vector2 senderPosition) {
        OnEnemyGetsHit?.Invoke(senderPosition);
    }
    public void NotifyOfOnEnemyDie(Vector2 senderPosition) {
        OnEnemyDie?.Invoke(senderPosition);
    }
    public void NotifyOfOnStaminaLow(object sender) {
        OnStaminaLow?.Invoke(sender, EventArgs.Empty);
    }
    public void NotifyOfOnStaminaNotLow(object sender) {
        OnStaminaNotLow?.Invoke(sender, EventArgs.Empty);
    }
    public void NotifyOfOnHealthLow(object sender) {
        OnHealthLow?.Invoke(sender, EventArgs.Empty);
    }
    public void NotifyOfOnHealthNotLow(object sender) {
        OnHealthNotLow?.Invoke(sender, EventArgs.Empty);
    }
    public void NotifyOfOnEnemyStartChase(Vector2 senderPosition) {
        OnEnemyStartChase?.Invoke(senderPosition);
    }    
}