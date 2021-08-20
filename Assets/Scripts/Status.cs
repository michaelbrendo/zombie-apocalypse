using UnityEngine;

public class Status : MonoBehaviour
{
    public int VidaInicial = 100;
    //[HideInInspector]
    public int Vida;
    public float Velocidade = 5;
    public float tempoEntrePosicoesAleatorias = 4;

    void Awake(){
        
        Vida = VidaInicial;
    }

}
