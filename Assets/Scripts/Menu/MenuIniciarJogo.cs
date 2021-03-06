﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuIniciarJogo : MonoBehaviour {

    public GameObject seleTela;
    public GameObject conq;
    public Sprite tut;
    public Sprite fas1;
    public Sprite fas2;
    public GameObject s;
    public GameObject prog;
    public GameObject fill;
    public static bool faseTutorial;
    public static bool fase1;
    public static bool fase2;
    public static bool fase3;
    public static bool fase4;
    public Sprite conqTutorial;

    public void Jogar() {
        seleTela.SetActive(true);
        SegundaFaseMecanica.gameOver = false;
        TargetVuforiaPersonalizado.jaRodou = false;
    }

    public void Voltar() {
        GameObject.Find("Voltar").transform.parent.gameObject.SetActive(false);
    }

    public void Conquistas() {
        conq.SetActive(true);
        if(FimTutorial.conquistaTutorial == true) {
            GameObject.Find("Bebe").GetComponent<Image>().sprite = conqTutorial;
        }
    }

    public void QrCodesVariados() {
        prog.SetActive(true);
        fill.GetComponent<Image>().sprite = tut;
        faseTutorial = false;
        fase1 = false;
        fase2 = false;
        fase3 = false;
        fase4 = false;
        StartCoroutine(Carregar("QrCodes"));
    }

    public void Tutoba() {
        prog.SetActive(true);
        fill.GetComponent<Image>().sprite = tut;
        faseTutorial = true;
        fase1 = false;
        fase2 = false;
        fase3 = false;
        fase4 = false;
        StartCoroutine(Carregar("Tutorial"));
    }

    public void Fase1() {
        prog.SetActive(true);
        fill.GetComponent<Image>().sprite = fas1;
        faseTutorial = false;
        fase1 = true;
        fase2 = false;
        fase3 = false;
        fase4 = false;
        StartCoroutine(Carregar("Fase1"));
    }

    public void Fase2() {
        prog.SetActive(true);
        fill.GetComponent<Image>().sprite = fas2;
        faseTutorial = false;
        fase1 = false;
        fase2 = true;
        fase3 = false;
        fase4 = false;
        StartCoroutine(Carregar("Fase2"));
    }

    public void Fase3() {
        prog.SetActive(true);
        fill.GetComponent<Image>().sprite = fas2;
        faseTutorial = false;
        fase1 = false;
        fase2 = false;
        fase3 = true;
        fase4 = false;
        StartCoroutine(Carregar("Fase3"));
    }

    public void Fase4() {
        prog.SetActive(true);
        fill.GetComponent<Image>().sprite = fas2;
        faseTutorial = false;
        fase1 = false;
        fase2 = false;
        fase3 = false;
        fase4 = true;
        StartCoroutine(Carregar("Fase4"));
    }

    IEnumerator Carregar(string fase) {
        AsyncOperation load = SceneManager.LoadSceneAsync(fase, LoadSceneMode.Single);
        s.GetComponent<Slider>().value = load.progress;
        while (!load.isDone) {
            s.GetComponent<Slider>().value = load.progress;
            yield return null;
        }

    }


}
