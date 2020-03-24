using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject damageSource;
    public enum State {
        Idle, Seek, Attack, Die, Back
    }
    private Vector3 fireDir;
    public int hp = 10;
    public float attackDis = 20;
    public State curState = State.Idle;
    public Vector3 idlePos;
    public GameObject bullet;
    public Vector3 idleForward;
    private int index = 0;
    private Vector3 forward;
    public GameObject firePos;
    // Start is called before the first frame update
    void Start() {
        idlePos = transform.position;
        idleForward = transform.forward;
        damageSource = GameObject.FindWithTag("Player");
        bullet = Resources.Load<GameObject>("AIDemo_01/EnemyBullet");
    }

    // Update is called once per frame
    void Update() {
        if (damageSource != null) {
            forward = transform.forward;
            if (Vector3.Distance(damageSource.transform.position, transform.position) < attackDis) {
                curState = State.Attack;
            }
            switch (curState) {
                case State.Idle: Idle(); break;
                case State.Back:
                    Back();
                    if (Mathf.Abs(Vector3.Distance(idlePos, transform.position)) <= 0.2f) {
                        transform.forward = idleForward;
                        curState = State.Idle;
                    }
                    else {
                        transform.position += transform.forward.normalized * Time.deltaTime * 8;
                    }
                    break;
                case State.Seek: Seek(); break;
                case State.Die: Destroy(this.gameObject); break;
                case State.Attack: Attack(); break;
            }
            Debug.Log(curState);
        }
        else {
            Back();
            Idle();
            curState = State.Idle;
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "PlayerBullet") {
            hp--;
            Destroy(other.gameObject);
            if (hp == 0) {
                curState = State.Die;
            }
            else {
                curState = State.Seek;
            }
        }
    }

    void Idle() {
        if (Mathf.Abs(Vector3.Distance(idlePos, transform.position)) <= 0.2f) {
            transform.forward = idleForward;
            transform.position = idlePos;
        }
        curState = State.Idle;
    }
    void Back() {
        //缓动转向
        Vector3 temp = Vector3.Slerp(transform.forward, idlePos - transform.position, 0.5f);
        transform.forward = temp;
    }
    void Seek() {
        if (damageSource != null) {
            //缓动转向
            Vector3 temp = Vector3.Slerp(transform.forward, damageSource.transform.position - transform.position, 0.2f);
            transform.forward = temp;
            if (Vector3.Distance(damageSource.transform.position, transform.position) > attackDis) {
                transform.position += transform.forward.normalized * 0.1f;
            }
            else {
                curState = State.Attack;
            }
        }
    }


    void Attack() {
        if (damageSource != null) {
            index++;
            curState = State.Attack;
            Vector3 temp = Vector3.Slerp(transform.forward, damageSource.transform.position - transform.position, 0.2f);
            transform.forward = temp;
            if (Vector3.Distance(damageSource.transform.position, transform.position) > attackDis) {
                curState = State.Back;
            }
            else {
                if (index % 10 == 0) {
                    Fire();
                }
            }
        }
    }

    void Fire() {
        fireDir = firePos.transform.position - transform.position;
        GameObject go = Instantiate(bullet, firePos.transform.position + new Vector3(fireDir.x, 0, fireDir.z), Quaternion.LookRotation(fireDir));
        go.GetComponent<Rigidbody>().AddForce(new Vector3(fireDir.x, 0, fireDir.z).normalized * 1000);
    }
}
