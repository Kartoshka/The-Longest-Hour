﻿Shader "Test/TexturedShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_NMIntensity("Normal Map Intensity", Range(0,5)) = 1
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalMap;

		struct Input { //basic input for the surface shader function below
			float2 uv_MainTex; // using uv in front of _MainTex gets you the uv data in the model
			float2 uv_NormalMap;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _NMIntensity;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;

			//get normal uv value
			fixed3 normalValue = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap)).rgb;
			normalValue.x *= _NMIntensity;
			normalValue.y *= _NMIntensity;
			o.Normal = normalize(normalValue);

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
