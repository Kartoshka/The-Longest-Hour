Shader "Custom/AlphaToggle" {
	Properties {
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Color("Color Tint", Color) = (1,1,1,1)
		_AlphaValue("Alpha" , Range(-1, 1)) = 1
	}
	SubShader {
		Tags{
			"RenderType" = "Qpaque"
		}

		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert fullforwardshadows alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		float _AlphaValue;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha =  _AlphaValue;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
