Shader "Unlit/color_shine_animated"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _ShineColor("Shine Color", Color) = (1,1,1,1)
        _ShineWidth("Shine Width", Float) = 0.1
        _ShineDirectionX("Shine Direction X", Float) = 1.0
        _ShineDirectionY("Shine Direction Y", Float) = 1.0
        _ShineSpeed("Shine Speed", Float) = 2.0 // Nueva propiedad
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;

            float4 _ShineColor;
            float _ShineWidth;
            float _ShineDirectionX;
            float _ShineDirectionY;
            float _ShineSpeed; // Nueva variable

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 baseColor = tex2D(_MainTex, i.uv);

                float2 dir = normalize(float2(_ShineDirectionX, _ShineDirectionY));

                // Shine animado con seno absoluto
                float shinePos = dot(i.uv, dir) - abs(sin(_Time.y * _ShineSpeed));

                float shine = smoothstep(0.0, _ShineWidth, shinePos) * 
                              (1.0 - smoothstep(_ShineWidth, _ShineWidth * 2.0, shinePos));

                float4 shineColor = _ShineColor * shine * baseColor.a;
                float4 finalColor = baseColor + shineColor;

                return finalColor;
            }
            ENDCG
        }
    }
}
