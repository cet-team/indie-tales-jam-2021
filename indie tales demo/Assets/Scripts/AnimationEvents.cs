using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour {
    private GameObject player;

    private void OnEnable() {
        player = GameObject.FindWithTag("Player");
    }

    public void Attack() {
        player.GetComponent<PlayerController>().AttackCalledbyAnimationEvents();
    }
}