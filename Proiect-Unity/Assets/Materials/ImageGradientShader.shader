Shader "Custom/ImageGradientShader"
{
    Properties
    {
        _Texture1 ("Texture 1 (Blue)", 2D) = "white" {}
        _Texture2 ("Texture 2 (Yellow)", 2D) = "white" {}
        _Texture3 ("Texture 3 (Red)", 2D) = "white" {}
        _HealthFactor ("Health Factor", Range(0, 1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Background" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            ZTest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _Texture1;
            sampler2D _Texture2;
            sampler2D _Texture3;
            float _HealthFactor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Blend between textures
                float4 color1 = tex2D(_Texture1, uv);
                float4 color2 = tex2D(_Texture2, uv);
                float4 color3 = tex2D(_Texture3, uv);

                float4 finalColor;
                if (_HealthFactor <= 0.5)
                {
                    float t = _HealthFactor / 0.5;
                    finalColor = lerp(color1, color2, t);
                }
                else
                {
                    float t = (_HealthFactor - 0.5) / 0.5;
                    finalColor = lerp(color2, color3, t);
                }

                return finalColor;
            }
            ENDCG
        }
    }
}
