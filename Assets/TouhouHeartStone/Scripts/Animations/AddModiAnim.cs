﻿using UI;
using UnityEngine;
using TouhouHeartstone;
namespace Game
{
    class AddModiAnim : EventAnimation<TouhouCardEngine.Card.AddModiEventArg>
    {
        AnimAnim _anim;
        public override bool update(TableManager table, TouhouCardEngine.Card.AddModiEventArg eventArg)
        {
            SimpleAnim simpleAnim = null;
            Animator animator = null;
            if (table.tryGetServant(eventArg.card, out var servant))
            {
                animator = servant.animator;
                if (eventArg.modifier is AttackModifier atkMod)
                {
                    if (atkMod.value > 0)
                        simpleAnim = servant.onAttackUp;
                    else
                        simpleAnim = servant.onAttackDown;
                }
                else if (eventArg.modifier is LifeModifier lifMod)
                {
                    if (lifMod.value > 0)
                        simpleAnim = servant.onLifeUp;
                    else
                        simpleAnim = servant.onLifeDown;
                }
            }
            else if (table.tryGetHand(eventArg.card, out var hand))
            {
                animator = hand.animator;
                if (eventArg.modifier is AttackModifier atkMod)
                {
                    if (atkMod.value > 0)
                        simpleAnim = hand.onAttackUp;
                    else
                        simpleAnim = hand.onAttackDown;
                }
                else if (eventArg.modifier is LifeModifier lifMod)
                {
                    if (lifMod.value > 0)
                        simpleAnim = hand.onLifeUp;
                    else
                        simpleAnim = hand.onLifeDown;
                }
                else if (eventArg.modifier is CostModifier costMod)
                {
                    if (costMod.value > 0)
                        simpleAnim = hand.onCostUp;
                    else
                        simpleAnim = hand.onCostDown;
                }
            }
            simpleAnim.beforeAnim.Invoke();
            if (!string.IsNullOrEmpty(simpleAnim.animName))
            {
                if (_anim == null)
                    _anim = new AnimAnim(animator, simpleAnim.animName);
                if (!_anim.update(table))
                    return false;
            }
            simpleAnim.afterAnim.Invoke();
            return true;
        }
    }
}
