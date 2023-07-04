using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BarrelHealth : Health , IDamageable<int>
{

    [SerializeField] private SpriteRenderer _spriteRenderer;

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
        Tween colorTween = _spriteRenderer.DOBlendableColor(Color.green, .1f);
        colorTween.OnComplete(() =>
            _spriteRenderer.DOBlendableColor(Color.white, .05f));
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        
        this.gameObject.transform.DOShakePosition(0.01f,new Vector3(.5f,0.05f,0), 50, 50);
        Tween colorTween = _spriteRenderer.DOBlendableColor(Color.green, .1f);
        colorTween.OnComplete(() => Destroy(gameObject));
    }
}
