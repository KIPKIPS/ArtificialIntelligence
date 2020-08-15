using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float Dup;
    public float Dright;
    public float speed=10;

    private float currentVelocityUp;
    private float currentVelocityRight;

    private int hp = 10;
    private GameObject bullet;
    private Vector3 forward;

    public GameObject firePos;
    // Start is called before the first frame update
    void Start() {
        bullet = Resources.Load<GameObject>("AIDemo_01/PlayerBullet");
    }

    // Update is called once per frame
    void Update() {
        Dup = Mathf.SmoothDamp(Dup,(Input.GetKey(KeyCode.W) ? 1 : 0)- (Input.GetKey(KeyCode.S) ? 1 : 0), ref currentVelocityUp,0.2f);
        Dright = Mathf.SmoothDamp(Dright,(Input.GetKey(KeyCode.D) ? 1 : 0)- (Input.GetKey(KeyCode.A) ? 1 : 0), ref currentVelocityRight,0.2f);
        transform.position+=new Vector3(Time.deltaTime*Dright*speed,0, Time.deltaTime * Dup*speed);
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground"));
        forward = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        
        transform.LookAt(forward);

        if (Input.GetMouseButtonDown(0)) {
            Fire();
        }
        Camera.main.transform.position = transform.position + new Vector3(1, 2, -4);
    }

    private Vector3 fireDir;
    void Fire() {
        fireDir = firePos.transform.position - transform.position;
        GameObject go=Instantiate(bullet,  firePos.transform.position+new Vector3(fireDir.x,0,fireDir.z), Quaternion.LookRotation(fireDir));
        go.GetComponent<Rigidbody>().AddForce(new Vector3(fireDir.x,0,fireDir.z).normalized*2000);
    }

    void OnCollisionEnter(Collision other) {
        if (other.transform.tag=="EnemyBullet") {
            hp--;
            Destroy(other.gameObject);
            if (hp==0) {
                Destroy(this.gameObject);
            }
        }
    }
}
