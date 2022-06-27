Shader "FeGameArt/Patreon/ColorMaskPerRenderData"
{
    Properties
    {
        [Header(____Textures____)]
        [Space(5)]
        _MainTex("Texture", 2D) = "white" {}
        _RGBMask("RGB Mask", 2D) = "white" {}

        [HideInInspector]
        [PerRendererData]
        _RChannel("Red Channel", Color) = (1, 1, 1, 1)
        [HideInInspector]
        [PerRendererData]
        _GChannel("Green Channel", Color) = (1, 1, 1, 1)
        [HideInInspector]
        [PerRendererData]
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
            
            // Use Shader model 3.0 target
            #pragma target 3.0
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"
            #include "Assets/Project/03-GeneralResources/Shaders/CustomLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _RGBMask;

            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _RChannel)
                UNITY_DEFINE_INSTANCED_PROP(float4, _GChannel)
                UNITY_DEFINE_INSTANCED_PROP(float4, _BChannel)
            UNITY_INSTANCING_BUFFER_END(Props)
            //float4 _RChannel;
            //float4 _GChannel;
            //float4 _BChannel;

            float4 applyRGBMask(float4 originalColor, float4 colorMask)
            {
                //Get custom color applied to mask
                float4 multipliedColor = colorMask.rrrr * UNITY_ACCESS_INSTANCED_PROP(Props, _RChannel) +
                    colorMask.gggg * UNITY_ACCESS_INSTANCED_PROP(Props, _GChannel) +
                    colorMask.bbbb * UNITY_ACCESS_INSTANCED_PROP(Props, _BChannel);

                //Check if the pixel is masked.
                //If there is value in any color channel, 
                //it means the pixel is masked
                float isMaskedPixel = colorMask.r + colorMask.g + colorMask.b;

                //Based in the isMaskedPixel, lerp between
                //the original color and the multiplied color
                //to keep the main texture that is not affected
                //by our mask
                float4 finalColor = lerp(originalColor, multipliedColor, isMaskedPixel);
                                
                return finalColor;
            }

            v2f vert (appdata v)
            {
                v2f o;
                
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 colMask = tex2D(_RGBMask, i.uv);

                //colorMask(col, colMask);

                col = applyRGBMask(col, colMask);

                diffuseLight(col, i.worldNormal);

                return col;
            }
            ENDCG
        }
    }
}
