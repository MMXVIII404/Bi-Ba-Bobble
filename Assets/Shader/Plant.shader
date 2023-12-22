// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.2,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3693,x:32719,y:32712,varname:node_3693,prsc:2|emission-5536-OUT;n:type:ShaderForge.SFN_NormalVector,id:5559,x:30970,y:32728,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:7853,x:30970,y:32892,varname:node_7853,prsc:2;n:type:ShaderForge.SFN_Multiply,id:493,x:31398,y:32728,varname:node_493,prsc:2|A-6461-OUT,B-7924-OUT;n:type:ShaderForge.SFN_Vector1,id:7924,x:31177,y:32926,varname:node_7924,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:2735,x:31580,y:32728,varname:node_2735,prsc:2|A-493-OUT,B-7924-OUT;n:type:ShaderForge.SFN_Append,id:5191,x:31790,y:32728,varname:node_5191,prsc:2|A-2735-OUT,B-7924-OUT;n:type:ShaderForge.SFN_Tex2d,id:450,x:32011,y:32728,ptovrint:False,ptlb:RampTex,ptin:_RampTex,varname:node_450,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-5191-OUT;n:type:ShaderForge.SFN_Dot,id:6461,x:31177,y:32728,varname:node_6461,prsc:2,dt:0|A-5559-OUT,B-7853-OUT;n:type:ShaderForge.SFN_Slider,id:708,x:31904,y:32930,ptovrint:False,ptlb:Multiplier,ptin:_Multiplier,varname:node_708,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Multiply,id:7594,x:32245,y:32728,varname:node_7594,prsc:2|A-450-RGB,B-708-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:6223,x:31758,y:33033,varname:node_6223,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5536,x:32482,y:32728,varname:node_5536,prsc:2|A-7594-OUT,B-8630-OUT;n:type:ShaderForge.SFN_Multiply,id:2172,x:31974,y:33033,varname:node_2172,prsc:2|A-6223-Y,B-8600-OUT;n:type:ShaderForge.SFN_Vector1,id:8600,x:31742,y:33188,varname:node_8600,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:8630,x:32180,y:33033,varname:node_8630,prsc:2|A-2172-OUT,B-8600-OUT;proporder:450-708;pass:END;sub:END;*/

Shader "Unlit/DIceShader" {
    Properties {
        _RampTex ("RampTex", 2D) = "white" {}
        _Multiplier ("Multiplier", Range(0, 2)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _RampTex; uniform float4 _RampTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Multiplier)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float node_7924 = 0.5;
                float2 node_5191 = float2(((dot(i.normalDir,lightDirection)*node_7924)+node_7924),node_7924);
                float4 _RampTex_var = tex2D(_RampTex,TRANSFORM_TEX(node_5191, _RampTex));
                float _Multiplier_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Multiplier );
                float node_8600 = 0.5;
                float3 emissive = ((_RampTex_var.rgb*_Multiplier_var)*((i.posWorld.g*node_8600)+node_8600));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _RampTex; uniform float4 _RampTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Multiplier)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
