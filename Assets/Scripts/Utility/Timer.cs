using UnityEngine;

public class Timer
{
	protected bool m_running;

	protected float m_duration;
	protected float m_startTime;
	protected float m_pauseTime;
	protected float m_endTime;

	public Timer() 
	{
		m_running	= true;
		m_startTime	= 0.0f;
		m_pauseTime	= 0.0f;
		m_endTime	= 0.0f;
		m_duration	= 0.0f;
	}

	public void Start(double duration) 
	{
		Start((float)duration);
	}

	public void Start(float duration) 
	{
		m_running = true;
		m_duration = duration;
		m_startTime = Time.time;
		m_pauseTime = m_startTime;
		m_endTime = m_startTime + m_duration;
	}

	public void Pause() 
	{
		if (m_running == false)
		{
			return;
		}

		m_running = false;
		m_pauseTime = Time.time;
	}

	public void Resume() 
	{
		if (m_running)
		{
			return;
		}

		m_running = true;
		m_endTime = Time.time + (m_endTime - m_pauseTime);
	}

	public void Reset() 
	{
		m_running = true;
		m_startTime = Time.time;
		m_endTime = m_startTime + m_duration;
	}

	public bool Finished() 
	{
		if (m_running)
		{
			return Time.time >= m_endTime;
		}
		return false;
	}

	public double GetElapsedTime() 
	{
		return m_duration - GetRemainingTime();
	}

	public double GetRemainingTime() 
	{
		if (m_running)
		{
			return m_endTime - Time.time;
		}
		return m_endTime - m_pauseTime;
	}
}
