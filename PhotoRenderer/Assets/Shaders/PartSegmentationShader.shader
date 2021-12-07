Shader "Custom/PartSigmentationShader"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _Tex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }

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
                float depth: DEPTH;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.depth = -UnityObjectToClipPos(v.vertex).z * _ProjectionParams.w;
                return o;
            }

            sampler2D _Tex;
            sampler2D _CameraDepthTexture;

            fixed4 frag (v2f i) : SV_Target
            {
                float depthAlfa = tex2D(_Tex, i.uv).r;

                float invert1 = 1 - Linear01Depth(tex2D(_CameraDepthTexture, i.uv));

                if (depthAlfa != 0 && depthAlfa >= invert1-0.01)
                    return fixed4 (1,1,1,1);
                else
                    return fixed4 (0,0,0,1);
                
            }
            ENDCG
        }
    }
}
