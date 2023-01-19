using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControl : MonoBehaviour
{
     //posicao da seta
    [SerializeField]private Transform posStart;

    //seta
    public GameObject setaGo;
    

    // angulo
    public float zRotate;
    public bool liberaRot = false;
    public bool liberaTiro = false;

    // forca
    private Rigidbody2D bola;
    public float force = 0;
    public GameObject seta2Img;


    void Awake()
    {
        setaGo = GameObject.Find ("Seta");
        seta2Img = setaGo.transform.GetChild (0).gameObject;
        setaGo.SetActive (false);
    }

    void Start()
    {
       
        posStart = GameObject.Find ("posStart").GetComponent<Transform> ();
        PosicionaBola ();

        //forca
        bola = GetComponent<Rigidbody2D> ();
       
    }

    // Update is called once per frame
    void Update()
    {
        RotacaoSeta ();
        InputDeRotacao ();
        LimitaRotacao ();
        PosicionaSeta ();

        //forca
         AplicaForca ();
         ControlaForca ();
    }

    void PosicionaSeta()
    {
        setaGo.GetComponent<Image>().rectTransform.position = transform.position;
    }

     void PosicionaBola()
    {
        this.gameObject.transform.position = posStart.position;
    }

    void RotacaoSeta()
    {
        setaGo.GetComponent<Image>().rectTransform.eulerAngles = new Vector3 (0,0,zRotate);
    }

    void InputDeRotacao()
    {

        if(liberaRot == true)
      {
        //float moveX = Input.GetAxis ("Mouse X");
        float moveY = Input.GetAxis ("Mouse Y");

        if (zRotate < 90)
         {
                if (moveY > 0)
                {
                    zRotate += 2.5f;
                }
         }

        if (zRotate > 0)
          {
                if (moveY < 0)
                {
                    zRotate -= 2.5f;
                }
          }

      }

    }    

      void LimitaRotacao()
    {

      if (zRotate >= 90)
        {
            zRotate = 90;
        }

      if (zRotate <= 0)
        {
            zRotate = 0;
        }

    } 

    void OnMouseDown()
    {
       liberaRot = true;
       setaGo.SetActive (true);
    }

    void OnMouseUp()
    {
        liberaRot = false;
        liberaTiro = true;
        setaGo.SetActive (false);
        AudioManager.instance.SonsFXToca (1);
    }

    //forca
    void AplicaForca()
    {
        //cos significa coceno e sin seno
        float x = force + Mathf.Cos (zRotate = Mathf.Deg2Rad);
        float y = force + Mathf.Sin (zRotate = Mathf.Deg2Rad);

        if(liberaTiro == true)
        {
            bola.AddForce(new Vector2 (x,y));
            liberaTiro = false;
        }
    }

    void ControlaForca()
    {
        if(liberaRot == true)
        {
            float moveX = Input.GetAxis ("Mouse X");

            if(moveX < 0)
            {
                seta2Img.GetComponent<Image>().fillAmount += 1 * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;
            }

             if(moveX > 0)
            {
                seta2Img.GetComponent<Image>().fillAmount -= 1 * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;
            }
        }
    }

}
