using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{

    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador")
        .GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);

        TextoTempoDeSobrevivencia.text = "Sobreviveu por " + minutos + "min e " + segundos + "s";

        AjustarpontucaoMaxima(minutos, segundos);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }

    void AjustarpontucaoMaxima(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Melhor tempo {0}min e {1}s", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }

        if(TextoPontuacaoMaxima.text == "")
        {
            min = (int)tempoPontuacaoSalvo / 60;
            seg = (int)tempoPontuacaoSalvo % 60;
            TextoPontuacaoMaxima.text = string.Format("Melhor tempo {0}min e {1}s", min, seg);
        }
    }
}