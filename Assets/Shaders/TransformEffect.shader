Shader "Custom/Transform Effect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_OldTex ("Base (RGB)", 2D) = "white" {}
		_Radius ("Dist to Edge",float) = 0
		
	}
	SubShader {
		
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			
			Blend SrcAlpha OneMinusSrcAlpha
					
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _OldTex;
			uniform half _Radius;

			fixed4 frag (v2f_img i) : COLOR
			{
				fixed4 base = tex2D(_MainTex, i.uv);
				fixed4 old = tex2D(_OldTex, i.uv);
				
				half2 distfromcenter = 2 * ( i.uv - float2(0.5,0.5));
				
				float dist = sqrt(distfromcenter.x * distfromcenter.x + distfromcenter.y * distfromcenter.y); // 0 - 1 distance from center
				
				float distcutoff = saturate(10*(_Radius-dist));
				
				fixed4 output = lerp(old,base,distcutoff);
				
				return output; 
			}
			ENDCG
		}
	} 
	FallBack off
}
