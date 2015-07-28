using UnityEngine;

public class Timer
{
	protected bool m_running;

	protected double m_duration;
	protected double m_startTime;
	protected double m_pauseTime;
	protected double m_endTime;

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

	public double Duration
	{
		get { return m_duration; }
		set { m_duration = value; }
	}

	public double ElapsedTime
	{
		get
		{
			return m_duration - RemainingTime;
		}
	}

	public double RemainingTime
	{
		get
		{
			if (m_running)
			{
				return m_endTime - Time.time;
			}
			else
			{
				return m_endTime - m_pauseTime;
			}
		}
	}

	public double Percentage
	{
		get { return ElapsedTime / m_duration; }
	}
}
