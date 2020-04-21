using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("PathFinding")]
    public List<GameObject> path = new List<GameObject>();
    [Space]
    [Header("Variables")]
    public bool hostile;
    public int damage, i = 0;
    public float rotSpeed, range, angle, speed, runSpeed, rays;

    private FOV fOv;

    public GameObject pfFFOV, originPos;
    public GameObject player, point;

    private Rigidbody rb;

    protected virtual void SetRays(int rays) {
        this.rays = rays;
    }

    protected virtual void SetAngle(float angle) {
        this.angle = angle;
    }

    protected virtual void SetDetection() {
        fOv = Instantiate(pfFFOV, null).GetComponent<FOV>();
    }

    protected virtual void SetRun(float runSpeed) {
        this.runSpeed = runSpeed;

    }

    public float GetRun() {
        return runSpeed;
    }

    protected virtual void SetDamage(int dmg) {
        this.damage = dmg;
    }

    public float GetDamage() {
        return damage;
    }

    protected virtual void SetRotSpeed(float rotSpeed) {
        this.rotSpeed = rotSpeed;
    }

    public float GetRotSpeed() {
        return rotSpeed;
    }

    protected virtual void SetRb(Rigidbody rb) {
        this.rb = rb;
        rb.isKinematic = true;
    }

    public Rigidbody GetRb() {
        return rb;
    }

    protected virtual void SetRange(float range) {
        this.range = range;
    }

    public float GetRange() {
        return range;
    }

    protected virtual void SetSpeed(float speed) {
        this.speed = speed;
    }

    public float GetSpeed() {
        return speed;
    }


    public GameObject Detection() {
        GameObject player = null;
        float startAngle = -(angle / 2);

        var offset = Quaternion.AngleAxis(-3 * rays / 2, Vector3.up);
        RaycastHit hit;
        var step = Quaternion.AngleAxis(3, Vector3.up);
        var rotation = transform.rotation;
        var origin = originPos.transform.position;
        Vector3 direction;

        List<Vector3> meshDetection = new List<Vector3> { new Vector3(0, 0, 0) };

        for (int i = 0; i < rays; i++) {
            float x = Mathf.Sin(Mathf.Deg2Rad * startAngle) * GetRange();
            float z = Mathf.Cos(Mathf.Deg2Rad * startAngle) * GetRange();

            direction = rotation * new Vector3(x, 0, z);

            if (Physics.Raycast(origin, direction.normalized, out hit, GetRange())) {
                Vector3 cHit = (hit.point - origin);

                meshDetection.Add(hit.point - origin);

                if (hit.collider.CompareTag("Entity")) {
                    player = hit.collider.gameObject;
                }

            } else {
                meshDetection.Add(direction);
            }
            startAngle += angle / rays;

        }

        fOv.GetComponent<FOV>().SetOrigin(origin);
        fOv.GetComponent<FOV>().SetVertices(meshDetection.ToArray());
        return player;

    }


    public void Move() {
        GameObject player = Detection();

        if (hostile) {
            if (player != null) {//Detection of Player/Entity
                Vector3 direction = player.transform.position - rb.transform.position;
                direction.y = 0;
                rb.transform.position += direction.normalized * GetSpeed() * Time.deltaTime * GetRun();
                //rotation
                direction.y = 0;
                Quaternion RotateTo = Quaternion.LookRotation(direction);
                rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, RotateTo, GetRotSpeed() * Time.deltaTime);
            } else {
                //Path
                if (path.Count > 0) {
                    point = path[i];

                    Vector3 direction = point.transform.position - rb.transform.position;
                    direction.y = 0;

                    rb.transform.position += direction.normalized * GetSpeed() * Time.deltaTime;

                    Quaternion RotateTo = Quaternion.LookRotation(direction);
                    rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, RotateTo, GetRotSpeed() * Time.deltaTime);

                    if (Vector3.Distance(rb.transform.position, point.transform.position) < 1) {
                        i++;
                        if (i > path.Count - 1) {
                            i = 0;
                        }
                    }

                }
            }
        }
        if (!hostile) {

            if (path.Count > 0) {
                point = path[i];

                Vector3 direction = point.transform.position - rb.transform.position;
                direction.y = 0;

                rb.transform.position += direction.normalized * GetSpeed() * Time.deltaTime;

                Quaternion RotateTo = Quaternion.LookRotation(direction);
                rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, RotateTo, GetRotSpeed() * Time.deltaTime);

                if (Vector3.Distance(rb.transform.position, point.transform.position) < 1) {
                    i++;
                    if (i > path.Count - 1) {
                        i = 0;
                    }
                }

            }
        }
    }

    private void Start() {

    }
    private void Update() {
        if (!player.GetComponent<Entity>().IsAlive) {
            hostile = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Entity")) {

            if (other.GetComponent<Entity>()) {
                Debug.Log("Ouch");
                other.GetComponent<Entity>().TakeDamage(damage);
                if (this.hostile) {
                    StartCoroutine(Wait());
                }
            }
        }
    }

    IEnumerator Wait() {
        hostile = false;
        yield return new WaitForSeconds(1f);
        hostile = true;
    }

}