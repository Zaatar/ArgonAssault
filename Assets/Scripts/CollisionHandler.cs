using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadLevelDelay = 1.0f;
    [SerializeField] ParticleSystem ps_ExplosionVFX;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject.name} collided with {gameObject.name} and this caused a trigger!");
        ps_ExplosionVFX.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<PlayerController>().enabled = false;
        Invoke("ReloadLevel", reloadLevelDelay);
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
