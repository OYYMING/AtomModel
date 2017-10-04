using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumBall : MonoBehaviour {

    Rigidbody obj1;
    Rigidbody obj2;
    Vector3 speed;
    float collisionV1;
    float collisionV2;

    // Use this for initialization
    void Start () {
        obj1 = this.GetComponent<Rigidbody>();
    }
	
	public void Shoot (Transform target, Vector3 speed) {
		obj2 = target.gameObject.GetComponent<Rigidbody>();
        this.speed = speed;

		float m1 = obj1.mass;
        float m2 = obj2.mass;
        float v1 = speed.magnitude;
        float v2 = 0f;
        PerfectElasticCollision(m1, m2, v1, v2, out collisionV1, out collisionV2);

        obj1.velocity = speed;
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// v1'=[(m1-m2）v1+2m2v2]/（m1+m2, v2'=[(m2-m1）v2+2m1v1]/（m1+m2)
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        obj1.velocity = speed.normalized * collisionV1;
        obj2.velocity = speed.normalized * collisionV2;
    }

	/// <summary>
	/// This method is used to calculate two objects' speeds after collision which is based on Perfect Elastic Collision
	/// </summary>
	static void PerfectElasticCollision (float m1, float m2, float v1, float v2, out float v1n, out float v2n) {
        v1n = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
        v2n = ((m2 - m1) * v2 + 2 * m1 * v1) / (m1 + m2);
    }
}
