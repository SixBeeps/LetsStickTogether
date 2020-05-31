using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col) {
        if (col.transform.tag == "Finish") {
            StickFigure.main.HitTarget(col.rigidbody);
        }
        if (col.transform.tag == "Tnt") {
            Vector2 toTnt = Vector2.MoveTowards(transform.position, col.transform.position, 1f);
            GetComponent<Rigidbody2D>().AddForce(toTnt * -10, ForceMode2D.Impulse);
            col.gameObject.GetComponent<AudioSource>().Play();
            col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            col.collider.enabled = false;
            StickFigure.main.Ded();
        }
    }
}
