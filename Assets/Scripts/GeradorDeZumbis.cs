using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeZumbis : MonoBehaviour
{

    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distaciaDeGeracao = 3;
    private float distaciaDoJogadorParaGeracao = 20;
    private GameObject Jogador;

    private void Start(){
        Jogador = GameObject.FindWithTag("Jogador");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Jogador.transform.position) > distaciaDoJogadorParaGeracao)
        {
            contadorTempo += Time.deltaTime;

            if(contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distaciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi(){
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while(colisores.Length > 0){
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
        Instantiate(Zumbi, posicaoDeCriacao, transform.rotation);
    }

    Vector3 AleatorizarPosicao(){

        Vector3 posicao = Random.insideUnitSphere * distaciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
