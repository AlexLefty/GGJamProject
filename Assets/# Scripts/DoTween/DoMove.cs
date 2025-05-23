﻿using DG.Tweening;
using UnityEngine;

[AddComponentMenu("DoTween/DoMove")]
public class DoMove : MonoBehaviour, IActivatable
{
    [SerializeField] private Vector3 m_position;
    [SerializeField] private float m_duration;
    [SerializeField] private Transform m_target;

    private bool m_isActivated;

    public void Activate()
    {
        if (m_isActivated) return;

        m_target.DOMove(m_position, m_duration);

        m_isActivated = true;
    }
}