using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //usado para buscar algo dentro da cena
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI pontosUI;

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
        pontosUI = GameObject.Find ("PontosUI").GetComponent<TextMeshProUGUI> ();
    }

    
    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        
    }
}
