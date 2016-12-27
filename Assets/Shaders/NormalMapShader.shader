Shader "Test/NormalMapShader" {
	Properties {
		_MainTint("Diffuse Tint", Color) = (1,1,1,1)
		_NormalTex("Normal Map", 2D) = "bump" {} // putting "bump" tells unity that this is a normal map. It intitilizes the field to grey which means no bump
		_NMIntensity("Normal Map Intensity", Range(0,5)) = 1
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _NormalTex;
		float4 _MainTint;
		float _NMIntensity;

		struct Input {
			float2 uv_NormalTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {

			fixed3 normalMap = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex)).rgb;
			normalMap.x *= _NMIntensity;
			normalMap.y *= _NMIntensity;
			o.Normal = normalize(normalMap);
			o.Albedo = _MainTint.rgb;
			o.Alpha = _MainTint.a;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
