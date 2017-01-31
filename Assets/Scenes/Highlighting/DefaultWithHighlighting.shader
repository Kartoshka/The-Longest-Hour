Shader "Test/DefaultWithHighlighting" {
	Properties {
		_Color("Tint Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalTex("Normal Map", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_NMIntensity("Normal Map Intensity", Range(0,5)) = 1
		_IsHighlighted ("IsHighlighted", Int ) = 0
		_HighlightColor("Highlight Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		float4 _Color;

		sampler2D _MainTex; 
		sampler2D _NormalTex;

		half _Glossiness;
		half _Metallic;

		float _NMIntensity;
		int _IsHighlighted;

		float4 _HighlightColor;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed4 c = _Color;
			if (_IsHighlighted == 0)
			{
				c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			}

			if (_IsHighlighted == 1)
			{
				c = _HighlightColor;
			}

			fixed3 normalMap = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex)).rgb;
			normalMap.x *= _NMIntensity;
			normalMap.y *= _NMIntensity;

			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Normal = normalize(normalMap);
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
