using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator meuAnimator;

    void Awake(){
        meuAnimator = GetComponent<Animator>();

    }
    public void atacar(bool estado){
        meuAnimator.SetBool("Atacando", estado);
    }

    public void Movimentar(float ValorDeMovimento)
    {          
          meuAnimator.SetFloat("Movendo", ValorDeMovimento);
    }



}
