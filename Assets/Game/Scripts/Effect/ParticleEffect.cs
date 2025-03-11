using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : ComponentBehavior
{
    [SerializeField] private ParticleSystem m_ParticleSystem;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (m_ParticleSystem == null) m_ParticleSystem = transform.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        if (m_ParticleSystem != null)
        {
            m_ParticleSystem.Play();
            StartCoroutine(DisableAfterPlay());
        }
       
    }

    IEnumerator DisableAfterPlay()
    {
        yield return new WaitUntil(() => !m_ParticleSystem.IsAlive());
        PoolingManager.Despawn(gameObject);
    }
}
