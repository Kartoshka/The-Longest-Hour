Shader "Test/Silhoutte" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Tint("Tint", Color) = (1,1,1,1)
		_DotProduct("Rim effect", Range(-1, 1)) = 0.25
	}
		SubShader{
		Tags{ 
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		Cull Off
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha:fade nolighting

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Tint;
		float _DotProduct;


	struct Input {
		float2 uv_MainTex;
		float3 worldNormal;
		float3 viewDir;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		
		float4 c = tex2D(_MainTex, IN.uv_MainTex) * _Tint;
		o.Albedo = c.rgb;

		float border = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
		float alpha = (border * (1 - _DotProduct) + _DotProduct);
		o.Alpha = c.a * alpha;

	}
	ENDCG
	}
		FallBack "Diffuse"
}
