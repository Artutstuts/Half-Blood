﻿/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class TargetVuforiaPersonalizado : MonoBehaviour, ITrackableEventHandler {

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    public static bool faseTutorial;
    bool fase1;
    bool fase2;
    bool fase3;
    bool fase4;
    public static bool jaRodou = false;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start() {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

    }

    protected virtual void OnDestroy() {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus ,
        TrackableBehaviour.Status newStatus) {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName +
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE) {
            OnTrackingLost();
        }
        else {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound() {
        if (mTrackableBehaviour) {
            if (RotacaoPersonagem.inicioAnim == false) {
                RotacaoPersonagem.naoMexer = false;
            }
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
                component.enabled = true;

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;

            if (gameObject.name == "FaseTutorialImageTarget") {
                faseTutorial = true;
                if (MenuIniciarJogo.faseTutorial == true) {
                    if (jaRodou == false) {
                        StartCoroutine(RodarAnim(faseTutorial , "LugarFaseTutorial"));
                    }
                }
            }
            if (gameObject.name == "Fase1ImageTarget") {
                fase1 = true;
                if (MenuIniciarJogo.fase1 == true) {
                    if (jaRodou == false) {
                        StartCoroutine(RodarAnim(fase1 , "LugarFase1"));
                    }
                }
            }
            if (gameObject.name == "Fase2ImageTarget") {
                fase2 = true;
                if (MenuIniciarJogo.fase2 == true) {
                    if (jaRodou == false) {
                        StartCoroutine(RodarAnim(fase2 , "LugarFase2"));
                    }
                }
            }
            if (gameObject.name == "Fase3ImageTarget") {
                fase3 = true;
                if (MenuIniciarJogo.fase3 == true) {
                    if (jaRodou == false) {
                        StartCoroutine(RodarAnim(fase3 , "LugarFase3"));
                    }
                }
            }
        }
    }


    protected virtual void OnTrackingLost() {
        RotacaoPersonagem.naoMexer = true;
        RotacaoPersonagem.x = 0;
        RotacaoPersonagem.z = 0;
        Movimento.rb.velocity = new Vector3(0 , 0 , 0);
        if (mTrackableBehaviour) {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;

            if (gameObject.name == "FaseTutorialImageTarget") {
                faseTutorial = false;
            }
            if (gameObject.name == "Fase1ImageTarget") {
                fase1 = false;
            }
            if (gameObject.name == "Fase2ImageTarget") {
                fase2 = false;
            }
            if (gameObject.name == "Fase3ImageTarget") {
                fase3 = false;
            }
        }
    }

    #endregion // PROTECTED_METHODS


    IEnumerator RodarAnim(bool fase , string lugar) {
        jaRodou = true;
        // yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(GameObject.Find("TelaDoCapitulo").GetComponent<Animation>().GetClip("Tela_Inicio1").length - 1f);
        //GetComponentInChildren<Animation>().Rewind();
        yield return new WaitUntil(() => fase == true);
        GetComponentInChildren<Animation>().Play();
        print("QQQQ");
        GetComponent<CameraShake>().enabled = true;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3);
        GameObject.FindGameObjectWithTag("Player").transform.SetParent(GameObject.Find(gameObject.name).transform);
        GameObject.FindGameObjectWithTag("Player").transform.localPosition = GameObject.Find(lugar).transform.localPosition;
        GameObject.FindGameObjectWithTag("Player").transform.localRotation = GameObject.Find(lugar).transform.localRotation;
        if (gameObject.name == "FaseTutorialImageTarget") {
            GameObject.FindGameObjectWithTag("Player").GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<AndarSosinho>().enabled = true;
        }
        yield return new WaitForSeconds(0.5f);
        RotacaoPersonagem.inicioAnim = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().speed = 1;
        yield return new WaitForSeconds(1);
        RotacaoPersonagem.naoMexer = false;
        GameObject.FindGameObjectWithTag("Finish").GetComponent<Canvas>().enabled = true;
    }

}