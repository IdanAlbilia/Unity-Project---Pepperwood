using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerControllerRPG : MonoBehaviour
{
    public LayerMask movementMask;

    PlayerMotor motor;
    Camera cam;

    public Interactable focus;

    public MoveType moveType = MoveType.Melee;

    SpawnProjectile spawnProjectile;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        spawnProjectile = GetComponent<SpawnProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            moveType = MoveType.Spell;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            moveType = MoveType.Melee;

        }
       
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("we hit " + hit.collider.name);

                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            if (moveType == MoveType.Spell && newFocus.GetComponent<Enemy>() != null);
            else if (moveType == MoveType.Melee)
            {
                motor.FollowTarget(newFocus);
            }
            else
                motor.FollowTarget(newFocus);
        }
        if (moveType == MoveType.Spell && newFocus.GetComponent<Enemy>() != null) // in case newFocus is an enemy and Spell is on
        {
            motor.LookAt(newFocus);
            spawnProjectile.SetTimeToFire(Time.time + 1 / spawnProjectile.GetEffect().GetComponent<ProjectileMove>().fireRate);
            spawnProjectile.SpawnVFX();
        }
        else
            newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if(focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}

public enum MoveType { Melee, Spell, Teleport }
