// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32740,y:33254,varname:node_2865,prsc:2|emission-2181-OUT;n:type:ShaderForge.SFN_TexCoord,id:4219,x:30694,y:33387,cmnt:Default coordinates,varname:node_4219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Relay,id:8397,x:30919,y:33387,cmnt:Refract here,varname:node_8397,prsc:2|IN-4219-UVOUT;n:type:ShaderForge.SFN_Tex2dAsset,id:4430,x:30694,y:33574,ptovrint:False,ptlb:MainTex,ptin:_MainTex,cmnt:MainTex contains the color of the scene,varname:_MainTex,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7542,x:31095,y:33504,varname:node_1672,prsc:2,ntxv:0,isnm:False|UVIN-8397-OUT,TEX-4430-TEX;n:type:ShaderForge.SFN_Posterize,id:2420,x:31812,y:33478,varname:node_2420,prsc:2|IN-7542-R,STPS-4700-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4700,x:31123,y:33744,ptovrint:False,ptlb:Steps,ptin:_Steps,varname:_Steps,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Color,id:6919,x:31859,y:33003,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:2181,x:32180,y:33271,varname:node_2181,prsc:2|A-6919-RGB,B-9141-RGB,T-2420-OUT;n:type:ShaderForge.SFN_Color,id:9141,x:31825,y:33207,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:_Color2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:2977,x:31512,y:33798,varname:node_2977,prsc:2,tex:e958c6041cfe445e987c73751e8d4082,ntxv:0,isnm:False|UVIN-2857-UVOUT,TEX-5075-TEX;n:type:ShaderForge.SFN_TexCoord,id:1486,x:30869,y:33961,varname:node_1486,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:2857,x:31315,y:33804,varname:node_2857,prsc:2,spu:0.1,spv:0.1|UVIN-1486-UVOUT;n:type:ShaderForge.SFN_Tex2dAsset,id:5075,x:30880,y:34138,ptovrint:False,ptlb:NoiseMain,ptin:_NoiseMain,varname:_NoiseMain,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e958c6041cfe445e987c73751e8d4082,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1054,x:31534,y:33969,varname:node_1054,prsc:2,tex:e958c6041cfe445e987c73751e8d4082,ntxv:0,isnm:False|UVIN-5323-UVOUT,TEX-5075-TEX;n:type:ShaderForge.SFN_Panner,id:5323,x:31319,y:33978,varname:node_5323,prsc:2,spu:-0.1,spv:0.1|UVIN-1486-UVOUT;n:type:ShaderForge.SFN_Add,id:4569,x:31920,y:33684,varname:node_4569,prsc:2|A-7542-RGB,B-2977-RGB,C-1054-RGB,D-4575-RGB;n:type:ShaderForge.SFN_Divide,id:6723,x:32090,y:33640,varname:node_6723,prsc:2|A-4569-OUT,B-7888-OUT;n:type:ShaderForge.SFN_Vector1,id:7888,x:31835,y:33919,varname:node_7888,prsc:2,v1:4;n:type:ShaderForge.SFN_Multiply,id:4648,x:32157,y:33445,varname:node_4648,prsc:2|A-2420-OUT,B-3700-OUT;n:type:ShaderForge.SFN_Slider,id:3700,x:32083,y:33865,ptovrint:False,ptlb:Power,ptin:_Power,varname:_Power,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Panner,id:229,x:31319,y:34154,varname:node_229,prsc:2,spu:-0.1,spv:-0.1|UVIN-1486-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4575,x:31534,y:34145,varname:node_1055,prsc:2,tex:e958c6041cfe445e987c73751e8d4082,ntxv:0,isnm:False|UVIN-229-UVOUT,TEX-5075-TEX;proporder:4430-4700-6919-9141-5075-3700;pass:END;sub:END;*/

Shader "Shader Forge/S_Posterization" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Steps ("Steps", Float ) = 4
        _Color ("Color", Color) = (1,0,0,1)
        _Color2 ("Color2", Color) = (0,0,1,1)
        _NoiseMain ("NoiseMain", 2D) = "white" {}
        _Power ("Power", Range(0, 10)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Steps;
            uniform float4 _Color;
            uniform float4 _Color2;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_8397 = i.uv0; // Refract here
                float4 node_1672 = tex2D(_MainTex,TRANSFORM_TEX(node_8397, _MainTex));
                float node_2420 = floor(node_1672.r * _Steps) / (_Steps - 1);
                float3 emissive = lerp(_Color.rgb,_Color2.rgb,node_2420);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
