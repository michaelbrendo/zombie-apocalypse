using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbody;

    private float waitBodyDisappear = 4;
    
    void Awake(){
        meuRigidbody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade){
                    meuRigidbody.MovePosition
                (meuRigidbody.position +
                direcao.normalized * velocidade * Time.deltaTime);
    }

    public void Rotacionar(Vector3 direcao){

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        meuRigidbody.MoveRotation(novaRotacao);
    }

    public void Morrer(){            
        StartCoroutine(waiter());
    }

IEnumerator waiter()
{
    yield return new WaitForSeconds(waitBodyDisappear);
    meuRigidbody.constraints = RigidbodyConstraints.None;
    meuRigidbody.velocity = Vector3.zero;
    GetComponent<Collider>().enabled = false;
}



}
