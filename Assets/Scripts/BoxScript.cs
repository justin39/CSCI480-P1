using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField]
    float lerpTime;
    [SerializeField]
    GameObject DiePrefab;

    private Vector2 startPos;
    private Vector3 destination;
    private List<GameObject> dice;

    void Start()
    {
        destination = new Vector3(0, -0.5f, 0);
        dice = new List<GameObject>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            destination.y = (startPos.y - Input.GetTouch(0).position.y) / 500 + -0.5f;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
        {
            destination = new Vector3(0, -0.5f, 0);
        }
    }

    void FixedUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, destination, 0.1f);
    }

    public void Reset()
    {
        foreach (GameObject die in dice)
        {
            die.transform.localPosition = new Vector3(0, 0.5f, 0);
            die.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void AddDie()
    {
        if (dice.Count == 6)
            return;

        dice.Add(Instantiate(DiePrefab, transform.parent));
    }

    public void RemoveDie()
    {
        if (dice.Count == 0)
            return;

        Destroy(dice[0]);
        dice.RemoveAt(0);
    }

    public void ThrowDice()
    {
        foreach (GameObject die in dice)
        {
            Rigidbody rb = die.GetComponent<Rigidbody>();
            Vector3 rand = new Vector3(Random.Range(-15f, 15f), Random.Range(10f, 20f), Random.Range(-15f, 15f));
            rb.AddForce(rand, ForceMode.Impulse);
            rb.AddTorque(rand, ForceMode.Impulse);
        }
    }
}
