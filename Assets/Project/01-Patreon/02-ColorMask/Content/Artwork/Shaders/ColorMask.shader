Shader "FeGameArt/Patreon/ColorMask"
{
    Properties
    {
        [Header(____Textures____)]
        [Space(5)]
        _MainTex("Texture", 2D) = "white" {}
        _ColorMask("Color Mask", 2D) = "white" {}

        [Space(10)]

        [Header(____Color channels____)]
        [Space(5)]
        _RChannel("Red Channel", Color) = (1, 1, 1, 1)
        _GChannel("Green Channel", Color) = (1, 1, 1, 1)
        _BChannel("Blue Channel", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Assets/Project/03-GeneralResources/Shaders/CustomLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _ColorMask;

            float4 _RChannel;
            float4 _GChannel;
            float4 _BChannel;

            void colorMask(inout float4 originalColor, float4 colorMask)
            {
                originalColor = lerp(originalColor, colorMask.r * _RChannel, colorMask.r);
                originalColor = lerp(originalColor, colorMask.g * _GChannel, colorMask.g);
                originalColor = lerp(originalColor, colorMask.b * _BChannel, colorMask.b);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 colMask = tex2D(_ColorMask, i.uv);

                colorMask(col, colMask);

                diffuseLight(col, i.worldNormal);

                return col;
            }
            ENDCG
        }
    }
}
