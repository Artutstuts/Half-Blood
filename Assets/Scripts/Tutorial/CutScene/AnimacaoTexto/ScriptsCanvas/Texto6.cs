﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texto6 : MonoBehaviour {

	public GameObject texto6;
	public GameObject textoContinuar;
	bool podeavancar = false;
	Touch touch;
	GameObject canvasBotoes;

	// Use this for initialization
	void Start () {
		canvasBotoes = GameObject.FindGameObjectWithTag("Finish");
		StartCoroutine(Trava());
		StartCoroutine(Continuar());
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Input.touchCount; i++) {
		if((Input.GetKeyDown(KeyCode.Space)|| Input.GetTouch(i).phase == TouchPhase.Began ||Input.GetMouseButtonDown (0) ) && podeavancar == true)
		{
			texto6.GetComponent<Animator>().SetBool("Proximo", true);
			textoContinuar.GetComponent<Animator>().SetBool("Proximo", true);
			StartCoroutine(IrTexto7());
		}
		}
		if(Input.GetKeyDown(KeyCode.Space)&& podeavancar == true)
		{
			texto6.GetComponent<Animator>().SetBool("Proximo", true);
			textoContinuar.GetComponent<Animator>().SetBool("Proximo", true);
			StartCoroutine(IrTexto7());
		}
	}


	IEnumerator Voltarbotao(){
			yield return new WaitForSeconds(3f);
			canvasBotoes.GetComponent<Canvas>().enabled = true;
		}

	IEnumerator Trava(){
		yield return new WaitForSeconds(6f);
		podeavancar = true;

	}

	IEnumerator Continuar(){
		yield return new WaitForSeconds (5f);
		textoContinuar.SetActive(true);
	}

	IEnumerator IrTexto7(){
		yield return new WaitForSeconds(2f);
		textoContinuar.SetActive(false);
		podeavancar = false;
		StartCoroutine(Destroir());
	}

	IEnumerator Destroir(){
		yield return new WaitForSeconds(3f);
		texto6.SetActive(false);
		
	}
}
