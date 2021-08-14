using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaLuz : MonoBehaviour
{

    public Light LuzDefeito;

    // Update is called once per frame
    void Update()
    {
        LuzDefeito = GetComponent<Light>();
        int FalhaNaLuz = Random.Range(0, 13);
        LuzDefeito.range = FalhaNaLuz;
    }
}
