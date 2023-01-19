using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Bola
    [SerializeField]
    private GameObject bola;
    private int bolasNum = 2;
    private bool bolaMorreu = false;
    private int bolasEmCena = 0;
    private Transform pos;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad (this.gameObject);
        }
        else
        {
            Destroy (gameObject);
        }

         SceneManager.sceneLoaded += Carrega;
        
    }

     void Carrega(Scene cena, LoadSceneMode modo)
    {
        //aqui coloca o que estamos buscando dentro da cena
        pos = GameObject.Find ("posStart").GetComponent<Transform> ();
    }
    
    void Start()
    {
        ScoreManager.instance.GameStartScoreM ();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.instance.UpdateScore ();
        UIManager.instance.UpdateUI ();

        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene ("Level2");
        }

        NascBolas ();
    }

    void NascBolas()
    {
        if(bolasNum > 0 && bolasEmCena == 0)
        {
            Instantiate (bola, new Vector2(pos.position.x,pos.position.y),Quaternion.identity);
            bolasEmCena += 1;
        }
    }
}
