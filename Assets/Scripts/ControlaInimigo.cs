using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public GameObject Jogador;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    public AudioClip SomDeMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private int distanceSphere = 10;
    private float porcentagemGerarKitMedico = 0.1f;
    public GameObject KitMedicoPrefab;



    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        AleatorizarZumbi();
        statusInimigo = GetComponent<Status>();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);
        
        if(distancia > 15){
            Vagar();
        }
        else if (distancia > 2.5)
        {
            Perseguir();
        }
        else
        {
            animacaoInimigo.atacar(true);
        }
    }

    void Perseguir() {
        direcao = Jogador.transform.position - transform.position;
        movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        animacaoInimigo.atacar(false);        
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        
        if(contadorVagar <= 0){
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += statusInimigo.tempoEntrePosicoesAleatorias;
        }

        bool ficouPertoOsuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if(ficouPertoOsuficiente == false ){
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        }        
    }

    Vector3 AleatorizarPosicao(){
        Vector3 posicao = Random.insideUnitSphere * distanceSphere;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }
    void Atacajogador()
    {
        int dano = Random.Range(20, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi(){
         int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if(statusInimigo.Vida <= 0){
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);   
        VerificarGeracaoDoKitMedico(porcentagemGerarKitMedico);

    }

    void VerificarGeracaoDoKitMedico(float porcentagemGeracao)
    {
        if(Random.value <= porcentagemGeracao){
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }

    }
}
