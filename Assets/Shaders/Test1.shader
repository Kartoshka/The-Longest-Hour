Shader "Test/Test1" {
	Properties{
		// Variable Name ("GUI Name", Type) = Default Value
		_Color("Color", Color) = (1,1,1,1)
		_AmbientColor("Ambient Color", Color) = (1,1,1,1)
		_Intensity("Intensity", Range(0,10)) = 5.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM

			
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0


		struct Input {
			float2 uv_MainTex;
		};

		//re-declaring the above parameters for use here
		fixed4 _Color; //becaue a color has 4 values
		float4 _AmbientColor; //float4 because a color is a R,G,B,A value
		float _Intensity; //float because sliders are float values


		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = pow((_Color + _AmbientColor), _Intensity); // == (_Color + _AmbientColor) ^ (_Intensity)
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
