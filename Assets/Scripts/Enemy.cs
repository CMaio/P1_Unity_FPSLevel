using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum ENEMY_STATE
    {
        PATROL,CHASING,IDLE, ALERT, HIT, DIE
    }

    [SerializeField] ENEMY_STATE currentState = ENEMY_STATE.IDLE;
    [SerializeField] float minDistanceToAlert = 15;
    //[SerializeField] LayerMask colllisionLayer;
    [SerializeField] float minDistanceToAttack = 5;
    [SerializeField] float distanceToChangePointPatrol = 1;
    [SerializeField] float ConeAngle = 30;
    //[SerializeField] float LerpAttackRotation = 0.6f;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform PatrolPointsPool;
    [SerializeField] List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] Transform player;
    [SerializeField] LayerMask layerMaskDrone;
    int nextPatrolPoint = 0;
    ENEMY_STATE lastState = ENEMY_STATE.IDLE;
    float damping = 0.1f;

    [SerializeField] List<GameObject> itemsDrop;
    [SerializeField] GameObject particleExplosion;

    [SerializeField] GameObject bodyEnemy;

    bool canShoot;
    [SerializeField] int timeBetweenShoot  = 2;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform pointToShoot;
    [SerializeField] float shootSpeed;

    private void Awake()
    {
        canShoot = true;
        foreach (Transform child in PatrolPointsPool)
        {
            patrolPoints.Add(child);
        }
    }

    private void Start()
    {
        GetComponent<EnemyHealthSystem>().setDieFunction(
               () =>
               {
                   Die();
               }
            );

    }

    void Update()
    {
        switch (currentState)
        {
            case ENEMY_STATE.ALERT:
                updateAlert();
                break;
            case ENEMY_STATE.PATROL:
                updatePatrol();
                break;
            case ENEMY_STATE.CHASING:
                updateChasing();
                break;
            case ENEMY_STATE.HIT:
                updateHIT();
                break;
            case ENEMY_STATE.DIE:
                updateDIE();
                break;
            default:
                updateIdle();
                break;
        }
    }
    void setLastState()
    {
        currentState = lastState;
    }
    void SetAlertState()
    {
        lastState = currentState;
        currentState = ENEMY_STATE.ALERT;
    }
    void SetPatrolState()
    {
        lastState = currentState;
        currentState = ENEMY_STATE.PATROL;
    }
    void SetChasingState()
    {
        lastState = currentState;
        currentState = ENEMY_STATE.CHASING;
    }
    public void SetHITstate()
    {
        lastState = currentState;
        currentState = ENEMY_STATE.HIT;
    }
    void SetDIEstate()
    {
        lastState = currentState;
        currentState = ENEMY_STATE.DIE;
    }
    void SetIdleState()
    {
        lastState = currentState;
        currentState = ENEMY_STATE.IDLE;
    }

    void updateAlert()
    {
        if (!HearsPlayer())
        {
            SetPatrolState();
            agent.isStopped = false;
        }

        if (SeesPlayer())
        {
            SetChasingState();
            agent.isStopped = false;
        }

        transform.Rotate(new Vector3(0, 0.3f, 0));
    }
    void updatePatrol()
    {
        if (HearsPlayer())
        {
            SetAlertState();
            agent.isStopped = true;
        }
        if (Vector3.Distance(patrolPoints[nextPatrolPoint].position, transform.position) < distanceToChangePointPatrol)
        {
            MoveToNextPatrolPosition();
        }
        agent.destination = patrolPoints[nextPatrolPoint].position;
        transform.rotation = Quaternion.Lerp(transform.rotation, patrolPoints[nextPatrolPoint].rotation, Time.deltaTime * damping);
    }

    void updateChasing()
    {
        if (!HearsPlayer())
        {
            SetPatrolState();
        }
        if(HearsPlayer() && !SeesPlayer())
        {
            SetAlertState();
            agent.isStopped = true;
        }
        if (Vector3.Distance(player.position, transform.position) < minDistanceToAttack)
        {
            agent.isStopped = true;
            Attack();
        }
        else
        {
            SetNextChasePosition();
        }
    }
    void updateHIT()
    {
        if (lastState == ENEMY_STATE.PATROL || lastState == ENEMY_STATE.IDLE)
            SetAlertState();
        else
            setLastState();
    }
    void updateDIE()
    {

    }
    void updateIdle()
    {
        StartCoroutine(idleToPatrol());
    }
    void SetNextChasePosition()
    {
        agent.isStopped = false;
        agent.destination = player.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, Time.deltaTime * damping);
    }
    void MoveToNextPatrolPosition()
    {
        nextPatrolPoint++;
        if (nextPatrolPoint == patrolPoints.Count) nextPatrolPoint = 0;
    }
    bool SeesPlayer()
    {
        Vector3 dir = player.position - transform.position;

        Debug.DrawRay(transform.position, dir * 1000, Color.green);
        Debug.DrawRay(transform.position, transform.forward * 1000, Color.cyan);
        RaycastHit hitRotate;
        if (Physics.Raycast(transform.position, dir, out hitRotate, 100, layerMaskDrone))
        {
            Debug.DrawRay(transform.position, dir * 1000, Color.red);
            float angle = Vector3.Angle(dir, transform.forward);
            if(angle < ConeAngle)
                return true;
        }
        return false;
    }
    bool HearsPlayer()
    {
        if (Vector3.Distance(player.position, transform.position) < minDistanceToAlert)
            return true;
        return false;
    }
    void Attack()
    {
        if (canShoot)
        {
            canShoot = false;
            Vector3 dir = player.position - pointToShoot.position;

            GameObject curretnBullet = Instantiate(bullet, pointToShoot.position, pointToShoot.rotation);

            curretnBullet.GetComponent<Rigidbody>().velocity = dir * shootSpeed;
            StartCoroutine(waitToShoot());
        }
    }

    IEnumerator waitToShoot()
    {
        yield return new WaitForSeconds(timeBetweenShoot);
        canShoot = true;
    }

    IEnumerator idleToPatrol()
    {
        yield return new WaitForSeconds(3);
        SetPatrolState();
    }


    void Die()
    {
        GameObject itemToDrop;
        itemToDrop = itemsDrop[UnityEngine.Random.Range(0, itemsDrop.Count)];
        GameObject expltmp = Instantiate(particleExplosion, bodyEnemy.transform.position, transform.rotation);
        Instantiate(itemToDrop, transform.position, itemToDrop.transform.rotation);

        float totalDuration = expltmp.GetComponent<ParticleSystem>().main.duration + expltmp.GetComponent<ParticleSystem>().main.startLifetime.constantMin;
        StartCoroutine(waitToDestroy(expltmp,totalDuration));
    }

    IEnumerator waitToDestroy(GameObject tmp,float timeDestroy)
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(tmp);
        Destroy(this.gameObject);
    }
}
