using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace WipeOut
{
	public class TimeTrial : MonoBehaviour
	{
		public TextMeshProUGUI hours;
		public TextMeshProUGUI minutes;
		public TextMeshProUGUI seconds;


		public bool timerCanStart;
		public Canvas menu;
		public BlackHole blackHole;

		private bool hasFinished;
		private bool addingSecond;
		private int m_hours;
		private int m_minutes;

		public int m_seconds;

		private void Start()
		{
			menu.enabled = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				hasFinished = true;
			}
		}

		private IEnumerator AddSecond()
		{
			addingSecond = true;

			yield return new WaitForSeconds(1);

			m_seconds++;
			addingSecond = false;
		}

		void FixedUpdate()
		{
			if(!hasFinished)
			{
				if(!addingSecond && timerCanStart)
					StartCoroutine(AddSecond());

				if(m_seconds >= 60)
				{
					m_seconds = 0;
					m_minutes++;
				}

				if(m_minutes >= 60)
				{
					m_minutes = 0;
					m_hours++;
				}
			}

			if(hasFinished)
			{
				blackHole.enabled = true;
			}

			hours.text = m_hours < 10 ? $"0{m_hours.ToString()}" : m_hours.ToString();
			minutes.text = m_minutes < 10 ? $"0{m_minutes.ToString()}" : m_minutes.ToString();
			seconds.text = m_seconds < 10 ? $"0{m_seconds.ToString()}" : m_seconds.ToString();
		}
	}
}