Shader "Test/PackedTexture" {
	Properties {
		_MainTint ("Main Tint", Color) = (1,1,1,1)
		_Color1 ("Color Tint 1", Color) = (1,1,1,1)
		_Color2 ("Color Tint 2", Color) = (1,1,1,1)
		_rImage ("rImage", 2D) = "white" {}
		_gImage ("gImage", 2D) = "white" {}
		_bImage ("bImage", 2D) = "white" {}
		_aImage ("aImage", 2D) = "white" {}
		_rBlend ("rBlend", 2D) = "white" {}
		_gBlend ("gBlend", 2D) = "white" {}
		_bBlend ("bBlend", 2D) = "white" {}
		_aBlend ("aBlend", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 4.0

		sampler2D _rImage;
		sampler2D _gImage;
		sampler2D _bImage;
		sampler2D _aImage;
		sampler2D _rBlend;
		sampler2D _gBlend;
		sampler2D _bBlend;
		sampler2D _aBlend;

		struct Input {
			float2 uv_rImage;
			float2 uv_gImage;
			float2 uv_bImage;
			float2 uv_aImage;
			float2 uv_rBlend;
			float2 uv_gBlend;
			float2 uv_bBlend;
			float2 uv_aBlend;
		};

		fixed4 _MainTint;
		fixed4 _Color1;
		fixed4 _Color2;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			float4 rData = tex2D(_rImage, IN.uv_rImage);
			float4 gData = tex2D(_gImage, IN.uv_gImage);
			float4 bData = tex2D(_bImage, IN.uv_bImage);
			float4 aData = tex2D(_aImage, IN.uv_aImage);

			float4 rBlendData = tex2D(_rBlend, IN.uv_rBlend);
			float4 gBlendData = tex2D(_gBlend, IN.uv_gBlend);
			float4 bBlendData = tex2D(_bBlend, IN.uv_bBlend);
			float4 aBlendData = tex2D(_aBlend, IN.uv_aBlend);

			float4 finalColor;
			finalColor = lerp(rData, gData, gBlendData); //blend from r to g using the g mask
			finalColor = lerp(finalColor, bData, bBlendData); //blend from the previous pixel to b using the b mask
			finalColor = lerp(finalColor, aData, aBlendData);
			finalColor.a = 1.0;

			float4 terrainLayers = lerp(_Color1, _Color2, rBlendData); //use the r mask to determine what the colour gradient for the terrain will be
			finalColor *= terrainLayers; //apply the gradient
			finalColor = saturate(finalColor); //saturate.com

			o.Albedo = finalColor.rgb * _MainTint.rgb; //apply colour to fixed4 by multiplying them
			o.Alpha = finalColor.a;


		}
		ENDCG
	}
	FallBack "Diffuse"
}
