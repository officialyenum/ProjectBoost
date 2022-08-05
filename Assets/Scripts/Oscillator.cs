using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)]float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        Debug.Log(startingPos);
    }

    // Update is called once per frame
    void Update()
    {
        OscillateObject();
    }

    void OscillateObject()
    {
        if (period <= Mathf.Epsilon){ return; } // Mathf.Epsilon is the smallest floating number i.e 0.001f or 0f
        float cycles = Time.time / period; // grows continually from 0
        const float tau = Mathf.PI * 2; // constant value of 6.28 
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1
        Debug.Log(rawSinWave);
        // movementFactor = rawSinWave / 2f + 0.5f;
        movementFactor = (rawSinWave + 1f) / 2f; // recalculating the movementFactor to be between 0 and 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
