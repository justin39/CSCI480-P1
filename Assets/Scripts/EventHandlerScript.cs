using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Events;

public class EventHandlerScript : MonoBehaviour, ITrackableEventHandler
{
    public UnityEvent onDetected;
	private TrackableBehaviour m_TrackableBehavior;

    void Start()
    {
		m_TrackableBehavior = GetComponent<TrackableBehaviour>();
		if (m_TrackableBehavior)
            m_TrackableBehavior.RegisterTrackableEventHandler(this);
    }

    void Update()
    {
    }

	public void OnTrackableStateChanged(TrackableBehaviour.Status previous, TrackableBehaviour.Status now)
	{
        if (now == TrackableBehaviour.Status.TRACKED && previous != TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            onDetected.Invoke();
        }
	}
}
