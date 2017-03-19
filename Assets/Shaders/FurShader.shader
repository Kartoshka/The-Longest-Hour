Shader "Custom/FurShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_PixelTex("Albedo (RGB)", 2D) = "white" {}

		_FurLength("Fur Length", Range(0, 1)) = .5
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_CutoffEnd("Alpha cutoff end", Range(0,1)) = 0.5
		_EdgeFade("Edge Fade", Range(0,1)) = 0.5

		_Gravity("Gravity direction", Vector) = (0,0,1,0)
		_GravityStrength("G strenght", Range(0,1)) = 0.25
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True" }
		ZWrite On
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		fixed4 _Color;
		sampler2D _MainTex;
		sampler2D _PixelTex;

		uniform float _FurLength;
		uniform float _Cutoff;
		uniform float _CutoffEnd;
		uniform float _EdgeFade;

		uniform fixed3 _Gravity;
		uniform fixed _GravityStrength;

		struct Input {
			float2 uv_MainTex;
			float2 uv_PixelTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Alpha = c.a;
		}
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.05
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.1
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.15
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.20
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.25
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.30
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.35
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.40
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.45
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.50
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.55
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.60
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.65
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.70
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.75
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.80
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.85
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.90
		#include "FurPass.cginc"
		ENDCG

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:blend vertex:vert
		#define FUR_MULTIPLIER 0.95
		#include "FurPass.cginc"
		ENDCG
	}
	FallBack "Diffuse"
}
