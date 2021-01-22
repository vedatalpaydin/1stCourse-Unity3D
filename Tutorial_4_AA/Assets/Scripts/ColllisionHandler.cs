using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColllisionHandler : MonoBehaviour
{
 [Tooltip("In seconds")][SerializeField]  float levelLoadDelay = 1f;
 [Tooltip("FX explosion on player")][SerializeField]  GameObject deathFX;
 
 void OnTriggerEnter(Collider col)
 {
  PlayerDeathSequence();
  deathFX.SetActive(true);
  Invoke("ReloadScene",levelLoadDelay);
 }

 private void PlayerDeathSequence()
 {
  SendMessage("OnPlayerDeath");
 }

 private void ReloadScene()
 {
  SceneManager.LoadScene(1);
 }
}
