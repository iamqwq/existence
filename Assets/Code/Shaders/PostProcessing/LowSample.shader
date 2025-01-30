Shader "Project/PostProcessing/LowSample" {
    
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader {
        
        Tags {
            "Pipeline" = "UniversalRenderPipeline"
        }
        
        ZWrite Off
        ZTest Always
        Cull Off
             
        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            struct appdata {
                float4 pos_os : POSITION;
                half2 texcoord : TEXCOORD0;
            };
            
            struct v2f {
                float4 pos_cs : SV_POSITION;
                half2 texcoord : TEXCOORD0;
            };
            
            v2f vert(appdata v) {
                v2f o;
                o.pos_cs = TransformObjectToHClip(v.pos_os.xyz);
                o.texcoord = v.texcoord;
                return o;
            }

            half4 frag(v2f i) : SV_Target {
                // return half4(1, 1, 1, 1);
                return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, floor(i.texcoord * _ScreenParams.xy / 4.0f) * 4.0f / _ScreenParams.xy);
            }
            ENDHLSL
        }
    }
}
