using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public int Vida = 100;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem AnimacaoJogador;

    private void Start()
    {
        Time.timeScale = 1;
        AnimacaoJogador = GetComponent<AnimacaoPersonagem>();
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
    }

    // Update is called once per frame
    void Update()
    {     
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);
                
        AnimacaoJogador.Movimentar(direcao.magnitude);
        
        if(Vida <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(direcao, Velocidade);
        meuMovimentoJogador.RotacaoJogador(MascaraChao);

    }
    public void TomarDano(int dano)
    {
        Vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if( Vida <= 0)
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }

    }
}
