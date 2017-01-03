Shader "Test/Transparent" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
		_AlphaTex("AlphaTex", 2D) = "white" {}
		_AlphaSlider("Alpha Slider", Range(-2,2)) = 1
		_Color("Tint", Color) = (1,1,1,1)
	}
	SubShader {
		Tags {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		Cull Back
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _AlphaTex;
		fixed4 _Color;
		float _AlphaSlider;

		struct Input {
			float2 uv_MainTex;
			float2 uv_AlphaTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;

			float4 cAlpha = tex2D(_AlphaTex, IN.uv_AlphaTex);
			o.Alpha = cAlpha.r * _AlphaSlider;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
