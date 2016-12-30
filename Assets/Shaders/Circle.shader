Shader "Test/Circle" {
	Properties{
		_Center("Center", Vector) = (0,0,0,0)
		_Radius("Radius", Float) = 0.5
		_RadiusColor("Radius Color", Color) = (1,0,0,1)
		_RadiusWidth("Radius Width", Float) = 2
		_MainTex("Main Texture", 2D) = "white" {}
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		float3 _Center;
		float _Radius;
		fixed4 _RadiusColor;
		float _RadiusWidth;
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos; //in world position of the current point we are looking to render
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			float d = distance(_Center, IN.worldPos);
			if (d > _Radius && d < (_Radius + _RadiusWidth))// if the point is outside of the circle but in the bounds of the circle's width then colour that point as the radius color
			{
				o.Albedo = _RadiusColor.rgb;
			}
			else {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			}

		}
		ENDCG
	}
	FallBack "Diffuse"
}
