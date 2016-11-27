// References:	http://gizma.com/easing/#l
//				http://robertpenner.com/easing/
//				https://github.com/jesusgollonet/ofpennereasing/tree/master/PennerEasing

using UnityEngine;
using System.Collections.Generic;

namespace MOJ.Helpers
{

public class InterpolationHelper
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	public enum InterpolationType
	{
		None,
		Linear,
		Quadratic,
		Cubic,
		Quartic,
		Quintic,
		Sinusoidal,
		Exponential,
		Circular,
	}

	public enum EasingType
	{
		None = 0,
		In = 1,
		Out = 1 << 1,
		Both = 1 << 2
	}

	public delegate float Interpolator(float elapsedTime);

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	public static Interpolator makeInterpolator(InterpolationType interpolationType, EasingType easingType, float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator;

		bool easeIn = (easingType & EasingType.In) == EasingType.In;
		bool easeOut = (easingType & EasingType.Out) == EasingType.Out;

		switch (interpolationType)
		{
			case (InterpolationType.Linear):
			{
				interpolator = makeLinearTween(startValue, deltaValue, duration);
				break;
			}
			case (InterpolationType.Quadratic):
			{
				interpolator = makeQuadratic(easeIn, easeOut, startValue, deltaValue, duration);
				break;
			}
			case (InterpolationType.Cubic):
			{
				interpolator = makeCubic(easeIn, easeOut, startValue, deltaValue, duration);
				break;
			}
			case (InterpolationType.Quartic):
			{
				interpolator = makeQuartic(easeIn, easeOut, startValue, deltaValue, duration);
				break;
			}
			case (InterpolationType.Quintic):
			{
				interpolator = makeQuintic(easeIn, easeOut, startValue, deltaValue, duration);
				break;
			}
			case (InterpolationType.Sinusoidal):
			{
				interpolator = makeSinusoidal(easeIn, easeOut, startValue, deltaValue, duration);
				break;
			}
			case (InterpolationType.Exponential):
			{
				interpolator = makeExponential(easeIn, easeOut, startValue, deltaValue, duration);
				break;
			}
			case (InterpolationType.Circular):
			{
				interpolator = makeCircular(easeIn, easeOut, startValue, deltaValue, duration);
				break;
			}
			default:
			{
				interpolator = makeNone(startValue, deltaValue, duration);
				break;
			}
		}

		return interpolator;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Interpolations
	//////////////////////////////////////////////////////////////////////////////////////////

	// No interpolation
	private static Interpolator makeNone(float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator = delegate (float t) 
									{
										return deltaValue + startValue;
									};
		return interpolator;
	}

	//////////////////////////////////////////////////

	// No easing, no acceleration.
	private static Interpolator makeLinearTween(float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator = delegate (float t) 
									{
										return deltaValue * t / duration + startValue;
									};
		return interpolator;
	}

	//////////////////////////////////////////////////

	private static Interpolator makeQuadratic(bool easeIn, bool easeOut, float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator;

		if(easeIn && easeOut) // Acceleration until halfway, then deceleration.
		{
			interpolator = delegate (float t) 
							{
								t /= duration / 2;
								if (t < 1) return deltaValue / 2 * t * t + startValue;
								t--;
								return -deltaValue / 2 * (t * (t - 2) - 1) + startValue;
							};
        }
		else if(easeIn) // Decelerating to zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								return deltaValue * t * t + startValue;
							};			
		}
		else if(easeOut) // Accelerating from zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								return -deltaValue * t * (t - 2) + startValue;
							};
		}
		else // No easing, no acceleration.
		{
			interpolator = makeLinearTween(startValue, deltaValue, duration);
        }	

		return interpolator;
	}

	//////////////////////////////////////////////////

	private static Interpolator makeCubic(bool easeIn, bool easeOut, float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator;

		if(easeIn && easeOut) // Acceleration until halfway, then deceleration.
		{
			interpolator = delegate (float t) 
							{
								t /= duration / 2;
								if (t < 1) return deltaValue / 2 * t * t * t + startValue;
								t -= 2;
								return deltaValue / 2 * (t * t * t + 2) + startValue;
							};
        }
		else if(easeIn) // Decelerating to zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								return deltaValue * t * t * t + startValue;
							};			
		}
		else if(easeOut) // Accelerating from zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								t--;
								return deltaValue * (t * t * t + 1) + startValue;
							};
		}
		else // No easing, no acceleration.
		{
			interpolator = makeLinearTween(startValue, deltaValue, duration);
        }	

		return interpolator;
	}

	//////////////////////////////////////////////////

	private static Interpolator makeQuartic(bool easeIn, bool easeOut, float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator;

		if(easeIn && easeOut) // Acceleration until halfway, then deceleration.
		{
			interpolator = delegate (float t) 
							{
								t /= duration / 2;
								if (t < 1) return deltaValue / 2 * t * t * t * t + startValue;
								t -= 2;
								return -deltaValue / 2 * (t * t * t * t - 2) + startValue;
							};
        }
		else if(easeIn) // Decelerating to zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								return deltaValue * t * t * t * t + startValue;
							};			
		}
		else if(easeOut) // Accelerating from zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								t--;
								return -deltaValue * (t * t * t * t - 1) + startValue;
							};
		}
		else // No easing, no acceleration.
		{
			interpolator = makeLinearTween(startValue, deltaValue, duration);
        }	

		return interpolator;
	}

	//////////////////////////////////////////////////

	private static Interpolator makeQuintic(bool easeIn, bool easeOut, float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator;

		if(easeIn && easeOut) // Acceleration until halfway, then deceleration.
		{
			interpolator = delegate (float t) 
							{
								t /= duration / 2;
								if (t < 1) return deltaValue / 2 * t * t * t * t * t + startValue;
								t -= 2;
								return deltaValue / 2 * (t * t * t * t * t + 2) + startValue;
							};
        }
		else if(easeIn) // Decelerating to zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								return deltaValue * t * t * t * t * t + startValue;
							};			
		}
		else if(easeOut) // Accelerating from zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								t--;
								return deltaValue * (t * t * t * t * t + 1) + startValue;
							};
		}
		else // No easing, no acceleration.
		{
			interpolator = makeLinearTween(startValue, deltaValue, duration);
        }	

		return interpolator;
	}

	//////////////////////////////////////////////////

	private static Interpolator makeSinusoidal(bool easeIn, bool easeOut, float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator;

		if(easeIn && easeOut) // Acceleration until halfway, then deceleration.
		{
			interpolator = delegate (float t) 
							{
								return -deltaValue / 2 * (Mathf.Cos(Mathf.PI * t / duration) - 1) + startValue;
							};
        }
		else if(easeIn) // Decelerating to zero velocity.
		{
			interpolator = delegate (float t) 
							{
								return -deltaValue * Mathf.Cos(t / duration * (Mathf.PI / 2)) + deltaValue + startValue;
							};			
		}
		else if(easeOut) // Accelerating from zero velocity.
		{
			interpolator = delegate (float t) 
							{
								return deltaValue * Mathf.Sin(t / duration * (Mathf.PI / 2)) + startValue;
							};
		}
		else // No easing, no acceleration.
		{
			interpolator = makeLinearTween(startValue, deltaValue, duration);
        }	

		return interpolator;
	}

	//////////////////////////////////////////////////

	private static Interpolator makeExponential(bool easeIn, bool easeOut, float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator;

		if(easeIn && easeOut) // Acceleration until halfway, then deceleration.
		{
			interpolator = delegate (float t) 
							{
								t /= duration / 2;
								if (t < 1) return deltaValue / 2 * Mathf.Pow(2, 10 * (t - 1)) + startValue;
								t--;
								return deltaValue / 2 * (-Mathf.Pow(2, -10 * t) + 2) + startValue;
							};
        }
		else if(easeIn) // Decelerating to zero velocity.
		{
			interpolator = delegate (float t) 
							{
								return deltaValue * Mathf.Pow(2, 10 * (t / duration - 1)) + startValue;
							};			
		}
		else if(easeOut) // Accelerating from zero velocity.
		{
			interpolator = delegate (float t) 
							{
								return deltaValue * (-Mathf.Pow(2, -10 * t / duration) + 1) + startValue;
							};
		}
		else // No easing, no acceleration.
		{
			interpolator = makeLinearTween(startValue, deltaValue, duration);
        }	

		return interpolator;
	}

	//////////////////////////////////////////////////

	private static Interpolator makeCircular(bool easeIn, bool easeOut, float startValue, float deltaValue, float duration)
	{
		Interpolator interpolator;

		if(easeIn && easeOut) // Acceleration until halfway, then deceleration.
		{
			interpolator = delegate (float t) 
							{
								t /= duration / 2;
								if (t < 1) return -deltaValue / 2 * (Mathf.Sqrt(1 - t * t) - 1) + startValue;
								t -= 2;
								return deltaValue / 2 * (Mathf.Sqrt(1 - t * t) + 1) + startValue;
							};
        }
		else if(easeIn) // Decelerating to zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								return -deltaValue * (Mathf.Sqrt(1 - t * t) - 1) + startValue;
							};			
		}
		else if(easeOut) // Accelerating from zero velocity.
		{
			interpolator = delegate (float t) 
							{
								t /= duration;
								t--;
								return deltaValue * Mathf.Sqrt(1 - t * t) + startValue;
							};
		}
		else // No easing, no acceleration.
		{
			interpolator = makeLinearTween(startValue, deltaValue, duration);
        }	

		return interpolator;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Custom Interpolators
	//////////////////////////////////////////////////////////////////////////////////////////

	public class Vector3Interpolator
	{
		public float duration;
		public Interpolator x;
		public Interpolator y;
		public Interpolator z;

		public Vector3Interpolator(InterpolationType interpolationType, EasingType easingType, Vector3 startValue, Vector3 deltaValue, float duration)
		{
			this.duration = duration;
			this.x = makeInterpolator(interpolationType, easingType, startValue.x, deltaValue.x, duration);
			this.y = makeInterpolator(interpolationType, easingType, startValue.y, deltaValue.y, duration);
			this.z = makeInterpolator(interpolationType, easingType, startValue.z, deltaValue.z, duration);
		}
	}

	public static void setVector3Interpolator(
		Vector3Interpolator interpolator, 
		InterpolationType interpolationType, 
		EasingType easingType, 
		Vector3 startValue, 
		Vector3 deltaValue, 
		float duration)
	{
		if(interpolator != null)
		{
			interpolator.duration = duration;
			interpolator.x = makeInterpolator(interpolationType, easingType, startValue.x, deltaValue.x, duration);
			interpolator.y = makeInterpolator(interpolationType, easingType, startValue.y, deltaValue.y, duration);
			interpolator.z = makeInterpolator(interpolationType, easingType, startValue.z, deltaValue.z, duration);
		}
	}

	public static Vector3 getInterpolatedVector3(Vector3Interpolator interpolator, float elapsedTime)
	{
		Vector3 retVal = Vector3.zero;

		retVal.x = interpolator.x(elapsedTime);
		retVal.y = interpolator.y(elapsedTime);
		retVal.z = interpolator.z(elapsedTime);

		return retVal;
	}

	#endregion
}

}