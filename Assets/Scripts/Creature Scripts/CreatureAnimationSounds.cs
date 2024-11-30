using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationSounds : MonoBehaviour
{
    [SerializeField] AudioSource breathing;
    [SerializeField] AudioSource growl;
    [SerializeField] AudioSource growl2;
    [SerializeField] AudioSource grunt;
    [SerializeField] AudioSource roar;
    [SerializeField] AudioSource roar2;
    [SerializeField] AudioSource moan;
    [SerializeField] AudioSource wetGore;
    [SerializeField] AudioSource metalScratch;
    [SerializeField] AudioSource metalTwang;

    public void playBreathing() { breathing.Play();}

    public void playGrowl() { growl.Play();}

    public void playGrowl2() { growl2.Play();}

    public void playGrunt() { grunt.Play();}

    public void playRoar() { roar.Play(); }

    public void playRoar2() { roar2.Play(); }

    public void playMoan() {  moan.Play();}

    public void playWetGore() {  wetGore.Play();}

    public void playMetalScratch() {  metalScratch.Play();}

    public void playMetalTwang() {  metalTwang.Play();}
}
