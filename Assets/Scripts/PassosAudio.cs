using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassosAudio : MonoBehaviour {


    private AudioClip clips;
    public AudioSource passosAudioSource;
    public List<AudioClip> passosFloor;
    public List<AudioClip> passosWater;

    public List<AudioClip> listAtual;

    public void Passo() {
        clips = listAtual[Random.Range(0, listAtual.Count)];
        passosAudioSource.PlayOneShot(clips);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Water")) {
            listAtual = passosWater;
        }
        else if (hit.gameObject.CompareTag("Floor")) {
            listAtual = passosFloor;
        }
    }
}
