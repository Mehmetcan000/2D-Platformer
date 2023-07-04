using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TreeHealth : Health , IDamageable<int>
{

    public SpriteRenderer spriteRenderer;
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        
        HitFeedBack();
        if (CheckIfWeDead())
        {
            OnDeath();
        }
        
    }

    protected override void HitFeedBack()
    {
        base.HitFeedBack();
        
        this.gameObject.transform.DOShakePosition(0.01f,new Vector3(.5f,0.05f,0), 50, 50);
        Tween colorTween = spriteRenderer.DOBlendableColor(Color.red, .1f);
         colorTween.OnComplete(() =>
            spriteRenderer.DOBlendableColor(Color.white, .05f));
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        this.gameObject.transform.DOShakePosition(0.1f,new Vector3(.5f,0.05f,0), 50, 50);
         Tween colorTween = spriteRenderer.DOBlendableColor(Color.red, .1f);
           colorTween.OnComplete(() => Destroy(gameObject));
    }
}
