using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects : MonoBehaviour
{
    public AudioSource attackMelee;
    public AudioSource rangeAttack1;
    public AudioSource rangeAttack2;
    public AudioSource rangeAttack3;
    public AudioSource rangeAttack4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMeleeAttackSound()
    {
        attackMelee.Play();
    }

    public void PlayAttackRange1Sound()
        { rangeAttack1.Play(); }

    public void PlayAttackRange2Sound() {
        rangeAttack2.Play(); }

    public void PlayAttackRange3Sound() {
        rangeAttack3.Play(); }

    public void PlayAttackRange4Sound() {
        rangeAttack4.Play(); }
}
