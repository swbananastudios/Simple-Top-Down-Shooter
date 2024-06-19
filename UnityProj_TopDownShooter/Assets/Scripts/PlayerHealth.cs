using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    public static Action PlayerDeathAction;
    public static Action<int> PlayerDamageAction;

    private Tweener tweener;

    public void Damage(int damage)
    {
        // Decrease player health depending on the damage parameter passed
        health -= damage;

        // Tween the transform of the player for user feedback (We call this Game Juice or Game Polish)
        tweener.Complete();
        tweener = transform.DOPunchScale(transform.localScale * 0.4f, 0.1f);

        // Invoke PlayerDamageAction to send a signal to whichever is subscribed to it. For this case the UI observes this event and will update the health in UI when it gets invoked
        PlayerDamageAction?.Invoke(health);

        // When health reaches 0, run Death() method
        if (health == 0)
            Death();
    }

    public void Death()
    {
        // Unvoke PlayerDeath Action to send a signal to whichever is subscribed to it. For this case it tells the game manager to call the game over method
        PlayerDeathAction?.Invoke();
    }
}
