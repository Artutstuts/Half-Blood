﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.AI;

public class Sala3 : MonoBehaviour {
	GameObject pedra;
	GameObject pedrainv;
	GameObject porta;
    GameObject garra;
	bool coletoupedra = false;
	bool abriuporta = false;
	static public bool iniciarAndar;
	static public bool colocoupedra;
	static public bool animacaoAndar;
    float distP;
    float distG;
    public Sprite Ituto;
    public GameObject teto;
    public AudioClip pegouAudio;
    public AudioClip colocouAudio;
    public AudioClip passos;


    // Use this for initialization
    void Start () {
		pedra = GameObject.Find("Pedra_Tutorial");
		pedrainv = GameObject.Find("Pedra_Tutorial_Inv");
		porta = GameObject.Find("PortaFase4");
        garra = GameObject.Find("GarraTutorial");
        GameObject.Find("TelaDoCapitulo").GetComponent<Image>().sprite = Ituto;
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = passos;
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(ColetarPedra());
        StartCoroutine(ColocarPedra());
	}

    IEnumerator ColetarPedra() {
        if (Input.GetKeyDown(KeyCode.Space) == true || CrossPlatformInputManager.GetButtonDown("Jump") == true) {
            distP = Vector3.Distance(gameObject.transform.position, pedra.gameObject.transform.position);
            if (distP < 15) {
                 if (coletoupedra == false) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = pegouAudio;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
                    coletoupedra = true;
                     RotacaoPersonagem.naoMexer = true;
                     RotacaoPersonagem.x = 0;
                     RotacaoPersonagem.z = 0;
                     Movimento.rb.velocity = new Vector3(0, 0, 0);
                     Vector3 alvo = new Vector3(pedra.transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, pedra.transform.position.z);
                     GameObject.FindGameObjectWithTag("Player").transform.LookAt(alvo, Vector3.up);
                     GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("PegouChao");
                     yield return new WaitForSecondsRealtime(1);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = passos;
                    RotacaoPersonagem.naoMexer = false;
                     distP = 100;
                     pedra.GetComponent<MeshRenderer>().enabled = false;
                     pedra.transform.localPosition = new Vector3(pedra.transform.localPosition.x, pedra.transform.localPosition.y - 5, pedra.transform.localPosition.z);
                 }
            }
        }
    }

    IEnumerator ColocarPedra() {
        if (Input.GetKeyDown(KeyCode.Space) == true || CrossPlatformInputManager.GetButtonDown("Jump") == true) {
            distG = Vector3.Distance(gameObject.transform.position, garra.gameObject.transform.position);
            if (distG < 10) {
                if (coletoupedra == true && colocoupedra == false) {
                    colocoupedra = true;
                    abriuporta = true;
                    porta.GetComponent<BoxCollider>().enabled = false;
                    RotacaoPersonagem.naoMexer = true;
                    RotacaoPersonagem.x = 0;
                    RotacaoPersonagem.z = 0;
                    Movimento.rb.velocity = new Vector3(0, 0, 0);
                    Vector3 alvo = new Vector3(garra.transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, garra.transform.position.z);
                    GameObject.FindGameObjectWithTag("Player").transform.LookAt(alvo, Vector3.up);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("TocouParede");
                    yield return new WaitForSecondsRealtime(1);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = colocouAudio;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
                    pedra.gameObject.transform.SetParent(garra.transform);
                    pedra.gameObject.transform.localPosition = pedrainv.transform.localPosition;
                    pedra.gameObject.transform.localRotation = pedrainv.transform.localRotation;
                    pedra.GetComponent<MeshRenderer>().enabled = true;
                   // pedrainv.GetComponent<MeshRenderer>().enabled = true;
                    yield return new WaitForSecondsRealtime(1);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = passos;
                    porta.GetComponent<Animator>().SetBool("Abrir", true);
                    teto.GetComponent<Animation>().Play();
                    iniciarAndar = true;
                    animacaoAndar = true;
                    RotacaoPersonagem.naoMexer = false;
                }
            }
        }
    }

	IEnumerator InicioAndando(){
		yield return new WaitForSeconds(2f);
		iniciarAndar = true;
		animacaoAndar = true;
	}

	IEnumerator TocandoParede(){
		yield return new WaitForSeconds(1f);
		pedrainv.GetComponent<MeshRenderer>().enabled = true;
	}

	IEnumerator AbrindoPorta(){
		yield return new WaitForSeconds(2f);
		porta.GetComponent<Animator>().SetBool("Abrir", true);
	}

}
