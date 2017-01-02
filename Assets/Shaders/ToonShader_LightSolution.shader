Shader "Test/ToonShader_LightSolution" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RampTex("Ramp Map", 2D) = "white" {}
		_EdgeColor("Edge Color", Color) = (0,0,0,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Toon

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _RampTex;
		fixed3 _EdgeColor;

		struct Input {
			float2 uv_MainTex;
			float2 uv_RampTex;
			float3 worldNormal;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color

			half NdotL = 1 - dot(IN.viewDir, IN.worldNormal);

			if (NdotL >= 0.75)
			{
				o.Albedo = _EdgeColor.rgb;
			}
			else
			{
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			}

		}

		half4 LightingToon(SurfaceOutput s, half3 lightDir, half attenuation)
		{
			half NdotL = dot(s.Normal, lightDir);
			NdotL = tex2D(_RampTex, float2(NdotL, 0.5)); // moving horizontaly along the texture's U axis

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb *(NdotL * attenuation); // color of surface * color of light * amount of light hitting surface * attenuation
			c.a = s.Alpha;

			return c;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
