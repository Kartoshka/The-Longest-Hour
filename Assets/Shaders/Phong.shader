Shader "Test/Phong" {
	Properties {
		_MainTint ("Diffuse Tint", Color) = (1,1,1,1)
		_MainTex("Base (RGB)" , 2D) = "white" {}
		_SpecularColor("Specular Color", Color) = (1,1,1,1)
		_SpecPower("Specular Power", Range(0,30)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf PhongLight

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		float4 _MainTint;
		float4 _SpecularColor;
		float _SpecPower;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
		}

		fixed4 LightingPhongLight(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed attenuation)
		{
			//Reflection Vector
			float NdotL = dot(s.Normal, lightDir);
			float3 reflectionVector = normalize(2.0 * s.Normal * NdotL - lightDir);

			//Specular Component
			float spec = pow(max(0, dot(reflectionVector, viewDir)), _SpecPower);
			float3 finalSpec = spec * _SpecularColor.rgb;

			//final light
			fixed4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * max(0, NdotL) * attenuation) + (_LightColor0.rgb * finalSpec);
			c.a = s.Alpha;
			return c;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
