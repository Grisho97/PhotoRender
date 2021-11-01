// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/Test"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        
    
       ZWrite on
		ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
     
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float depth: SV_Depth;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.depth = -UnityObjectToClipPos(v.vertex).z * _ProjectionParams.w;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;

            fixed4 frag (v2f i) : SV_Target
            {
                //get depth from depth texture
                float invert = tex2D(_CameraDepthTexture, i.uv).r;
                //linear depth between camera and far clipping plane
                invert = Linear01Depth(invert);
                //depth as distance from camera in units
                invert = invert * _ProjectionParams.z;
                return fixed4 (invert,invert,invert,1);
            }
            ENDCG
        }
    }
}
