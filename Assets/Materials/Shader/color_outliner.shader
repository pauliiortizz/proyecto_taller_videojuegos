Shader "Unlit/color_outliner"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1, 0, 0, 1)
        _OutlineSize ("Outline Size", Float) = 1.0
        _GlowStrength ("Glow Strength", Float) = 1.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _OutlineColor;
            float _OutlineSize;
            float _GlowStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 offset = float2(_OutlineSize / _ScreenParams.x, _OutlineSize / _ScreenParams.y);
                float outline = 0;

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        float2 sampleUV = i.uv + float2(x, y) * offset;
                        outline += step(0.1, tex2D(_MainTex, sampleUV).a);
                    }
                }

                float alphaCenter = tex2D(_MainTex, i.uv).a;
                float edge = saturate(outline / 9 - alphaCenter);

                float4 baseColor = tex2D(_MainTex, i.uv);
                float4 outlineColor = _OutlineColor * edge * _GlowStrength;

                float4 finalColor = baseColor + outlineColor;
                finalColor.a = max(baseColor.a, outlineColor.a);

                return finalColor;
            }
            ENDCG
        }
    }
}
