using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum OscillationMode
{
    Horizontal,
    Vertical
}

public class OscillatingPlatform : MonoBehaviour
{
    [SerializeField]
    OscillationMode oscillationMode;

    [SerializeField]
    Vector2 oscillationLimits = new Vector2(-1f, 1f);

    [SerializeField]
    float speed = 1.0f;

    Vector3 initPosition;
    int target = 0; //0 or 1

    float displacement = 0;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        target = 1;
    }

    private Vector3 offset;
    private GameObject targetGO = null;

    // Update is called once per frame
    void FixedUpdate()
    {
        displacement = 0.0f;

        float targetPosition = 0f;
        Vector3 newPosition = transform.position;
        if (oscillationMode == OscillationMode.Horizontal)
        {
            targetPosition = initPosition.x + (target == 0 ? oscillationLimits.x : oscillationLimits.y);
            displacement = targetPosition - transform.position.x > 0.0 ? 1.0f : -1.0f;
            displacement = displacement * Time.fixedDeltaTime * speed;
    
            newPosition.x += displacement;

            if (Math.Abs(targetPosition - transform.position.x) < displacement)
                target = target == 0 ? 1 : 0;
        }
        else
        {
            targetPosition = initPosition.y + (target == 0 ? oscillationLimits.x : oscillationLimits.y);
            displacement = targetPosition - transform.position.y > 0.0 ? 1.0f : -1.0f;
            displacement = displacement * Time.fixedDeltaTime * speed;

            newPosition.y += displacement;

            if (Math.Abs(targetPosition - transform.position.y) < displacement)
                target = target == 0 ? 1 : 0;
        }

        GetComponent<Rigidbody2D>().MovePosition(newPosition);
        if (targetGO != null)
        {
            
            if (targetGO.tag == "Player" && PlayerController.IsPlayerNotMoving)
            {
                Debug.Log("GameObject is " + targetGO.tag + " IsPlayerNotMoving: " + PlayerController.IsPlayerNotMoving);
                //targetGO.GetComponent<Rigidbody2D>().MovePosition(targetGO.transform.position + (newPosition - transform.position));
                targetGO.transform.position = newPosition + offset;
            }
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {
        targetGO = col.gameObject;
        offset = targetGO.transform.position - transform.position;

        //Debug.Log(col.name);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        targetGO = null;
    }
}
