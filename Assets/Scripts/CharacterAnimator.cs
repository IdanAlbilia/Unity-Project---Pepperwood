using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    protected const float locomationAnimationSmoothTime = .1f;

    protected NavMeshAgent agent;
    protected Animator animator;

    public AnimatorOverrideController overrideController;
    protected CharacterCombat combat;

    bool dead = false;

    PlayerControllerRPG playerController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
        playerController = GetComponent<PlayerControllerRPG>();

        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (dead)
            return;
       
        float speedPercentage = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercentage", speedPercentage, .1f, Time.deltaTime);

        animator.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        if (dead)
            return;
        //if(playerController && playerController.mo)
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim] = currentAttackAnimSet[attackIndex];
    }

    protected virtual void OnDefend()
    {
        //animator.SetTrigger("defend");
    }

    public void Die(CharacterStats dyingChar, bool isPlayer)
    {
        dead = true;
        if (animator)
        {
            animator.SetTrigger("dead");
            StartCoroutine(DoAnimation(dyingChar, isPlayer));
        }
    }

    IEnumerator DoAnimation(CharacterStats dyingChar, bool isPlayer)
    {
        yield return new WaitForSeconds(1.75f);
        if (isPlayer)
            PlayerManager.instance.KillPlayer();
        else
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().addExp(dyingChar.getExp(), "Enemy");
            foreach (Quest quest in PlayerManager.instance.GetQuests())
            {
                quest.Objective(dyingChar.gameObject);
                Debug.Log("called objective on quest: " + quest.name + " with tag: " + dyingChar.gameObject.tag);
            }
            Vector3 position = transform.position;
            LootManager.instance.DropItem(position);
            Destroy(dyingChar.gameObject);
        }

    }
}
