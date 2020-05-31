using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StickFigure : MonoBehaviour
{
    public static StickFigure main;
    public bool isLaunching = false;
    public Transform head;
    public float launchMultiplier;
    public float yRespawn = -10f;
    public bool allowMovement = true;
    public AudioSource swingSound;
    private LineRenderer lRender;
    private bool hasHit = false;
    private bool hasDied = false;

    private Vector2 startPos;
    private Vector2 curPos;
    void Start()
    {
        main = this;
        lRender = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (allowMovement) {
            if (Input.GetMouseButtonDown(0)) {
                startPos = GetCurrentMousePosition().GetValueOrDefault();
                lRender.SetPosition(0, head.position);
                lRender.positionCount = 1;
                lRender.enabled = true;
            } else if (Input.GetMouseButton(0)) {
                curPos = GetCurrentMousePosition().GetValueOrDefault();
                lRender.positionCount = 2;
                lRender.SetPosition(0, head.position);
                lRender.SetPosition(1, curPos);

            } else if (Input.GetMouseButtonUp(0)) {
                lRender.enabled = false;
                var releasePosition = GetCurrentMousePosition().GetValueOrDefault();
                var direction = releasePosition - startPos;
                head.GetComponent<Rigidbody2D>().AddForce(direction * launchMultiplier, ForceMode2D.Impulse);
                if (!UIManager.main.startTime.HasValue) {
                    UIManager.main.startTime = System.DateTime.Now;
                }
                if (swingSound) swingSound.Play();
            }
        }
        if (head.position.y <= yRespawn) {
            Ded();
        }
    }

    public void HitTarget(Rigidbody2D other) {
        if (hasDied) return;
        if (!hasHit) {
            hasHit = true;
            foreach(SpringJoint2D spring in FindObjectsOfType<SpringJoint2D>()) {
                spring.enabled = false;
            }
            UIManager.main.startTime = null; // Stops the timer
            UIManager.main.winImg.gameObject.SetActive(true);
            if (PlayerPrefs.GetInt("bestLevel", 0) < LevelManager.main.levelNumber) {
                PlayerPrefs.SetInt("bestLevel", LevelManager.main.levelNumber);
            }
            enabled = false;
        }
    }

    public void Ded() {
        if (hasHit) return;
        lRender.enabled = false;
        foreach(SpringJoint2D spring in GetComponentsInChildren<SpringJoint2D>()) {
            spring.enabled = false;
        }
        if (UIManager.main.loseImg) UIManager.main.loseImg.gameObject.SetActive(true);
        hasDied = true;
        enabled = false;
    }

    public void ToggleAllowMovement() {
        allowMovement = !allowMovement;
    }

    private Vector2? GetCurrentMousePosition() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        float rayDistance;
        if (plane.Raycast(ray, out rayDistance)) {
            return ray.GetPoint(rayDistance);

        }

        return null;
    }
}
